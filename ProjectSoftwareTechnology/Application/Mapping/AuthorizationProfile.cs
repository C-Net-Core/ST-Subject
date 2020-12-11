using Application.DTOs;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Mapping
{
    public static class AuthorizationProfile
    {
        public static AuthorizationDTO MappingDto(this Authorization f1)
        {
            var f = new AuthorizationDTO
            {
                IDAdmin = f1.IDAdmin,
                IDCN = f1.IDCN,
            };
            return f;
        }

        public static Authorization Mapping(this AuthorizationDTO f)
        {
            var f1 = new Authorization
            {
                IDAdmin = f.IDAdmin,
                IDCN = f.IDCN
            };
            return f1;
        }

        public static void Mapping(this AuthorizationDTO f, Authorization f1)
        {
            f1.IDAdmin = f.IDAdmin;
            f1.IDCN = f.IDCN;
        }

        public static IEnumerable<AuthorizationDTO> MappingDtos(this IEnumerable<Authorization> f1s)
        {
            foreach (var i in f1s)
            {
                yield return i.MappingDto();
            }
        }

        public static IEnumerable<Authorization> Mappings(this IEnumerable<AuthorizationDTO> fs)
        {
            foreach (var i in fs)
            {
                yield return i.Mapping();
            }
        }
    }
}
