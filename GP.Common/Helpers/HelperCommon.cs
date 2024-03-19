using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace GP.Common.Helpers
{
    public class HelperCommon
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HelperCommon(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        private static readonly string[] VietnameseSigns = new string[]
        {

            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"
        };

        public string ConvertFullnameCode(string fullname)
        {
            if(fullname == null)
            {
                return "";
            }
            string str = fullname.Trim().Replace(" ", "");
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }

        public string GennerationUsernameStudent(string fullname = "", string studentCode = "", string semesterId = "")
        {
            return semesterId + "_" + ConvertFullnameCode(fullname) + "_" + studentCode;
        }

        public string GennerationUsernameTeacher(string fullname = "", string teacherCode = "")
        {
            return ConvertFullnameCode(fullname) + "_" + teacherCode;
        }

        public bool IsExcelFile(string fileName)
        {
            // Kiểm tra phần mở rộng của tên file để đảm bảo đúng định dạng Excel
            string[] allowedExtensions = { ".xlsx", ".xls" };
            string extension = Path.GetExtension(fileName);
            return allowedExtensions.Contains(extension.ToLower());
        }
    }
}
