using Common_Layer.Models.user;
using Common_Layer.NewFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interface
{
    public interface IUserBL
    {
        public UserModel Registration(UserModel userReg);
        public string Login(UserLog userLog);
        public string ForgotPassword(string emailID);
        public string ResetPassword(string emailID, string newPassword, string confirmPassword);

    }
}
