using Business_Layer.Interface;
using Common_Layer.Models.user;
using Common_Layer.NewFolder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Bookstore_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class USERController : ControllerBase
    {
        private readonly IUserBL userBL;
        public USERController(IUserBL userRL)
        {
            this.userBL = userRL;
        }

        [HttpPost("User_Registration")]
        public IActionResult Register(UserModel userReg)
        {
            try
            {
                var resUser = userBL.Registration(userReg);
                if (resUser != null)
                {
                    return Ok(new { success = true, message = "Registeration Successfully", data = resUser });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registeration Failed EmailId Already Exist", data = resUser });
                }
            }
            catch (Exception)
            {
                return NotFound(new { success = false, message = " Something went wrong" });
            }
        }

        [HttpPost("User_Login")]
        public IActionResult Login(UserLog userLog)
        {
            try
            {
                var resUser = userBL.Login(userLog);
                if (resUser != null)
                {
                    return Ok(new { success = true, message = "Login Successfull", data = resUser });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login Failed ", data = resUser });
                }
            }
            catch (Exception)
            {
                return NotFound(new { success = false, message = " Something went wrong" });
            }
        }

        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string emailID)
        {
            try
            {
                var resUser = userBL.ForgotPassword(emailID);
                if (resUser != null)
                {
                    return Ok(new { success = true, message = "Reset link sent Successfully", data = resUser });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset link sent Failed ", data = resUser });
                }
            }
            catch (Exception)
            {
                return NotFound(new { success = false, message = " Something went wrong" });
            }
        }


        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(string emailID, string newPassword, string confirmPassword)
        {
            try
            {
                var resUser = userBL.ResetPassword(emailID, newPassword, confirmPassword);
                if (resUser != null)
                {
                    return Ok(new { success = true, message = "Password Reset Successfully", data = resUser });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset Failed ", data = resUser });
                }
            }
            catch (Exception)
            {
                return NotFound(new { success = false, message = " Something went wrong" });
            }
        }

    }
}
