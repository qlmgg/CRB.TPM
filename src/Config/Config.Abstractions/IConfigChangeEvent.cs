﻿namespace CRB.TPM.Config.Abstractions;

/// <summary>
/// 配置变更事件
/// </summary>
public interface IConfigChangeEvent<in TConfig> where TConfig : IConfig
{
    /// <summary>
    /// 变更事件
    /// </summary>
    /// <param name="newConfig"></param>
    /// <param name="oldConfig"></param>
    void OnChanged(TConfig newConfig, TConfig oldConfig);
}
