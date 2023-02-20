﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CRB.TPM.Auth.Abstractions.Annotations;
using CRB.TPM.Mod.Admin.Core.Application.DictGroup;
using CRB.TPM.Mod.Admin.Core.Application.DictGroup.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.DictGroup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NSwag.Annotations;
using Swashbuckle.AspNetCore.Annotations;

namespace CRB.TPM.Mod.Admin.Web.Controllers;

[OpenApiTag("DictGroup", AddToDocument = true, Description = "数据字典分组")]
public class DictGroupController : Web.ModuleController
{
    private readonly IDictGroupService _service;

    public DictGroupController(IDictGroupService service)
    {
        _service = service;
    }

    /// <summary>
    /// 查询
    /// </summary>
    [HttpGet]
    public Task<IResultModel<IList<DictGroupEntity>>> Query([FromQuery] DictGroupQueryDto dto)
    {
        return _service.Query(dto);
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <remarks></remarks>
    [HttpPost]
    public Task<IResultModel> Add(DictGroupAddDto dto)
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
    public Task<IResultModel> Update(DictGroupUpdateDto dto)
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

    /// <summary>
    /// 下拉列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowWhenAuthenticated]
    public Task<IResultModel> Select()
    {
        return _service.Select();
    }
}