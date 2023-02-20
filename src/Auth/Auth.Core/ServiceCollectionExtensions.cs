﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using CRB.TPM.Auth.Abstractions;
using CRB.TPM.Auth.Abstractions.Options;
using CRB.TPM.Auth.Core;
using CRB.TPM.Data.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加CRB.TPM身份认证核心功能
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration">配置属性</param>
    /// <returns></returns>
    public static CRBTPMAuthBuilder AddCRBTPMAuth(this IServiceCollection services, IConfiguration configuration)
    {
        //添加身份认证配置项
        services.Configure<AuthOptions>(configuration.GetSection("CRB.TPM:Auth"));

        //添加http上下文访问器，用于获取认证信息
        services.AddHttpContextAccessor();
        //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

        //尝试添加账户信息
        services.TryAddSingleton<IAccount, Account>();

        //添加权限解析器
        services.TryAddSingleton<IPermissionResolver, PermissionResolver>();

        //尝试添加租户解析器
        services.TryAddSingleton<ITenantResolver, DefaultTenantResolver>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("CRB.TPM", policy => policy.Requirements.Add(new PermissionRequirement()));
        });

        //自定义权限验证处理器
        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();

        //添加数据访问的账户解析器实现
        services.TryAddSingleton<IOperatorResolver, OperatorResolver>();

        //添加默认权限验证处理器
        services.TryAddSingleton<IPermissionValidateHandler, DefaultPermissionValidateHandler>();

        //添加平台转换器
        services.TryAddSingleton<IPlatformProvider, DefaultPlatformProvider>();

        var builder = new CRBTPMAuthBuilder(services, configuration);

        return builder;
    }
}