using Application.DTOs;
using Application.IServices;
using Application.Mapping;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.IEFRepository;

namespace Application.Services
{
    public class RegionService : IRegionDTOService
    {
        private readonly IRegionRepository regionRepository;
        public RegionService(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }
        public void AddRegion(RegionDTO region)
        {
            var regionEF = region.MappingRegion();
            regionRepository.Add(regionEF);
        }

        public void DeleteRegion(int id)
        {
            var regionEF = regionRepository.GetBy(id);
            regionRepository.Delete(regionEF);
        }

        public IEnumerable<RegionDTO> GetAllRegion()
        {
            IEnumerable<Region> list = regionRepository.GetAll();
            return list.MappingRegionDtos();
        }

        public RegionDTO GetRegionByID(int id)
        {
            var regionEF = regionRepository.GetBy(id);
            return regionEF.MappingRegionDto();
        }

        public void UpdateRegion(RegionDTO region)
        {
            var regionEF = regionRepository.GetBy(region.IdRegion);
            region.MappingRegion(regionEF);
            regionRepository.Update(regionEF);
        }
    }
}
