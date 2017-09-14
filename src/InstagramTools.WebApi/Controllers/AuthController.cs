using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using InstagramTools.Common.Helpers;
using InstagramTools.Core.Interfaces;
using InstagramTools.WebApi.Models;
using InstagramTools.WebApi.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace InstagramTools.WebApi.Controllers
{
    public class AuthController: Controller
    {
        private readonly IAuthorizeService _authService;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ILogger _logger;
        private readonly JsonSerializerSettings _serializerSettings;

        public AuthController(IAuthorizeService authService, IOptions<JwtIssuerOptions> jwtOptions,
            ILoggerFactory loggerFactory)
        {
          this._jwtOptions = jwtOptions.Value;
          this.ThrowIfInvalidOptions(this._jwtOptions);
          this._authService = authService;
          this._logger = loggerFactory.CreateLogger<AuthController>();
          this._serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        [HttpPost("/api/auth/token")]
        [AllowAnonymous]
        public async Task<OperationResult<JwtResultModel>> Get([FromBody] LoginModel applicationUser)
        {
            var result = await this.GetToken(applicationUser);
            return result;
        }

        [HttpPost("/api/auth/prolong")]
        [AllowAnonymous]
        public async Task<OperationResult<JwtResultModel>> Prolong([FromBody] LoginModel applicationUser)
        {
            try
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                var jwt = jwtHandler.ReadJwtToken(applicationUser.Token);
                var identity = jwt.Claims;
                if (identity == null)
                {
                  this._logger.LogInformation($"Invalid token.");
                    return new OperationResult<JwtResultModel>(false, "Invalid token!");
                }

                string username = jwt.Claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.UserName).Value;
                string password = jwt.Claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.Password).Value;

                var result = await this.GetToken(new LoginModel() { Username = username, Password = password });
                return result;
            }
            catch (Exception ex)
            {
              this._logger.LogInformation($"On Auth error: {ex.Message}");
                return new OperationResult<JwtResultModel>(false, ex.Message);
            }
        }


        private void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        private async Task<OperationResult<JwtResultModel>> GetToken(LoginModel applicationUser)
        {
            try
            {
                var claims = await this.GetClaimsIdentity(applicationUser);
                if (claims == null)
                {
                  this._logger.LogInformation($"Invalid username ({applicationUser.Username}) or password ({applicationUser.Password})");
                    return new OperationResult<JwtResultModel>(false, "Invalid credentionals!");
                }

                int userId = Convert.ToInt32(claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.UserId).Value);
                string role = claims.FirstOrDefault(x => x.Type == Constants.ClaimTypes.RoleName).Value;

                claims.AddRange(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, await this._jwtOptions.JtiGenerator()),
                    new Claim(JwtRegisteredClaimNames.Iat,
                      this.ToUnixEpochDate(this._jwtOptions.IssuedAt).ToString(),
                              ClaimValueTypes.Integer64),
                });

                // Create the JWT security token and encode it.
                var jwt = new JwtSecurityToken(
                    issuer: this._jwtOptions.Issuer,
                    audience: this._jwtOptions.Audience,
                    claims: claims,
                    notBefore: this._jwtOptions.NotBefore,
                    expires: this._jwtOptions.Expiration,
                    signingCredentials: this._jwtOptions.SigningCredentials);

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new JwtResultModel()
                {
                    UserId = userId,
                    Username = applicationUser.Username,
                    Role = role,
                    AccessToken = encodedJwt,
                    ExpiresIn = (int)this._jwtOptions.ValidFor.TotalSeconds
                };

                var result = new OperationResult<JwtResultModel>(response);
                return result;
            }
            catch (Exception ex)
            {
              this._logger.LogInformation($"On Auth error: {ex.Message}");
                return new OperationResult<JwtResultModel>(false, ex.Message);
            }
        }


        private async Task<List<Claim>> GetClaimsIdentity(LoginModel user)
        {
            var validatePeople = await this._authService.ValidateUser(user.Username, user.Password);
            if (validatePeople == null)
            {
                // Credentials are invalid, or account doesn't exist
                return null;
            }

            var claims = new List<Claim>();
            claims.AddRange(new[]
                {
                    new Claim(Constants.ClaimTypes.UserId, validatePeople.Id.ToString()),
                    new Claim(Constants.ClaimTypes.UserName, validatePeople.Username),
                    new Claim(Constants.ClaimTypes.Password, validatePeople.Password),
                    new Claim(Constants.ClaimTypes.RoleName, validatePeople.RoleId),
                });

            // TODO: Implement Permissions
            // claimsIdentity.SetPermissions(_authService.GetPermissions(validatePeople.RoleId));
            return claims;
        }
    }
}
