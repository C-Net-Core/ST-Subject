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
    public class ProvinceService : IProvinceDTOService
    {
        private readonly IProvinceRepository provinceRepository;
        public ProvinceService(IProvinceRepository provinceRepository)
        {
            this.provinceRepository = provinceRepository;
        }
        public void AddProvince(ProvinceDTO province)
        {
            var provinceEF = province.MappingProvince();
            provinceRepository.Add(provinceEF);
        }

        public void DeleteProvince(int id)
        {
            var provinceEF = provinceRepository.GetBy(id);
            provinceRepository.Delete(provinceEF);
        }

        public IEnumerable<ProvinceDTO> GetAllProvince()
        {
            IEnumerable<Province> provinces = provinceRepository.GetAll();
            return provinces.MappingProvinceDtos();
        }

        public ProvinceDTO GetProvinceByID(int id)
        {
            var provinceEF = provinceRepository.GetBy(id);
            return provinceEF.MappingProvinceDto();
        }

        public void UpdateProvince(ProvinceDTO province)
        {
            var provinceEF = provinceRepository.GetBy(province.IdProvince);
            province.MappingProvince(provinceEF);
            provinceRepository.Update(provinceEF);
        }
    }
}
