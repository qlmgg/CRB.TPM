﻿using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Logging.Core.Application.CrmRewrite.Dto;
using CRB.TPM.Mod.Logging.Core.Domain.CrmRewrite;
using CRB.TPM.Mod.Logging.Core.Infrastructure;
using CRB.TPM.Utils.Map;
using System;
using CRB.TPM.Data.Core.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.Logging.Core.Application.CrmRewrite
{
    public class CrmRewriteService : ICrmRewriteService
    {
        private readonly IMapper _mapper;
        private readonly LoggingDbContext _dbContext;
        private readonly ICrmRewriteRepository _repository;
        public CrmRewriteService(IMapper mapper,
            LoggingDbContext dbContext,
            ICrmRewriteRepository repository)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _repository = repository;
        }

        public Task<PagingQueryResultModel<CrmRewriteEntity>> Query(CrmRewriteQueryDto dto)
        {
            var query = _repository.Find();
            return query.ToPaginationResult(dto.Paging);
        }

        [Transaction]
        public async Task<IResultModel> Add(CrmRewriteAddDto dto)
        {
            var entity = _mapper.Map<CrmRewriteEntity>(dto);
            //if (await _repository.Exists(entity))
            //{
            //return ResultModel.HasExists;
            //}

            var result = await _repository.Add(entity);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Delete(Guid id)
        {
            var result = await _repository.Delete(id);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _repository.Get(id);
            if (entity == null)
                return ResultModel.NotExists;

            var dto = _mapper.Map<CrmRewriteUpdateDto>(entity);
            return ResultModel.Success(dto);
        }

        [Transaction]
        public async Task<IResultModel> Update(CrmRewriteUpdateDto dto)
        {
            var entity = await _repository.Get(dto.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(dto, entity);

            //if (await _repository.Exists(entity))
            //{
            //return ResultModel.HasExists;
            //}

            var result = await _repository.Update(entity);

            return ResultModel.Result(result);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public async Task<IResultModel> BulkAdd(List<CrmRewriteEntity> dtos)
        {
            if (dtos != null && dtos.Count > 0)
            {
                if (await _repository.BulkAdd(dtos))
                {
                    return ResultModel.Success();
                }
                else
                { 
                    return ResultModel.Failed("添加失败");
                }
            }
            return ResultModel.Success();
        }
    }
}
