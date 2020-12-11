using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IServices
{
    public interface IAuthorizationDTOService
    {
        IEnumerable<AuthorizationDTO> GetAll();   // trả về danh sách 
        AuthorizationDTO GetByID(int id);  // lấy đối tượng theo ID
        void Create(AuthorizationDTO authorizationDTO);
        void Update(AuthorizationDTO authorizationDTO);
        void Remove(int id);     // xóa theo ID
        void Remove(AuthorizationDTO authorizationDTO);
        void RemoveByIDCN(AuthorizationDTO authorizationDTO);
        Boolean GetAuthor(int idAdmin,int idCN);  // lấy đối tượng theo ID
    }
}
