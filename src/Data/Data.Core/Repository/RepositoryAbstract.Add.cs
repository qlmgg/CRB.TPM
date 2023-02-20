using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Events;
using CRB.TPM.Data.Core.Descriptors;
using CRB.TPM.Data.Core.Extensions;

namespace CRB.TPM.Data.Core.Repository;

/// <summary>
/// 新增
/// </summary>
public abstract partial class RepositoryAbstract<TEntity>
{
    public async Task<bool> Add(TEntity entity, IUnitOfWork uow = null)
    {
        string tableName = null;

        //启用按时间分表策略时
        if (EntityDescriptor.IsSharding)
        {
            //取分表字段的日期，为空时使用系统日期
            var date = EntityDescriptor.GetShardingFieldValue(entity);

            //获取指定时间分表表名
            tableName = GetShardingTableName(date, entity);

        }

        return await Add(entity, tableName, uow);
    }

    /// <summary>
    /// 新增，自定义表名称
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="tableName"></param>
    /// <param name="uow">工作单元</param>
    /// <returns></returns>
    public async Task<bool> Add(TEntity entity, string tableName, IUnitOfWork uow = null)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity), "entity is null");

        var sql = _sql.GetAdd(tableName);

        SetCreateInfo(entity);

        SetTenantInfo(entity);

        _logger.Write("Add", sql);

        var primaryKey = EntityDescriptor.PrimaryKey;
        if (primaryKey.IsInt)
        {
            //自增主键
            sql += _adapter.IdentitySql;
            var id = await ExecuteScalar<int>(sql, entity, uow);
            if (id > 0)
            {
                primaryKey.PropertyInfo.SetValue(entity, id);

                await HandleEntityAddEvents(entity, tableName, uow);

                _logger.Write("NewID", id.ToString());

                return true;
            }

            return false;
        }

        if (primaryKey.IsLong)
        {
            //自增主键
            sql += _adapter.IdentitySql;
            var id = await ExecuteScalar<long>(sql, entity, uow);
            if (id > 0)
            {
                primaryKey.PropertyInfo.SetValue(entity, id);

                await HandleEntityAddEvents(entity, tableName, uow);

                _logger.Write("NewID", id.ToString());
                return true;
            }
            return false;
        }

        if (primaryKey.IsGuid)
        {
            var id = (Guid)primaryKey.PropertyInfo.GetValue(entity)!;
            if (id == Guid.Empty)
            {
                primaryKey.PropertyInfo.SetValue(entity, _adapter.CreateSequentialGuid());
            }

            _logger.Write("NewID", id.ToString());

            if (await Execute(sql, entity, uow) > 0)
            {
                await HandleEntityAddEvents(entity, tableName, uow);

                return true;
            }
            return false;
        }

        if (await Execute(sql, entity, uow) > 0)
        {
            await HandleEntityAddEvents(entity, tableName, uow);

            return true;
        }

        return false;
    }

    /// <summary>
    /// 处理实体新增事件
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="tableName"></param>
    /// <param name="uow"></param>
    /// <returns></returns>
    private async Task HandleEntityAddEvents(TEntity entity, string tableName, IUnitOfWork uow)
    {
        if (!EntityDescriptor.EnableAddEvent) return;

        var events = _sp.GetServices<IEntityAddEvent>().ToList();
        if (!events.Any()) return;

        try
        {
            foreach (var changeEvents in events)
            {
                await changeEvents.OnAdd(new EntityAddContext
                {
                    DbContext = DbContext,
                    EntityDescriptor = EntityDescriptor,
                    Entity = entity,
                    TableName = tableName,
                    Uow = uow,
                    AddTime = DateTime.Now,
                    TenantId = DbContext.AccountResolver.TenantId,
                    Operator = DbContext.AccountResolver.AccountId,
                    OperatorName = DbContext.AccountResolver.AccountName
                });
            }
        }
        catch (Exception ex)
        {
            _logger.Write("HandleEntityAddEvents", ex.Message);
        }
    }
}