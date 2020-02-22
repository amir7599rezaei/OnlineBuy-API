﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineBuy.Common.Enums;
using OnlineBuy.Common.Messages.Persian;
using OnlineBuy.Common.ResultApiStructure;
using OnlineBuy.Common.Utility;
using OnlineBuy.Data.DataContext;
using OnlineBuy.Data.Models;
using OnlineBuy.Data.ViewModels;
using OnlineBuy.Presentation.CustomFilters;
using OnlineBuy.Repository.Infrastructure.Interfaces;
using OnlineBuy.Service.JWT.Interface;
using Serilog;

namespace OnlineBuy.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : MyBaseController
    {
        private readonly IUnitOfWork<OnlineBuyContext> _db;
        private readonly IConfiguration _config;
        private readonly IJsonWebTokensService _jwt;
        public CustomerController(IUnitOfWork<OnlineBuyContext> db,
            IConfiguration config, IJsonWebTokensService jwt)
        {
            this._db = db;
            this._config = config;
            this._jwt = jwt;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CustomerDot.CustomerRegister customerRegister)
        {

            var retrivedCustomer = await _db.CustomerRepository.GetAsync(c => c.UserName == customerRegister.UserName);
            if (retrivedCustomer != null)
                return BadRequest(new ReturnApiMessages
                {
                    Code = (int)StatusMethods.DuplicateRegister,
                    Status = StatusMethods.DuplicateRegister.GetTitle(),
                    Message = StatusMethods.DuplicateRegister.GetDescription(),
                    Title = PersianMessages.CustomerTitle,
                });

            var customer = new Customer
            {
                Name = customerRegister.Name,
                Family = customerRegister.Family,
                UserName = customerRegister.UserName,
                Address = customerRegister.Address,
                Tel = customerRegister.Tel,
                Mobile = customerRegister.Mobile,
                PostalCode = customerRegister.PostalCode
            };

            await _db.CustomerRepository.InsertAsync(customer);
            var saveResult = await _db.SaveAsync();

            if (saveResult > 0)
            {
                return Ok(new ReturnApiMessages
                {
                    Title = PersianMessages.CustomerTitle,
                    Code = (int)StatusMethods.SuccessRegister,
                    Status = StatusMethods.SuccessRegister.GetTitle(),
                    Message = StatusMethods.SuccessRegister.GetDescription(),
                    IdentifyCode = customer.Id
                });
            }
            else
            {
                return BadRequest(new ReturnApiMessages
                {
                    Code = (int)StatusMethods.OperationFailed,
                    Title = PersianMessages.CustomerTitle,
                    Status = StatusMethods.OperationFailed.GetTitle(),
                    Message = StatusMethods.OperationFailed.GetDescription()
                });
            }
        }

        [HttpPost("validation")]
        public async Task<IActionResult> Validation(CustomerDot.CustomerValidation customerValidation)
        {
            var registeredCustomer = await _db.CustomerRepository.GetByIdAsync(customerValidation.CustomerId);
            var recievedCustomer = await _db.CustomerSmsCodeRepository.CustomerIsRecievedCode(customerValidation.CustomerId);

            var returnMessage = new ReturnApiMessages();
            returnMessage.Code = (int)StatusMethods.SuccessCreateToken;
            returnMessage.Title = PersianMessages.CustomerTitle;
            returnMessage.Status = StatusMethods.SuccessCreateToken.GetTitle();
            returnMessage.Token = _jwt.CreateToken(_config.GetSection("Appsettings:Token").Value,
                    new[] {
                        new Claim(ClaimTypes.NameIdentifier, registeredCustomer.Id),
                        new Claim(ClaimTypes.Name, registeredCustomer.Name+" "+registeredCustomer.Family),
                    },
                    DateTime.Now.AddSeconds(double.Parse(_config.GetSection("Appsettings:TokenTimeSecond").Value)));

            if (registeredCustomer != null)
            {
                returnMessage.IsRegistered = true;
            }
            if (recievedCustomer)
            {
                returnMessage.IsRecievedCode = true;
            }

            return Ok(returnMessage);
        }

        [HttpPost("sendSmsCode")]
        [Authorized]
        public async Task<IActionResult> SendSmsCode(CustomerDot.CustomerSendCode customerSendCode)
        {
            var cusSmsCode = new CustomerSmsCode
            {
                SmsCode = _db.CustomerSmsCodeRepository.GenerateCode(),
                DateCreated = DateTime.Now,
                DurationCode = double.Parse(_config.GetSection("Appsettings:DurationSmsCode").Value),
                SentTime = GetCurrentTime(),
                CustomerId = GetIdentifyCode()
            };
            await _db.CustomerSmsCodeRepository.InsertAsync(cusSmsCode);
            await _db.SaveAsync();

            return Ok(new ReturnApiMessages
            {
                DurationTime = cusSmsCode.DurationCode,
                Code = (int)StatusMethods.SuccessSmsCode,
                Title = PersianMessages.CustomerTitle,
                Status = StatusMethods.SuccessSmsCode.GetTitle(),
                SmsCode = cusSmsCode.SmsCode,
            });
        }

        [HttpPost("recieveSmsCode")]
        [Authorized]
        public IActionResult ReciveSmsCode(CustomerDot.CustomerRecieveCode customerRecieveCode)
        {
            var msgRecive = _db.CustomerSmsCodeRepository
                .ValidReciveCode(GetIdentifyCode(), customerRecieveCode.Code);
            switch (msgRecive)
            {
                case Common.Enums.RecieveSms.WrongCodeOrMobile:
                    return BadRequest(new ReturnApiMessages
                    {
                        Code = (int)StatusMethods.WrongCodeOrMobile,
                        Title = PersianMessages.CustomerTitle,
                        Status = StatusMethods.WrongCodeOrMobile.GetTitle(),
                        Message = StatusMethods.WrongCodeOrMobile.GetDescription(),
                    });
                case Common.Enums.RecieveSms.PastTime:
                    return BadRequest(new ReturnApiMessages
                    {
                        Code = (int)StatusMethods.PastTime,
                        Title = PersianMessages.CustomerTitle,
                        Status = StatusMethods.PastTime.GetTitle(),
                        Message = StatusMethods.PastTime.GetDescription(),
                    });
                case Common.Enums.RecieveSms.SuccessCode:

                    var customerSms = _db.CustomerSmsCodeRepository.Get(c => c.CustomerId == GetIdentifyCode());
                    customerSms.RecievedTime = GetCurrentTime();
                    _db.CustomerSmsCodeRepository.Update(customerSms);
                    _db.Save();

                    return Ok(new ReturnApiMessages
                    {
                        Code = (int)StatusMethods.SuccessRecieveCode,
                        Title = PersianMessages.CustomerTitle,
                        Status = StatusMethods.SuccessRecieveCode.GetTitle(),
                        Message = StatusMethods.SuccessRecieveCode.GetDescription(),
                    });
                default:
                    return null;
            }
        }

        [HttpPost("Test")]
        [Authorized]
        public IActionResult TestSerilog()
        {
            return Ok(new
            {
                result = GetIdentifyCode()
            });

        }

    }
}