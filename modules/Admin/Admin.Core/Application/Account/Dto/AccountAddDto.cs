﻿using System.ComponentModel.DataAnnotations;
using CRB.TPM.Mod.Admin.Core.Domain.Account;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Auth.Abstractions;
using System.Collections.Generic;
using System;
using CRB.TPM.Data.Abstractions.Annotations;

namespace CRB.TPM.Mod.Admin.Core.Application.Account.Dto;

/// <summary>
/// 账户新增模型
/// </summary>
[ObjectMap(typeof(AccountEntity))]
public class AccountAddDto
{
    /// <summary>
    /// 类型
    /// </summary>
    public AccountType Type { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    [Required(ErrorMessage = "请输入用户名")]
    public string Username { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// 姓名或名称
    /// </summary>
    [Required(ErrorMessage = "请输入姓名或名称")]
    public string Name { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 账户是否锁定(锁定后不允许在账户管理中修改)
    /// </summary>
    public bool IsLock { get; set; }

    /// <summary>
    /// 账户状态
    /// </summary>
    public AccountStatus Status { get; set; } = AccountStatus.Register;

    /// <summary>
    /// 绑定角色列表
    /// </summary>
    [Required(ErrorMessage = "请选择角色")]
    [NotMappingColumn]
    public IList<Guid> Roles { get; set; }
}