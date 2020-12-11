using Application.DTOs;
using System;
using System.Collections.Generic;

namespace Application.IServices
{
    public interface IAdminDTOService
    {
        IEnumerable<AdminDTO> GetAllAdmin();   // trả về danh sách 
        AdminDTO GetAdminByID(int id);  // lấy đối tượng theo ID
        void AddAdmin(AdminDTO admin);
        void UpdateAdmin(AdminDTO admin);
        void DeleteAdmin(int id);     // xóa theo ID
        IEnumerable<AdminDTO> GetByStatus(string status);   // lọc danh sách tài khoản theo trạng thái
        IEnumerable<AdminDTO> SearchByUser(string user);  // tìm kiếm theo tên
        string getPermission(string user);
        int getIDAdmin(string user);
        Boolean checkExistAdmin(string user);
    }
}
