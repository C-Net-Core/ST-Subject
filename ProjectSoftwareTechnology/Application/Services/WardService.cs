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
    public class WardService : IWardDTOService
    {
        private readonly IWardRepository wardRepository;
        public WardService(IWardRepository wardRepository)
        {
            this.wardRepository = wardRepository;
        }
        public void AddWard(WardDTO ward)
        {
            var wardEF = ward.MappingWard();
            wardRepository.Add(wardEF);
        }

        public void DeleteWard(int id)
        {
            var wardEF = wardRepository.GetBy(id);
            wardRepository.Delete(wardEF);
        }

        public IEnumerable<WardDTO> GetAllWard()
        {
            IEnumerable<Ward> list = wardRepository.GetAll();
            return list.MappingWardDtos();
        }

        public WardDTO GetWardByID(int id)
        {
            var wardEF = wardRepository.GetBy(id);
            return wardEF.MappingWardDto();
        }

        public void UpdateWard(WardDTO ward)
        {
            var wardEF = wardRepository.GetBy(ward.IdWard);
            ward.MappingWard(wardEF);
            wardRepository.Update(wardEF);
        }
    }
}
