using Business_Layer.Interface;
using Common_Layer.Models.user;
using Common_Layer.NewFolder;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public UserModel Registration(UserModel userReg)
        {
            try
            {
                return userRL.Registration(userReg);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string Login(UserLog userLog)
        {
            try
            {
                return userRL.Login(userLog);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string ForgotPassword(string emailID)
        {
            try
            {
                return userRL.ForgotPassword(emailID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string ResetPassword(string emailID, string newPassword, string confirmPassword)
        {
            try
            {
                return userRL.ResetPassword(emailID , newPassword , confirmPassword);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
