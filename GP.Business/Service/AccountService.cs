using Azure;
using Azure.Identity;
using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.DAL.IRepository;
using GP.Models.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly AuthHelper _authHelper;
        private readonly MappingProfile _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService( IAccountRepository accountRepository, AuthHelper authHelper, IHttpContextAccessor httpContextAccessor, MappingProfile mapper)
        {
            _accountRepository = accountRepository;
            _authHelper = authHelper;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Kiểm tra thông tin đăng ký đã tồn tại chưa
        /// </summary>
        /// <param name="accountDTO"></param>
        /// <returns></returns>
        public bool CheckUserExist(AccountDTO accountDTO, out string message)
        {
            message = string.Empty;
            Account account = _accountRepository.GetByEmail(accountDTO.Email);
            if (account != null)
            {
                message = "Email này đã được tài khoản khác sử dụng";
                return true;
            }

            account = _accountRepository.GetByUsername(accountDTO.UserName);
            if (account != null)
            {
                message = "Username này đã được tài khoản khác sử dụng";
                return true;
            }

            return false;
        }

        public AccountDTO CreateToken(string username)
        {
            Account account = _accountRepository.GetByUsernameOrEmail(username);
            AccountDTO accountDTO = _mapper.MapAccountToDTO(account);

            string token = _authHelper.CreateToken(account);

            accountDTO.Token = token;


            // Generate and set refresh token (như hàm GenAndSetRefreshToken bên dưới nhưng ko set vào cookies)
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            // cập nhật thông tin user 
            if (account != null)
            {
                account.RefreshToken = refreshToken.Token;
                account.TokenCreated = refreshToken.Created;
                account.TokenExpires = refreshToken.Expires;

                _accountRepository.UpdateAccount(account);
            }

            accountDTO.RefreshToken = refreshToken.Token;

            return accountDTO;
        }

        // Giống với bên AuthHelper.cs
        public string GetCurrentUsername()
        {
            var username = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                username = _httpContextAccessor.HttpContext.User.FindFirstValue("username");
            }
            return username;
        }

        public AccountDTO GetCurrentAccount()
        {
            string username = GetCurrentUsername();
            
            AccountDTO account = _mapper.MapAccountToDTO(_accountRepository.GetByUsername(username));

            return account;
        }

        public bool CheckValidRefreshToken(string refreshToken, out string message)
        {
            message = string.Empty;
            AccountDTO account = GetCurrentAccount();

            if (account.RefreshToken == null || !account.RefreshToken.Equals(refreshToken))
            {
                message = "Invalid Refresh Token.";
                return false;
            }
            else if (account.TokenExpires < DateTime.Now)
            {
                message = "Toke expired.";
                return false;
            }

            return true;
        }

        public void Register(AccountDTO accountDTO)
        {
            Account account = _mapper.MapDTOToAccount(accountDTO);


            AuthHelper.CreatePassHash(accountDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
            account.Password = passwordHash;
            account.PasswordSalt = passwordSalt;

            _accountRepository.Create(account);
        }
    
        public bool VerifyLoginInfo(string username, string password, out string message)
        {
            Account account = _accountRepository.GetByUsernameOrEmail(username);

            if (account == null)
            {
                message = "Tài khoản không tồn tại";
                return false;
            }

            if (!AuthHelper.VerifyPasswordHash(password, account.Password, account.PasswordSalt))
            {
                message = "Sai mật khẩu";
                return false;
            }
            message = "Thông tin đăng nhập đúng";
            return true;
        }
    }
}
