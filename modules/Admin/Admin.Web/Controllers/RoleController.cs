﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using CRB.TPM.Auth.Abstractions.Annotations;
using CRB.TPM.Mod.Admin.Core.Application.Role;
using CRB.TPM.Mod.Admin.Core.Application.Role.Dto;
using CRB.TPM.Mod.Admin.Core.Domain.Role;
using Swashbuckle.AspNetCore.Annotations;
using System;
using NSwag.Annotations;

namespace CRB.TPM.Mod.Admin.Web.Controllers;

[OpenApiTag("Role", AddToDocument = true, Description = "角色管理")]
public class RoleController : Web.ModuleController
{
    private readonly IRoleService _service;

    public RoleController(IRoleService service)
    {
        _service = service;
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <remarks>查询角色列表</remarks>
    [HttpGet]
    public Task<IResultModel<IList<RoleEntity>>> Query()
    {
        return _service.Query();
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <remarks></remarks>
    [HttpPost]
    public Task<IResultModel> Add(RoleAddDto dto)
    {
        return _service.Add(dto);
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns></returns>
    [HttpGet]
    public Task<IResultModel> Edit(Guid id)
    {
        return _service.Edit(id);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <remarks></remarks>
    [HttpPost]
    public Task<IResultModel> Update(RoleUpdateDto dto)
    {
        return _service.Update(dto);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns></returns>
    [HttpDelete]
    public Task<IResultModel> Delete([BindRequired] Guid id)
    {
        return _service.Delete(id);
    }

    /// <summary>
    /// 查询角色绑定菜单信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public Task<IResultModel> QueryBindMenus([BindRequired] Guid id)
    {
        return _service.QueryBindMenus(id);
    }

    /// <summary>
    /// 更新绑定菜单信息
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<IResultModel> UpdateBindMenus(RoleBindMenusUpdateDto dto)
    {
        return _service.UpdateBindMenus(dto);
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
