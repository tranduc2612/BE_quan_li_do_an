using GP.Common.DTO;
using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.IRepository
{
    public interface IAccountRepository
    {
        public Account Create(Account account);
        public Account GetByUsername(string username);
        public Account GetByEmail(string email);

        public Account GetByUsernameOrEmail(string username);

        public void UpdateAccount(Account account);
    }
}
