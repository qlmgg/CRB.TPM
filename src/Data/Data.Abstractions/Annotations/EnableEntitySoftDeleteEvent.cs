﻿using System;

namespace CRB.TPM.Data.Abstractions.Annotations
{
    /// <summary>
    /// 启用实体软删除事件
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EnableEntitySoftDeleteEvent : Attribute
    {
    }
}
