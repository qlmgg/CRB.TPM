﻿using System;

namespace CRB.TPM.Auth.Abstractions.Annotations;

/// <summary>
/// 禁用审计功能
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
public class DisableAuditAttribute : Attribute
{
}
