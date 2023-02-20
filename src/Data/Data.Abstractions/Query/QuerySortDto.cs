﻿using CRB.TPM.Data.Abstractions.Pagination;

namespace CRB.TPM.Data.Abstractions.Query;

/// <summary>
/// 查询排序
/// </summary>
public class QuerySortDto
{
    /// <summary>
    /// 字段
    /// </summary>
    public string Field { get; set; }

    /// <summary>
    /// 排序类型
    /// </summary>
    public SortType Type { get; set; }
}