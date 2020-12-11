using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mapping
{
    public static class WardProfile
    {

        public static WardDTO MappingWardDto(this Ward ward)
        {
            var wardDTO = new WardDTO
            {
                NameWard = ward.NameWard,
                IdProvince = ward.IdProvince
            };
            return wardDTO;
        }


        public static Ward MappingWard(this WardDTO wardDTO)
        {
            var ward = new Ward
            {
                NameWard = wardDTO.NameWard,
                IdProvince = wardDTO.IdProvince
            };
            return ward;
        }



        public static void MappingWard(this WardDTO wardDto, Ward ward)
        {
            ward.NameWard = wardDto.NameWard;
            ward.IdProvince = wardDto.IdProvince;
        }



        public static IEnumerable<WardDTO> MappingWardDtos(this IEnumerable<Ward> wards)
        {
            foreach (var ward in wards)
            {
                yield return ward.MappingWardDto();
            }
        }
    }
}
