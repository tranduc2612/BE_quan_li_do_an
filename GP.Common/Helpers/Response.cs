using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.Helpers
{
    public class Response
    {
        #region -- Properties --
        /// <summary>
        /// đối tượng trả về
        /// </summary>
        public object ReturnObj { get; set; }

        /// <summary>
        /// danh sách đối tượng trả về
        /// </summary>
        //public List<Dictionary<string, object>> ReturnListObj { get; set; }

        /// <summary>
        /// trạng thái thực hiện true | false
        /// </summary>
        public bool Success { get; set; } = false;

        /// <summary>
        /// mã phản hồi / status
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// thông báo
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// thông báo lỗi
        /// </summary>
        //public string MsgError { get; set; }

        /// <summary>
        /// object lỗi
        /// </summary>
        public object ExceptionInfo { get; set; }

        #endregion

        public Response()
        {
            Success = true;
            Msg = string.Empty;
            //Code = 200;
        }

        public Response(string message) : this()
        {
            Msg = message;
        }

        public Response(int code, object data) : this()
        {
            Code = code;
            ReturnObj = data;

        }

        public void SetError(string message)
        {
            Success = false;
            Msg = message;
        }

        public void SetError(int code, string message)
        {
            Success = false;
            Code = code;
            Msg = message;
        }

        public void SetData(int code, object data)
        {
            Code = code;
            ReturnObj = data;
        }

    }
}
