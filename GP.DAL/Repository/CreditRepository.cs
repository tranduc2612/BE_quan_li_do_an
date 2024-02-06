using GP.Common.Models;
using GP.DAL.IRepository;
using GP.Models.Data;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.Repository
{
    public class CreditRepository : ICreditRepository
    {
        private readonly QuizletDbContext _dbContext;
        public CreditRepository(QuizletDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Credit CreateCredit()
        {
            throw new NotImplementedException();
        }

        public PaginatedResultBase<Credit> GetListCreditByFilter(SearchBase searchBase, string username = "")
        {
            PaginatedResultBase<Credit> result = new PaginatedResultBase<Credit>();

            List<Credit> listCredit = _dbContext.Credits.Where(x => 
                                            (string.IsNullOrEmpty(username) || x.CreatedBy == username) && 
                                            x.Name.ToLower().Contains(searchBase.SearchText.ToLower())
                                        ).ToList();

            List<Credit> listCreditPaging = listCredit.Skip((searchBase.PageIndex - 1) * searchBase.PageSize)
                                                      .Take(searchBase.PageSize).ToList();

            int totalItem = listCredit.Count();
            
            int totalPage = (int)Math.Ceiling((double)totalItem / searchBase.PageSize);

            if (searchBase.PageSize == 0) totalPage = 0;

            result.PageIndex = searchBase.PageIndex;
            result.TotalPage = totalPage;
            result.ListResult = listCredit;

            return result;
        }
    }
}
