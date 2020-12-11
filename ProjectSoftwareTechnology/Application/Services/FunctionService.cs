using Application.DTOs;
using Application.IServices;
using Application.Mapping;
using Domain.Entities;
using Domain.Repositories.IEFRepository;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class FunctionService : IFunctionDTOService
    {
        private readonly IFunctionRepository functionRepository;
        public FunctionService(IFunctionRepository functionRepository)
        {
            this.functionRepository = functionRepository;
        }
        public void Create(FunctionDTO f1)
        {
            var f = f1.Mapping();
            functionRepository.Add(f);
        }

        public void Remove(int id)
        {
            var f = functionRepository.GetBy(id);
            functionRepository.Delete(f);
        }

        public FunctionDTO GetByID(int id)
        {
            var f1= functionRepository.GetBy(id);
            return f1.MappingDto();
        }

        public IEnumerable<FunctionDTO> GetAll()
        {
            IEnumerable<Function> f1 = functionRepository.GetAll();
            return f1.MappingDtos();
        }

        public void Update(FunctionDTO f)
        {
            var f1 = functionRepository.GetBy(f.IDCN);

            f.Mapping(f1);

            functionRepository.Update(f1);
        }

        public bool Is_Exist_Function_ByName(string name)
        {
            var function = functionRepository.GetAll().Where(f => f.Name == name).FirstOrDefault();
            if (function != null) return true;
            return false;
        }
    }
}
