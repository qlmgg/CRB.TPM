﻿using CRB.TPM.Cache.Redis;
using CRB.TPM.Config.Abstractions;
using CRB.TPM.Module.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CRB.TPM.Config.Core.Provider;

/// <summary>
/// 用于Config配置的Redi 存储
/// </summary>
public class RedisConfigProvider : IConfigProvider
{
    private const string CACHE_KEY = "CONFIG";
    private readonly RedisHelper _redisHelper;
    private readonly IConfigCollection _configCollection;
    private readonly IRedisSerializer _redisSerializer;
    private readonly IConfigStorageProvider _storageProvider;
    private readonly IConfiguration _cfg;

    /// <summary>
    /// 表示含有实例的配置描述符集合
    /// </summary>
    private readonly Dictionary<RuntimeTypeHandle, IConfig> _configs = new();

    public RedisConfigProvider(RedisHelper redisHelper, IConfigCollection configCollection, IRedisSerializer redisSerializer, IConfigStorageProvider storageProvider, IConfiguration cfg)
    {
        _redisHelper = redisHelper;
        _configCollection = configCollection;
        _redisSerializer = redisSerializer;
        _storageProvider = storageProvider;
        _cfg = cfg;
    }

    public IConfig Get(Type implementType)
    {
        var descriptor = _configCollection.FirstOrDefault(m => m.ImplementType == implementType);
        if (descriptor == null)
            throw new NotImplementedException("没有找到配置类型");

        return Get(descriptor);
    }

    public IConfig Get(string code, ConfigType type)
    {
        var descriptor = _configCollection.FirstOrDefault(m => m.Code.EqualsIgnoreCase(code) && m.Type == type);
        if (descriptor == null)
            throw new NotImplementedException("没有找到配置类型");

        return Get(descriptor);
    }


    public TConfig Get<TConfig>() where TConfig : IConfig, new()
    {
        var item = _configs.FirstOrDefault(m => m.Key.Value == typeof(TConfig).TypeHandle.Value);
        return (TConfig)item.Value;
    }


    public TConfig GetFor<TConfig>() where TConfig : IConfig, new()
    {
        var descriptor = _configCollection.FirstOrDefault(m => m.ImplementType == typeof(TConfig));
        if (descriptor == null)
            throw new NotImplementedException("没有找到配置类型");

        return (TConfig)Get(descriptor);
    }


    public bool Set(ConfigType type, string code, string json)
    {
        var descriptor = _configCollection.FirstOrDefault(m => m.Code.EqualsIgnoreCase(code) && m.Type == type);
        if (descriptor == null)
            throw new NotImplementedException("没有找到配置类型");

        //持久化
        if (_storageProvider.SaveJson(type, code, json).GetAwaiter().GetResult())
        {
            var key = _redisHelper.GetKey($"{CACHE_KEY}:{descriptor.Type.ToString().ToUpper()}:{descriptor.Code.ToUpper()}");
            var config = JsonConvert.DeserializeObject(json, descriptor.ImplementType);
            _redisHelper.Db.StringSetAsync(key, _redisSerializer.Serialize(config)).GetAwaiter().GetResult();
            return true;
        }

        return false;
    }

    private IConfig Get(ConfigDescriptor descriptor)
    {
        var key = _redisHelper.GetKey($"{CACHE_KEY}:{descriptor.Type.ToString().ToUpper()}:{descriptor.Code.ToUpper()}");
        var cache = _redisHelper.Db.StringGetAsync(key).Result;
        if (cache.HasValue)
        {
            return (IConfig)_redisSerializer.Deserialize(cache, descriptor.ImplementType);
        }

        IConfig config;
        var json = _storageProvider.GetJson(descriptor.Type, descriptor.Code).Result;
        if (json.IsNull())
        {
            config = (IConfig)Activator.CreateInstance(descriptor.ImplementType);
            if (descriptor.Type == ConfigType.Library)
            {
                var section = _cfg.GetSection(descriptor.Code);
                if (section != null)
                {
                    section.Bind(config);
                }
            }
        }
        else
            config = (IConfig)JsonConvert.DeserializeObject(json, descriptor.ImplementType);

        _redisHelper.Db.StringSetAsync(key, _redisSerializer.Serialize(config)).GetAwaiter().GetResult();

        return config;
    }


    /// <summary>
    /// 添加模块配置
    /// </summary>
    /// <param name="modules"></param>
    public void AddModuleConfig(IModuleCollection modules)
    {
        foreach (var module in modules)
        {
            var configType = module.LayerAssemblies.Core.GetTypes().FirstOrDefault(m => typeof(IConfig).IsImplementType(m));
            if (configType != null)
            {
                //模块配置节点
                var instance = (IConfig)Activator.CreateInstance(configType)!;
                _cfg.GetSection($"CRB.TPM:Modules:{module.Code}:Config").Bind(instance);
                if (!_configs.Select(s => s.Key).Contains(configType.TypeHandle))
                    _configs.Add(configType.TypeHandle, instance);
            }
        }
    }
}

