using System;
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


namespace OnlineBuy.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : MyBaseController
    {
        public CustomerController(IUnitOfWork<OnlineBuyContext> db,
            IConfiguration config, IJsonWebTokensService jwt) : base(db, config, jwt)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CustomerDot.CustomerRegister customerRegister)
        {

            var retrivedCustomer = await _db.CustomerRepository.GetAsync(c => c.Mobile == customerRegister.Mobile);
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
            var returnMessage = new ReturnApiMessages();
            var registeredCustomer = await _db.CustomerRepository.GetAsync(c => c.Mobile == customerValidation.Mobile);
            if (registeredCustomer == null)
            {
                return BadRequest(new ReturnApiMessages
                {
                    IsRegistered = false
                });
            }


            returnMessage.IsRegistered = true;
            var recievedCustomer = await _db.CustomerSmsCodeRepository.CustomerIsRecievedCode(registeredCustomer.Id);
            if (recievedCustomer)
                returnMessage.IsRecievedCode = true;


            returnMessage.Code = (int)StatusMethods.SuccessCreateToken;
            returnMessage.Title = PersianMessages.CustomerTitle;
            returnMessage.Status = StatusMethods.SuccessCreateToken.GetTitle();
            returnMessage.Token = _jwt.CreateToken(_config.GetSection("Appsettings:Token").Value,
                    new[] {
                        new Claim(ClaimTypes.NameIdentifier, registeredCustomer.Id),
                        new Claim(ClaimTypes.Name, registeredCustomer.Name+" "+registeredCustomer.Family),
                    },
                    DateTime.Now.AddSeconds(double.Parse(_config.GetSection("Appsettings:TokenTimeSecond").Value)));


            return Ok(returnMessage);
        }

        [HttpPost("sendSmsCode")]
        [Authorized]
        public async Task<IActionResult> SendSmsCode(CustomerDot.CustomerSendCode customerSendCode)
        {
            var customer = await _db.CustomerRepository.GetAsync(c => c.Id == GetIdentifyCode());
            var cusSmsCode = new CustomerSmsCode
            {
                SmsCode = _db.CustomerSmsCodeRepository.GenerateCode(),
                DateCreated = DateTime.Now,
                DurationCode = double.Parse(_config.GetSection("Appsettings:DurationSmsCode").Value),
                SentTime = GetCurrentTime(),
                CustomerId = GetIdentifyCode()
            };
            await _db.CustomerSmsCodeRepository.InsertAsync(cusSmsCode);
            var saveResult = await _db.SaveAsync();

            if (saveResult > 0)
            {
                SmsService.SendMessageWithCodeResponse res = await new SmsService.
                   FastSendSoapClient(new SmsService.FastSendSoapClient.EndpointConfiguration())
                   .SendMessageWithCodeAsync(_config.GetSection("Appsettings:SmsUser").Value,
                   _config.GetSection("Appsettings:SmsPassword").Value, customer.Mobile, cusSmsCode.SmsCode.ToString());

                if (res.Body.SendMessageWithCodeResult >= 200)
                {
                    return Ok(new ReturnApiMessages
                    {
                        DurationTime = cusSmsCode.DurationCode,
                        Code = (int)StatusMethods.SuccessSmsCode,
                        Title = PersianMessages.CustomerTitle,
                        Status = StatusMethods.SuccessSmsCode.GetTitle(),
                        Message=StatusMethods.SuccessSmsCode.GetDescription()
                    });
                }
                else
                {
                    return BadRequest(new ReturnApiMessages
                    {
                        Code = (int)StatusMethods.FailedSmsCode,
                        Title = PersianMessages.CustomerTitle,
                        Status = StatusMethods.FailedSmsCode.GetTitle(),
                        Message = StatusMethods.FailedSmsCode.GetDescription()
                    });
                }
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

        [HttpGet("orders")]
        public IActionResult Orders(string customerId)
        {
            var orders = _db.CustomerOrderRepository.GetOrders(customerId);
            return Ok(new
            {
                orders
            });
        }

    }
}