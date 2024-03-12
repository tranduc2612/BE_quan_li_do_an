using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.Models
{
    public class ProjectListModel: ListSearchBase
    {
        public string NameProject { get; set; }
        public string Datetime { get; set; }
    }
}
