using Application.DTOs;
using System;
using System.Collections.Generic;

namespace Application.IServices
{
    public interface IFunctionDTOService
    {
        IEnumerable<FunctionDTO> GetAll();   // trả về danh sách 
        FunctionDTO GetByID(int id);  // lấy đối tượng theo ID
        void Create(FunctionDTO functionDTO);
        void Update(FunctionDTO functionDTO);
        void Remove(int id);     // xóa theo ID
        Boolean Is_Exist_Function_ByName(string name);
    }
}
