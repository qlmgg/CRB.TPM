﻿using CRB.TPM.Mod.AuditInfo.Core.Domain.AuditInfo;
using CRB.TPM.Utils.Annotations;
using System.ComponentModel.DataAnnotations;

namespace CRB.TPM.Mod.AuditInfo.Core.Application.AuditInfo.Dto;


[ObjectMap(typeof(AuditInfoEntity), true)]
public class AuditInfoUpdateDto : AuditInfoAddDto
{

    [Required(ErrorMessage = "请选择要修改的项目")]
    public int Id { get; set; }
}
