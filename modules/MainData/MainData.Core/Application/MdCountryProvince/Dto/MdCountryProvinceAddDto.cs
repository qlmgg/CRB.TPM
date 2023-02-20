﻿using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

using CRB.TPM.Mod.MainData.Core.Domain.MdCountryProvince;
using CRB.TPM.Mod.MainData.Core.Domain.MdCityDistrict;

namespace CRB.TPM.Mod.MainData.Core.Application.MdCountryProvince.Dto;

/// <summary>
/// 国家省份 D_CountryProvince添加模型
/// </summary>
[ObjectMap(typeof(MdCountryProvinceEntity))]
public class MdCountryProvinceAddDto
{
    /// <summary>
    ///  
    /// </summary>
    public string CountryCD { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string CountryNm { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ProvinceCD { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ProvinceNm { get; set; }

}