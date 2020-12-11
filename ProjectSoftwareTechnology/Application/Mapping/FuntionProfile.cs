using Application.DTOs;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Mapping
{
    public static class FuntionProfile
    {
        public static FunctionDTO MappingDto(this Function f1)
        {
            var f = new FunctionDTO
            {
                IDCN = f1.IDCN,
                Name = f1.Name,
                NameMethod = f1.NameMethod,
            };
            return f;
        }



        public static Function Mapping(this FunctionDTO f)
        {
            var f1 = new Function
            {
                IDCN = f.IDCN,
                Name = f.Name,
                NameMethod = f.NameMethod,
            };
            return f1;
        }


        public static void Mapping(this FunctionDTO f, Function f1)
        {
            f1.IDCN = f.IDCN;
            f1.Name = f.Name;
            f1.NameMethod = f.NameMethod;
        }


        public static IEnumerable<FunctionDTO> MappingDtos(this IEnumerable<Function> f1s)
        {
            foreach (var i in f1s)
            {
                yield return i.MappingDto();
            }
        }
    }
}
