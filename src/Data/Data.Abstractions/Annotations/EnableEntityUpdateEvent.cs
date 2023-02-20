﻿using System;

namespace CRB.TPM.Data.Abstractions.Annotations
{
    /// <summary>
    /// 启用实体更新事件
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EnableEntityUpdateEvent : Attribute
    {
    }
}
