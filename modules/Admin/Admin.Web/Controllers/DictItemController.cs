﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.DictItem;
using CRB.TPM.Mod.Admin.Core.Application.DictItem.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.DictItem;
using Swashbuckle.AspNetCore.Annotations;
using NSwag.Annotations;

namespace CRB.TPM.Mod.Admin.Web.Controllers;

[OpenApiTag("DictItem", AddToDocument = true, Description = "数据字典项")]
public class DictItemController : Web.ModuleController
{
    private readonly IDictItemService _service;

    public DictItemController(IDictItemService service)
    {
        _service = service;
    }

    /// <summary>
    /// 查询
    /// </summary>
    [HttpGet]
    public Task<PagingQueryResultModel<DictItemEntity>> Query([FromQuery] DictItemQueryDto dto)
    {
        return _service.Query(dto);
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <remarks></remarks>
    [HttpPost]
    public Task<IResultModel> Add(DictItemAddDto dto)
    {
        return _service.Add(dto);
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public Task<IResultModel> Edit(int id)
    {
        return _service.Edit(id);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <remarks></remarks>
    [HttpPost]
    public Task<IResultModel> Update(DictItemUpdateDto dto)
    {
        return _service.Update(dto);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public Task<IResultModel> Delete([BindRequired] int id)
    {
        return _service.Delete(id);
    }
}