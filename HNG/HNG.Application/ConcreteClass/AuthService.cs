using HNG.Application.Interface;
using HNG.Domain.AuthEntities;
using HNG.Domain.BaseEntities;
using System.Security.Cryptography;

namespace HNG.Application.ConcreteClass
{
    public class AuthService : IAuthService
    {
        public async Task<ApiResponse<string>> LoginAsync(Login loginDTO)
        {
            if (!IsValidEmailFormat(loginDTO.Email))
            {
                return ApiResponse<string>.Failed("Invalid email format.", 400);
            }

            string firstName = GetFirstNameFromEmail(loginDTO.Email);

            if (!IsValidPasswordFormat(loginDTO.Password, firstName))
            {
                return ApiResponse<string>.Failed("Invalid password format.", 400);
              
            }
            string token = GenerateRandomToken();

            return ApiResponse<string>.Success(token, "Login successful", 200);
        }

        private bool IsValidEmailFormat(string email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(email,
                @"^[a-z]+\.[a-z]+@gmail\.com$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        private string GetFirstNameFromEmail(string email)
        {
            return email.Split('@')[0].Split('.')[0];
        }

        private bool IsValidPasswordFormat(string password, string firstName)
        {
            return password == $"{firstName}##";
        }

        private string GenerateRandomToken()
        {
            var randomBytes = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }
    }
}
