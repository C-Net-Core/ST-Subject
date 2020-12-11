using Application.DTOs;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Mapping
{
    public static class AdminProfile
    {
        public static AdminDTO MappingAdmintDto(this Admin admin)
        {
            var adminDTO = new AdminDTO
            {
                ID = admin.ID,
                UID = admin.UID,
                PW = admin.PW,
                Permission = admin.Permission,
                DateActive = admin.DateActive,
                Status = admin.Status,
            };
            return adminDTO;
        }



        public static Admin MappingAdmin(this AdminDTO adminDTO)
        {
            var admin = new Admin
            {
                ID = adminDTO.ID,
                UID = adminDTO.UID,
                PW = adminDTO.PW,
                Permission = adminDTO.Permission,
                DateActive = adminDTO.DateActive,
                Status = adminDTO.Status,
            };
            return admin;
        }


        public static void MappingAdmin(this AdminDTO adminDTO, Admin admin)
        {
            admin.ID = adminDTO.ID;
            admin.UID = adminDTO.UID;
            admin.PW = adminDTO.PW;
            admin.Permission = adminDTO.Permission;
            admin.DateActive = adminDTO.DateActive;
            admin.Status = adminDTO.Status;
        }


        public static IEnumerable<AdminDTO> MappingAdminDtos(this IEnumerable<Admin> admins)
        {
            foreach (var admin in admins)
            {
                yield return admin.MappingAdmintDto();
            }
        }
    }
}
