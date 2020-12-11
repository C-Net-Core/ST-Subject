using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mapping
{
    public static class RegionProfile
    {

        public static RegionDTO MappingRegionDto(this Region region)
        {
            var regionDTO = new RegionDTO
            {
                IdRegion = region.IdRegion,
                NameArea = region.NameArea,
                FeeShip = region.FeeShip
            };
            return regionDTO;
        }



        public static Region MappingRegion(this RegionDTO regionDTO)
        {
            var region = new Region
            {
                IdRegion = regionDTO.IdRegion,
                NameArea = regionDTO.NameArea,
                FeeShip = regionDTO.FeeShip
            };
            return region;
        }



        public static void MappingRegion(this RegionDTO regionDTO, Region region)
        {
            region.IdRegion = regionDTO.IdRegion;
            region.NameArea = regionDTO.NameArea;
            region.FeeShip = regionDTO.FeeShip;
        }



        public static IEnumerable<RegionDTO> MappingRegionDtos(this IEnumerable<Region> regions)
        {
            foreach (var region in regions)
            {
                yield return region.MappingRegionDto();
            }
        }
    }
}
