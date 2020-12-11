using Application.DTOs;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Mapping
{
    public static class AccountProfile
    {
        public static AccountDTO MappingAccountDto(this Account account)
        {
            var accountDTO = new AccountDTO
            {
                ID = account.ID,
                UID = account.UID,
                PW = account.PW,
                Status = account.Status,
                Address = account.Address,
                Email = account.Email,
                DateActive = account.DateActive,
            };
            return accountDTO;
        }



        public static Account MappingAccount(this AccountDTO accountDTO)
        {
            var account = new Account
            {
                ID = accountDTO.ID,
                UID = accountDTO.UID,
                PW = accountDTO.PW,
                Status = accountDTO.Status,
                Address = accountDTO.Address,
                Email = accountDTO.Email,
                DateActive = accountDTO.DateActive,
            };
            return account;
        }


        public static void MappingAccount(this AccountDTO accountDTO, Account account)
        {
            account.ID = accountDTO.ID;
            account.UID = accountDTO.UID;
            account.PW = accountDTO.PW;
            account.Status = accountDTO.Status;
            account.Address = accountDTO.Address;
            account.Email = accountDTO.Email;
            account.DateActive = accountDTO.DateActive;
        }


        public static IEnumerable<AccountDTO> MappingAccountDtos(this IEnumerable<Account> accounts)
        {
            foreach (var account in accounts)
            {
                yield return account.MappingAccountDto();
            }
        }
    }
}
