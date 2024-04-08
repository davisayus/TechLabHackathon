using LT.TechLabHackathon.Domain.Contracts;
using LT.TechLabHackathon.Domain.Entities;
using LT.TechLabHackathon.Shared.DTOs;
using LT.TechLabHackathon.Shared.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static LT.TechLabHackathon.Shared.DTOs.Records;

namespace LT.TechLabHackathon.Core.v1
{
    public class AuthCore
    {
        private readonly ILogger _logger;
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly ErrorHandler<AuthCore> _errors;
        private readonly EncryptCore _encryptCore;

        public AuthCore(IConfiguration configuration, IAuthRepository repository, ILogger<AuthCore> logger)
        {
            _configuration = configuration;
            _repository = repository;
            _logger = logger;
            _errors = new ErrorHandler<AuthCore>(logger);
            _encryptCore = new EncryptCore();
        }
        public async Task<ResponseService<LoginResponseDto>> Login(string userEmail, string password)
        {
            try
            {
                var userInfo = await ValidateUserAccess(userEmail);
                //string passEncrypt = _encryptCore.Encrypt_SHA256(password, userEmail, _configuration["EncrypPassword:Key"]!);
                string encrypValue = $"{userEmail}.{password}";
                string passEncrypt = _encryptCore.Encrypt_HMACSHA256(encrypValue, _configuration["EncrypPassword:Key"]!);
                if (userInfo.Password.Trim() != passEncrypt)
                    throw new Exception("Invalid user name or password");

                if (userInfo.AuthDoublefactor)
                {
                    await SendDynamicPassword(userInfo);
                    return new ResponseService<LoginResponseDto>(false, "We have sent you an email with the dynamic password", HttpStatusCode.OK, new LoginResponseDto(string.Empty, userInfo.MapToDto()));
                }
                else
                {
                    var userDto = userInfo.MapToDto();
                    var response = new LoginResponseDto(GetTokenJWT(userDto), userDto);
                    return new ResponseService<LoginResponseDto>(false, "Success Authentication", HttpStatusCode.OK, response);
                }

            }
            catch (Exception ex)
            {
                return _errors.Error(ex, "Login", new LoginResponseDto(string.Empty, new UserDto()));
            }
        }
        public async Task<ResponseService<bool>> RequestDynamicPassword(string userEmail)
        {
            try
            {
                var userInfo = await ValidateUserAccess(userEmail);

                if (userInfo.AuthDoublefactor)
                    throw new Exception("User handles two-factor authentication, invalid login path");

                if (!await SendDynamicPassword(userInfo))
                    throw new Exception("There were problems sending the dynamic key, please try again.");

                return new ResponseService<bool>(false, "we have sent you an email with the dynamic password", HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return _errors.Error(ex, "RequestDynamicPassword", false);
            }
        }
        public async Task<ResponseService<LoginResponseDto>> LoginWithDynamicPassword(string userEmail, string dynamicPassword)
        {
            try
            {
                if (!int.TryParse(dynamicPassword, out int value))
                    throw new Exception("User or dynamic password not valid");

                var userInfo = await ValidateUserAccess(userEmail);

                var userKey = await _repository.GetUserDynamicKey(userInfo.UserId);
                if (userKey is null || string.IsNullOrEmpty(userKey.DynamicKey))
                    throw new Exception("The user has not generated a dynamic password");

                string dynamicPassEncrypt = _encryptCore.Encrypt_SHA256(dynamicPassword, userEmail, _configuration["EncrypPassword:Key"]!);
                if (userKey.KeyExpiration <= DateTime.Now || userKey.DynamicKey != dynamicPassEncrypt)
                    throw new Exception("User or dynamic password not valid");

                var userDto = userInfo.MapToDto();
                var response = new LoginResponseDto(GetTokenJWT(userDto), userDto);

                return new ResponseService<LoginResponseDto>(false, "Success Authentication", HttpStatusCode.OK, response, 1);
            }
            catch (Exception ex)
            {
                return _errors.Error<LoginResponseDto>(ex, "LoginWithDynamicPassword", new LoginResponseDto(string.Empty, new UserDto()));
            }
        }
        public async Task<ResponseService<bool>> SetPassword(string userEmail, string password)
        {
            try
            {
                var userInfo = await ValidateUserAccess(userEmail);

                if (userInfo.StatusId != 5) // Pending by password
                    throw new Exception("Invalid user status for password assignment");

                //string passEncrypt = _encryptCore.Encrypt_SHA256(password, userEmail, _configuration["EncrypPassword:Key"]!);
                string encrypValue = $"{userEmail}.{password}";
                string passEncrypt = _encryptCore.Encrypt_HMACSHA256(encrypValue, _configuration["EncrypPassword:Key"]!);
                if (!await _repository.UpdatePasswordUser(userInfo.UserId, passEncrypt))
                    throw new Exception("There were problems assigning the password, try again");

                return new ResponseService<bool>(false, "Success assigning password", System.Net.HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return _errors.Error(ex, "AddPassword", false);
            }
        }
        public async Task<ResponseService<bool>> ChangePassword(string userEmail, string currentPassword, string newPassword)
        {
            try
            {
                var userInfo = await ValidateUserAccess(userEmail);

                //string passEncrypt = _encryptCore.Encrypt_SHA256(currentPassword, userEmail, _configuration["EncrypPassword:Key"]!);

                string encrypValue = $"{userEmail}.{currentPassword}";
                string passEncrypt = _encryptCore.Encrypt_HMACSHA256(encrypValue, _configuration["EncrypPassword:Key"]!);

                if (userInfo.Password != passEncrypt)
                    throw new Exception("Invalid username or password");

                //string newPassEncrypt = _encryptCore.Encrypt_SHA256(newPassword, userEmail, _configuration["EncrypPassword:Key"]!);
                encrypValue = $"{userEmail}.{newPassword}";
                string newPassEncrypt = _encryptCore.Encrypt_HMACSHA256(encrypValue, _configuration["EncrypPassword:Key"]!);
                if (!await _repository.UpdatePasswordUser(userInfo.UserId, newPassEncrypt))
                    throw new Exception("There were problems assigning the password, try again");

                return new ResponseService<bool>(false, "Success Change Password", System.Net.HttpStatusCode.OK, true);
            }
            catch (Exception e)
            {
                return _errors.Error(e, "ChangePassword", false);
            }
        }
        public async Task<ResponseService<UserDto>> GetUserAuthenticated(string userEmail)
        {
            try
            {
                var userInfo = await ValidateUserAccess(userEmail);
                var userDto = userInfo.MapToDto();
                return new ResponseService<UserDto>(false, "User found", HttpStatusCode.OK, userDto);
            }
            catch (Exception ex)
            {
                return _errors.Error(ex, "GetUserAuthenticated", new UserDto());
            }
        }
        private async Task<User> ValidateUserAccess(string userEmail)
        {
            try
            {
                var userSelected = await _repository.GetUserByEmail(userEmail);
                if (userSelected is null || userSelected.Email.ToLower() != userEmail.ToLower())
                    throw new Exception("Invalid user name or password");

                if (userSelected.StatusId == 4) // Status: Canceled
                    throw new Exception("Invalid user state, please contact your system administrator to review your status");

                return userSelected;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<bool> SendDynamicPassword(User userInfo)
        {
            try
            {
                var dynamicPassword = GetDynamicPassword();
                string dynamicPassEncrypt = _encryptCore.Encrypt_SHA256(dynamicPassword, userInfo.Email, _configuration["EncrypPassword:Key"]!);

                var userKey = await _repository.GetUserDynamicKey(userInfo.UserId);
                if (userKey is null || string.IsNullOrEmpty(userKey.DynamicKey))
                {
                    var userToken = new AuthUserKey()
                    {
                        DynamicKey = dynamicPassEncrypt,
                        UserId = userInfo.UserId,
                        KeyCreation = DateTime.Now,
                        KeyExpiration = DateTime.Now.AddMinutes(5),
                        StatusId = 1 // Active
                    };
                    _ = await _repository.AddUserDynamicKey(userToken);
                }
                else
                {
                    userKey.DynamicKey = dynamicPassEncrypt;
                    userKey.KeyCreation = DateTime.Now;
                    userKey.KeyExpiration = DateTime.Now.AddMinutes(5);
                    userKey.StatusId = 1; // Active
                    _ = await _repository.UpdateUserDynamicKey(userKey);
                }

                var (SuccessfullySent, Message) = await SendDynamicPasswordByEmail(userInfo.Email, dynamicPassword);
                if (SuccessfullySent)
                    return SuccessfullySent;
                else
                    throw new Exception(Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public async Task<ResponseService<bool>> ResetPassword(string userEmail)
        //{
        //    try
        //    {
        //        var result = await GetUser(userEmail);
        //        if (result.Item2)
        //        {
        //            result.Item1.Status = "00";
        //            result.Item1.Password = string.Empty;
        //            await _dbContext.UpdateAsync(result.Item1);
        //            return new ResponseService<bool>(false, "Success Reset Password", System.Net.HttpStatusCode.OK, true);
        //        }
        //        else
        //            return new ResponseService<bool>(true, "User not found", System.Net.HttpStatusCode.OK, false);
        //    }
        //    catch (Exception e)
        //    {
        //        return _errors.Error<bool>(e, "ResetPassword", false);
        //    }
        //}

        private string GetTokenJWT(UserDto usuarioInfo)
        {
            // Create header
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]!)
                );
            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature
                );

            // Create claims
            var _Claims = new ClaimsIdentity(new Claim[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, usuarioInfo.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuarioInfo.Email),
                new Claim(JwtRegisteredClaimNames.Name, usuarioInfo.Name),
            });

            // Create a descriptor
            var _tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = _Claims,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = _signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(_tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private static string GetDynamicPassword()
        {
            var availableDigits = Enumerable.Range(0, 9).ToList();
            var random = new Random();
            var dynamicPassword = new List<int>();

            // To generate a 6-digit password
            for (int i = 0; i < 6; i++)
            {
                int randomIndex = random.Next(0, availableDigits.Count);
                int randomDigit = availableDigits[randomIndex];
                dynamicPassword.Add(randomDigit);
                availableDigits.RemoveAt(randomIndex);
            }

            return string.Join("", dynamicPassword);
        }
        private async Task<(bool SuccessfullySent, string Message)> SendDynamicPasswordByEmail(string userEMail, string dynamicPassword)
        {
            try
            {

                //MailServerConfig mailServerConfig = new();
                //mailServerConfig = _configuration.GetSection("EMailConfig").Get<MailServerConfig>() ?? new MailServerConfig();
                //MailService eMail = new(mailServerConfig);

                //StringBuilder body = new($"<p><span style=\"font-family: Tahoma, Geneva, sans-serif; font-size: 18px;\">We have received a request for an EQIS access key.</span><br><span style=\"font-family: Tahoma, Geneva, sans-serif; font-size: 18px;\">To continue with the process, please use the following code:</span> <span style=\"font-family: Tahoma, Geneva, sans-serif; font-size: 24px;\">{dynamicPassword}</span><br><span style=\"font-size: 18px; font-family: Tahoma, Geneva, sans-serif;\">(Expires in: 5 minutes)</span><br><span style=\"font-size: 18px; font-family: Tahoma, Geneva, sans-serif;\">Do not share this email or code with anyone else</span></p>");

                ////StringBuilder body = new StringBuilder("User Dynamic Password <br>");
                ////body.Append($"Please login with this dynamic password: {dynamicPassword} <br>");
                ////body.Append($"Expire in: 5 minutes");


                //var (Ok, Message) = await eMail.SendEMail($"EQIS Authentication", new List<string>() { userEMail }, new List<string>(), new List<string>(), body.ToString(), false, new List<string>());
                //return (Ok, Message);
                return (true,"Ok");
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
