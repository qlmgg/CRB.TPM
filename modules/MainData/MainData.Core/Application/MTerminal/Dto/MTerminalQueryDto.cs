﻿using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto;
using System;
using System.Collections.Generic;

namespace CRB.TPM.Mod.MainData.Core.Application.MTerminal.Dto;

/// <summary>
/// 终端信息查询模型
/// </summary>
public class MTerminalQueryDto : GlobalOrgFilterDto
{
    //private IList<Guid> headOfficeIds;
    //private IList<Guid> divisionIds;
    //private IList<Guid> marketingIds;
    //private IList<Guid> dutyregionIds;
    //private IList<Guid> departmentIds;
    //private IList<Guid> stationIds;
    //private IList<Guid> distributorIds;

    /// <summary>
    /// 终端编码/名称
    /// </summary>
    public string Name { get; set; }

    ///// <summary>
    ///// 客户id
    ///// </summary>
    //public Guid DistributorId { get; set; }
    ///// <summary>
    ///// 雪花ids
    ///// </summary>
    //public IList<Guid> HeadOfficeIds { get => headOfficeIds.RemoveGuidEmpty(); set => headOfficeIds = value; }
    ///// <summary>
    ///// 事业部ids
    ///// </summary>
    //public IList<Guid> DivisionIds { get => divisionIds.RemoveGuidEmpty(); set => divisionIds = value; }
    ///// <summary>
    ///// 营销中心ids
    ///// </summary>
    //public IList<Guid> MarketingIds { get => marketingIds.RemoveGuidEmpty(); set => marketingIds = value; }
    ///// <summary>
    ///// 大区ids
    ///// </summary>
    //public IList<Guid> DutyregionIds { get => dutyregionIds.RemoveGuidEmpty(); set => dutyregionIds = value; }
    ///// <summary>
    ///// 业务部ids
    ///// </summary>
    //public IList<Guid> DepartmentIds { get => departmentIds.RemoveGuidEmpty(); set => departmentIds = value; }
    ///// <summary>
    ///// 工作站ids
    ///// </summary>
    //public IList<Guid> StationIds { get => stationIds.RemoveGuidEmpty(); set => stationIds = value; }
    ///// <summary>
    ///// 经销商ids
    ///// </summary>
    //public IList<Guid> DistributorIds { get => distributorIds.RemoveGuidEmpty(); set => distributorIds = value; }
}