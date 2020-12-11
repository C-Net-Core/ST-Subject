using Application.DTOs;
using System.Collections.Generic;

namespace Application.IServices
{
    public interface IAccountDTOService
    {
        IEnumerable<AccountDTO> GetAllAccount();   // trả về danh sách 
        AccountDTO GetAccountByID(int id);  // lấy đối tượng theo ID
        void AddAccount(AccountDTO account);
        void UpdateAccount(AccountDTO account);
        void DeleteAccount(int id);     // xóa theo ID
        IEnumerable<AccountDTO> GetAccountByStatus(string status);   // lọc danh sách tài khoản theo trạng thái
        IEnumerable<AccountDTO> SearchByUser(string user);  // tìm kiếm theo tên
        string getEmailUser(string user);
    }
}
