using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Models.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.IService
{
    public interface IAccountService
    {
        public void Register(AccountDTO accountDTO);

        public bool CheckUserExist(AccountDTO accountDTO, out string message);

        public bool VerifyLoginInfo(string username, string password, out string message);

        public AccountDTO CreateToken(string username);

        public AccountDTO GetCurrentAccount();

        public string GetCurrentUsername();

        public bool CheckValidRefreshToken(string refreshToken, out string message);
    }
}
