using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Models.Data;
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

        public string CreateToken(string username);

        public void GenAndSetRefreshToken(HttpResponse response, string username = null);

        public Account GetCurrentAccount();

        public string GetCurrentUsername();

        public bool CheckValidRefreshToken(string refreshToken, out string message);
    }
}
