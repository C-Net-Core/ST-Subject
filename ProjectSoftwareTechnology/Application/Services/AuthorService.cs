using Application.DTOs;
using Application.IServices;
using Application.Mapping;
using Domain.Entities;
using Domain.Repositories.IEFRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class AuthorService : IAuthorizationDTOService
    {
        private readonly IAuthorizationRepository authorizationRepository;
        public AuthorService(IAuthorizationRepository authorizationRepository)
        {
            this.authorizationRepository = authorizationRepository;
        }
        public void Create(AuthorizationDTO f1)
        {
            var f = f1.Mapping();
            authorizationRepository.Add(f);
        }

        public void Remove(int id)
        {
            var f = authorizationRepository.GetBy(id);
            authorizationRepository.Delete(f);
        }

        public AuthorizationDTO GetByID(int id)
        {
            var f1 = authorizationRepository.GetBy(id);
            return f1.MappingDto();
        }

        public IEnumerable<AuthorizationDTO> GetAll()
        {
            IEnumerable<Authorization> f1 = authorizationRepository.GetAll();
            return f1.MappingDtos();
        }

        public void Update(AuthorizationDTO f)
        {
            var f1 = authorizationRepository.GetAll()
                                            .Where(p => p.IDAdmin == f.IDAdmin && p.IDCN == f.IDCN)
                                            .FirstOrDefault();

            f.Mapping(f1);

            authorizationRepository.Update(f1);
        }

        public Boolean GetAuthor(int idAdmin, int idCN)
        {
            var f1 = authorizationRepository.GetAll()
                                            .Where(a => a.IDAdmin == idAdmin && a.IDCN == idCN)
                                            .FirstOrDefault();
            if(f1 == null)
            {
                return false;
            }
            return true;
        }

        public void Remove(AuthorizationDTO authorizationDTO)
        {
            var authors = authorizationRepository.GetAll();
            foreach(var item in authors)
            {
                if(item.IDAdmin == authorizationDTO.IDAdmin)
                {
                    authorizationRepository.Delete(item);
                }
            }                                    
        }

        public void RemoveByIDCN(AuthorizationDTO authorizationDTO)
        {
            var authors = authorizationRepository.GetAll();
            foreach (var item in authors)
            {
                if (item.IDCN == authorizationDTO.IDCN)
                {
                    authorizationRepository.Delete(item);
                }
            }
        }
    }
}
