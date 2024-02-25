using GP.DAL.IRepository;
using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ManagementGraduationProjectContext _dbContext;
        public AccountRepository(ManagementGraduationProjectContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Account Create(Account account)
        {
            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
            return account;
        }


        public Account GetByEmail(string email)
        {
            return _dbContext.Accounts.SingleOrDefault(account => account.Email == email);
        }

        public Account GetByUsername(string username)
        {
            return _dbContext.Accounts.SingleOrDefault(account => account.UserName == username);
        }

        public Account GetByUsernameOrEmail(string username)
        {
            return _dbContext.Accounts.SingleOrDefault(account => account.UserName == username || account.Email == username);
        }

        public void UpdateAccount(Account account)
        {
            _dbContext.Accounts.Update(account);
            _dbContext.SaveChanges();
        }
    }
}
