using Common_Layer.Models.user;
using Common_Layer.NewFolder;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Repository_Layer.Service
{
    public class UserRL : IUserRL
    {
        private SqlConnection sqlConnection;
        private IConfiguration _appSettings;
        public IConfiguration Configuration { get; }
        public UserRL(IConfiguration configuration , IConfiguration _appSettings)
        {
            this.Configuration = configuration;
            this._appSettings = _appSettings;   
        }

        public UserModel Registration(UserModel userReg)
        {
            try
            {
                sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
                try
                {
                    using (sqlConnection)
                    {
                        SqlCommand sqlCommand = new SqlCommand("sp_UserReg", sqlConnection);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@FullName",userReg.FullName);
                        sqlCommand.Parameters.AddWithValue("@EmailID", userReg.EmailID);
                        sqlCommand.Parameters.AddWithValue("@Password", userReg.Password);
                        sqlCommand.Parameters.AddWithValue("@PhoneNumber", userReg.PhoneNumber);
                        sqlConnection.Open();
                        int result = sqlCommand.ExecuteNonQuery();
                        if (result >= 1)
                        {
                            sqlConnection.Close();
                            return userReg;
                        }
                        else
                        {
                            sqlConnection.Close();
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string GenerateSecurityToken(string emailID, long userID)
        {
            //header
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings["Jwt:SecurityKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //payload
            var claims = new[]
            {new Claim(ClaimTypes.Email,emailID),
            new Claim("UserID",userID.ToString())};
            //signature
            var token = new JwtSecurityToken(
            _appSettings["Jwt:Issuer"],
            _appSettings["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string Login(UserLog userLog)
        {
            try
            {
                sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
                try
                {
                    using (sqlConnection)
                    {
                        SqlCommand sqlCommand = new SqlCommand("sp_UserLog", sqlConnection);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@EmailID", userLog.EmailID);
                        sqlCommand.Parameters.AddWithValue("@Password", userLog.Password);
                        sqlConnection.Open();
                        SqlDataReader reader = sqlCommand.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int UserID = 0;
                            while (reader.Read())
                            {
                                userLog.EmailID = Convert.ToString(reader["EmailID"] == DBNull.Value ? default : reader["EmailID"]);
                                UserID = Convert.ToInt32(reader["UserID"] == DBNull.Value ? default : reader["UserID"]);
                                userLog.Password = Convert.ToString(reader["Password"] == DBNull.Value ? default : reader["Password"]);
                            }
                            sqlConnection.Close();
                            var token = GenerateSecurityToken(userLog.EmailID, UserID);
                            return token;
                        }
                        else
                        {
                            sqlConnection.Close();
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

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
                sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStoreDB"));
                try
                {
                    using (sqlConnection)
                    {
                        UserModel model = new UserModel();
                        SqlCommand sqlCommand = new SqlCommand("sp_UserForgotPass", sqlConnection);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@EmailID", emailID);
                        sqlConnection.Open();
                        SqlDataReader reader = sqlCommand.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int UserID = 0;
                            while (reader.Read())
                            {
                                emailID = Convert.ToString(reader["EmailID"] == DBNull.Value ? default : reader["EmailID"]);
                                UserID = Convert.ToInt32(reader["UserID"] == DBNull.Value ? default : reader["UserID"]);
                            }
                            sqlConnection.Close();
                            var token = GenerateSecurityToken(emailID, UserID);
                            new Msmq().SendMessage(token , model.FullName);
                            return token;
                        }
                        else
                        {
                            sqlConnection.Close();
                            return null;
                        }
                       
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

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
                if (newPassword == confirmPassword)
                {
                    sqlConnection = new SqlConnection(Configuration.GetConnectionString("BookStoreDB"));
                    using (sqlConnection)
                    {
                        SqlCommand command = new SqlCommand("sp_UserResetPass", sqlConnection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@EmailID", emailID);
                        command.Parameters.AddWithValue("@Password", confirmPassword);
                        sqlConnection.Open();
                        int i = command.ExecuteNonQuery();
                        sqlConnection.Close();
                        if (i >= 1)
                        {
                            return "Password changed successfully" ;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
