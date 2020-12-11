using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mapping
{
    public static class ProvinceProfile
    {

        public static ProvinceDTO MappingProvinceDto(this Province province)
        {
            var provinceDTO = new ProvinceDTO
            {
                IdProvince = province.IdProvince,
                IdReion = province.IdReion,
                NameProvince = province.NameProvince
            };
            return provinceDTO;
        }



        public static Province MappingProvince(this ProvinceDTO provinceDTO)
        {
            var province = new Province
            {
                IdReion = provinceDTO.IdReion,
                NameProvince = provinceDTO.NameProvince,
                IdProvince = provinceDTO.IdProvince
            };
            return province;
        }


        public static void MappingProvince(this ProvinceDTO provinceDTO, Province province)
        {
            province.IdReion = provinceDTO.IdReion;
            province.NameProvince = provinceDTO.NameProvince;
            province.IdProvince = provinceDTO.IdProvince;
        }



        public static IEnumerable<ProvinceDTO> MappingProvinceDtos(this IEnumerable<Province> provinces)
        {
            foreach (var province in provinces)
            {
                yield return province.MappingProvinceDto();
            }
        }
    }
}
