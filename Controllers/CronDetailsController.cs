using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MementoScraperApi.Models;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using MementoScraperApi.Exceptions;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using MementoScraperApi.Credentials;
using Microsoft.AspNetCore.Cors;

namespace MementoScraperApi.Controllers {
    
    [Authorize]
    [Route("api/[controller]")]
    public class CronDetailsController : Controller {
        private ICronDetailService _cronDetailService;
        private IMapper _mapper;
        private readonly AppSecret _appSecret;


        public CronDetailsController(
            ICronDetailService cronDetailService,
            IMapper mapper,
            IOptions<AppSecret> appSettings) {
            this._cronDetailService = cronDetailService;
            this._mapper = mapper;
            this._appSecret = appSettings.Value;
        }

        [HttpPost("create")]
        public IActionResult createCronDetail([FromBody]CronDetailDto cronDetailDto) {
            // map dto to entity
            var cronDetail = this._mapper.Map<CronDetail>(cronDetailDto);

            try {
                // save 
                this._cronDetailService.Create(cronDetail);
                return Ok();
            } catch(AppException ex) {
                // return error message if there was an exception
                return BadRequest(ex.Message);
            }
        }

        [Route("GetMementosByUser/{userId}")]
        [HttpGet("{userId}", Name = "GetMementosByUser")]
        public ActionResult<List<CronDetail>> GetMementosByUser(int userId) {
            var items = this._cronDetailService.GetMementosBy(userId);
            if (items == null) {
                return NotFound();
            }
            return items;
        }
    }
}