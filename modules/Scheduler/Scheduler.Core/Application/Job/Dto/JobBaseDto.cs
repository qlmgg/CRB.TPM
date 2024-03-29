﻿using System;
using CRB.TPM.Mod.Scheduler.Core.Application.Job.Dto;
using CRB.TPM.Mod.Scheduler.Core.Domain.Job;
using CRB.TPM.Utils.Annotations;
using System.ComponentModel.DataAnnotations;

namespace CRB.TPM.Mod.Scheduler.Core.Application.Job.Dto;

public class JobBaseDto
{
    /// <summary>
    /// 所属模块
    /// </summary>
    [Required(ErrorMessage = "请选择模块")]
    public string ModuleCode { get; set; }

    /// <summary>
    /// 任务组
    /// </summary>
    [Required(ErrorMessage = "请选择任务组")]
    public string Group { get; set; }

    /// <summary>
    /// 任务名称
    /// </summary>
    [Required(ErrorMessage = "请输入任务名称")]
    public string Name { get; set; }

    /// <summary>
    /// 任务编码
    /// </summary>
    [Required(ErrorMessage = "请输入任务编码")]
    public string Code { get; set; }

    /// <summary>
    /// 触发类型
    /// </summary>
    public TriggerType TriggerType { get; set; }

    /// <summary>
    /// 简单触发器时间间隔(秒)
    /// </summary>
    public int Interval { get; set; }

    /// <summary>
    /// 简单触发器重复次数，0表示无限
    /// </summary>
    public int RepeatCount { get; set; }

    /// <summary>
    /// Cron表达式
    /// </summary>
    public string Cron { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    [Required(ErrorMessage = "请选择开始日期")]
    public DateTime BeginDate { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    [Required(ErrorMessage = "请选择结束日期")]
    public DateTime EndDate { get; set; }
}
