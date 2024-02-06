using AutoMapper;
using GP.Common.DTO;
using GP.Models.Data;
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
                cfg.CreateMap<Flashcard, FlashcardDTO>().ReverseMap();
                //cfg.CreateMap<Account, AccountDTO>(); //.ForMember(account => account.CreatedAt, act => act.MapFrom(dto => dto.CreatedAt)).ReverseMap();
            });

            _mapper = config.CreateMapper();

        }

        // example
        public FlashcardDTO MapFlashcardToDTO(Flashcard flashcard)
        {
            return _mapper.Map<FlashcardDTO>(flashcard);
        }
        public Flashcard MapDTOToFlashcard(FlashcardDTO flashcardDTO)
        {
            return _mapper.Map<Flashcard>(flashcardDTO);
        }
        //public AccountDTO MapAccountToDTO(Account account)
        //{
        //    return _mapper.Map<AccountDTO>(account);
        //}
        //public Account MapDTOToAccount(AccountDTO accountDTO)
        //{
        //    return _mapper.Map<Account>(accountDTO);
        //}
        

    }
}
