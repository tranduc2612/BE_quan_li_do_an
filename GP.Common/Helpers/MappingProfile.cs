using AutoMapper;
using GP.Common.DTO;
using GP.Models.Model;
using AutoMapper;
using GP.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.Helpers
{
    public class MappingProfile
    {
        private readonly IMapper _mapper;
        public MappingProfile()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // example
                cfg.CreateMap<Account, AccountDTO>().ReverseMap(); //.ForMember(account => account.CreatedAt, act => act.MapFrom(dto => dto.CreatedAt)).ReverseMap();

            });

            _mapper = config.CreateMapper();

        }
        public AccountDTO MapAccountToDTO(Account account)
        {
            return _mapper.Map<AccountDTO>(account);
        }
        public Account MapDTOToAccount(AccountDTO accountDTO)
        {
            return _mapper.Map<Account>(accountDTO);
        }
 

    }
}