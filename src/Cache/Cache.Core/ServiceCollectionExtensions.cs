﻿using CRB.TPM.Cache.Abstractions;
using CRB.TPM.Cache.Core;


namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加缓存
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static ICacheBuilder AddCache(this IServiceCollection services)
    {
        services.AddSingleton<ICacheProvider, MemoryCacheHandler>();

        return new CacheBuilder(services);
    }
}