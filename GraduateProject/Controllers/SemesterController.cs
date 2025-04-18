﻿using GP.Business.IService;
using GP.Business.Service;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.Models.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterController : ControllerBase
    {
        private readonly ISemesterService _semesterService;

        public SemesterController(ISemesterService semesterService)
        {
            _semesterService = semesterService;
        }

        [HttpGet("get-semester")]
        public Response GetListSemester(string idSemester)
        {
            Response response = new Response();

            try
            {
                //var currentAccount = _accountService.GetInfoAccount(username);
                response.Msg = "Sucess";
                response.Code = 200;
                response.ReturnObj = _semesterService.getSemester(idSemester);
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }


        // 
        [HttpPost("get-list-semester")]
        public Response GetListSemester([FromBody]SemesterDTO req)
        {
            Response response = new Response();

            try
            {
                //var currentAccount = _accountService.GetInfoAccount(username);
                response.Msg = "Sucess";
                response.Code = 200;
                response.ReturnObj = _semesterService.GetListSemester(req);
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        // 
        [HttpPost("get-list-page-semester")]
        public Response GetListSemesterPage([FromBody] SemesterListModel req)
        {
            Response response = new Response();

            try
            {
                //var currentAccount = _accountService.GetInfoAccount(username);
                response.Msg = "Sucess";
                response.Code = 200;
                response.ReturnObj = _semesterService.GetListSemesterPage(req);
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpPost("add-semester")]
        public Response AddSemeter([FromBody] SemesterDTO req)
        {
            Response response = new Response();

            try
            {
                //var currentAccount = _accountService.GetInfoAccount(username);
                response.Code = 201;
                bool isCheck = _semesterService.Add(req, out string message);
                if(!isCheck)
                {
                    response.SetError(400,message);
                }
                response.Msg = message;
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }
    }
}
