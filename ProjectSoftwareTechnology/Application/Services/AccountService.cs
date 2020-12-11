using Application.DTOs;
using Application.IServices;
using Application.Mapping;
using Domain.Entities;
using Domain.Repositories.IEFRepository;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class AccountService : IAccountDTOService
    {
        private readonly IAccountRepository accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }
        public void AddAccount(AccountDTO account)
        {
            var acc = account.MappingAccount();
            accountRepository.Add(acc);
        }

        public void DeleteAccount(int id)
        {
            var account = accountRepository.GetBy(id);
            accountRepository.Delete(account);
        }

        public AccountDTO GetAccountByID(int id)
        {
            var account = accountRepository.GetBy(id);
            return account.MappingAccountDto();
        }

        public IEnumerable<AccountDTO> GetAccountByStatus(string status)
        {
            IEnumerable<Account> accounts = accountRepository.GetAll()
                                                                .Where(a=>a.Status.Contains(status))
                                                                .ToList();
            return accounts.MappingAccountDtos();
        }


        public IEnumerable<AccountDTO> GetAllAccount()
        {
            IEnumerable<Account> accounts = accountRepository.GetAll();
            return accounts.MappingAccountDtos();
        }

        public string getEmailUser(string user)
        {
            var acc = accountRepository.GetAll().Where(a => a.UID == user).FirstOrDefault();
            var getEmail = acc.Email;
            return getEmail;
        }

        public IEnumerable<AccountDTO> SearchByUser(string user)
        {
            var accounts = accountRepository.GetAll().Where(a => a.UID.Contains(user)).ToList();
            return accounts.MappingAccountDtos();
        }

        public void UpdateAccount(AccountDTO account)
        {
            var acc = accountRepository.GetBy(account.ID);

            account.MappingAccount(acc);

            accountRepository.Update(acc);
        }
    }
}
