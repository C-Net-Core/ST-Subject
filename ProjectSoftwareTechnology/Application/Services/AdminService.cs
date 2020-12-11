using Application.DTOs;
using Application.IServices;
using Application.Mapping;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.IEFRepository;
using System.Linq;

namespace Application.Services
{
    public class AdminService : IAdminDTOService
    {
        private readonly IAdminRepository adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }
        public void AddAdmin(AdminDTO admin)
        {
            var ad = admin.MappingAdmin();
            adminRepository.Add(ad);
        }

        public bool checkExistAdmin(string user)
        {
            var admin = adminRepository.GetAll()
                                        .Where(a => a.UID == user)
                                        .FirstOrDefault();
            if(admin != null)
            {
                return true;
            }
            return false;
        }

        public void DeleteAdmin(int id)
        {
            var admin = adminRepository.GetBy(id);
            adminRepository.Delete(admin);
        }

        public AdminDTO GetAdminByID(int id)
        {
            var admin = adminRepository.GetBy(id);
            return admin.MappingAdmintDto();
        }

        public IEnumerable<AdminDTO> GetAllAdmin()
        {
            IEnumerable<Admin> admins = adminRepository.GetAll();
            return admins.MappingAdminDtos();
        }

        public IEnumerable<AdminDTO> GetByStatus(string status)
        {
            IEnumerable<Admin> admins = adminRepository.GetAll().Where(a => a.Status.Contains(status)).ToList();
            return admins.MappingAdminDtos();
        }

        public int getIDAdmin(string user)
        {
            var admin = adminRepository.GetAll().Where(a => a.UID == user).FirstOrDefault();
            var getID = admin.ID;
            return getID;
        }

        public string getPermission(string user)
        {
            var admin = adminRepository.GetAll().Where(a => a.UID == user).FirstOrDefault();
            var getPermission = admin.Permission;
            return getPermission;
        }

        public IEnumerable<AdminDTO> SearchByUser(string user)
        {
            var admins = adminRepository.GetAll().Where(a => a.UID.Contains(user)).ToList();
            return admins.MappingAdminDtos();
        }

        public void UpdateAdmin(AdminDTO admin)
        {
            var ad = adminRepository.GetBy(admin.ID);
            admin.MappingAdmin(ad);
            adminRepository.Update(ad);
        }
    }
}
