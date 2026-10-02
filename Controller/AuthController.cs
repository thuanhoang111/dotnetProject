using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyAppApi.Data;
using MyAppApi.Models;
using MyAppApi.Models.OtherModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyAppApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public AuthController(AppDbContext _appDbContext, IConfiguration configuration)
        {
            _context = _appDbContext;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var riyosya = _context.Riyosya
                .FirstOrDefault(x =>
                    x.Riyosha_Id == request.Username &&
                    x.password == request.Password &&
                    x.delflg != 1
                );

            // Không tìm thấy user
            if (riyosya == null)
            {
                return Unauthorized(new
                {
                    message = "Username hoặc password không đúng"
                });
            }

            var claimsAccess = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    riyosya.Riyosha_Id
                ),

                new Claim(
                    ClaimTypes.Name,
                    riyosya.name ?? riyosya.Riyosha_Id
                ),

                new Claim(
                    ClaimTypes.Role,
                    riyosya.lebel?.ToString() ?? "User"
                ),
                new Claim(
                    "token_type",
                    "access"
                )
            };
            var claimsRefresh = new List<Claim>
                {
                    new Claim(
                        ClaimTypes.NameIdentifier,
                        riyosya.Riyosha_Id
                    ),
                    new Claim(
                        "token_type",
                        "refreshToken"
                    )
                };  

            var jwtSettings = _configuration.GetSection("Jwt");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    jwtSettings["Key"]!
                )
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            // =========================
            // ACCESS TOKEN
            // =========================

            var accessToken = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claimsAccess,
                expires: DateTime.UtcNow.AddMinutes(
                    double.Parse(
                        jwtSettings["ExpireMinutes"]!
                    )
                ),
                signingCredentials: credentials
            );

            var accessTokenString =
                new JwtSecurityTokenHandler()
                    .WriteToken(accessToken);


            // =========================
            // REFRESH TOKEN
            // =========================

            var refreshToken = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claimsRefresh,
                expires: DateTime.UtcNow.AddMinutes(
                    double.Parse(
                        jwtSettings["ExpireMinutesRefreshToken"]!
                    )
                ),
                signingCredentials: credentials
            );

            var refreshTokenString =
                new JwtSecurityTokenHandler()
                    .WriteToken(refreshToken);


            return Ok(new
            {
                accessToken = accessTokenString,
                refreshToken = refreshTokenString
            });
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken(RefreshTokenRequest request)
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    jwtSettings["Key"]!
                )
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(
                    request.RefreshToken,
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings["Issuer"],

                        ValidateAudience = true,
                        ValidAudience = jwtSettings["Audience"],

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,

                        IssuerSigningKey = key,

                        ClockSkew = TimeSpan.Zero
                    },
                    out SecurityToken validatedToken
                );

                // Kiểm tra đây có phải Refresh Token không
                var tokenType =
                    principal.FindFirst("token_type")?.Value;

                if (tokenType != "refresh")
                {
                    return Unauthorized(new
                    {
                        message = "Invalid refresh token"
                    });
                }

                // Lấy UserId từ token
                var userId =
                    principal
                        .FindFirst(
                            ClaimTypes.NameIdentifier
                        )?
                        .Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                // Kiểm tra user còn tồn tại không
                var riyosya = _context.Riyosya
                    .FirstOrDefault(x =>
                        x.Riyosha_Id == userId &&
                        x.delflg != 1
                    );

                if (riyosya == null)
                {
                    return Unauthorized();
                }

                // Tạo Access Token mới
                var claims = new List<Claim>
                    {
                        new Claim(
                            ClaimTypes.NameIdentifier,
                            riyosya.Riyosha_Id
                        ),

                        new Claim(
                            ClaimTypes.Name,
                            riyosya.name ?? riyosya.Riyosha_Id
                        ),

                        new Claim(
                            ClaimTypes.Role,
                            riyosya.lebel?.ToString() ?? "User"
                        ),

                        new Claim(
                            "token_type",
                            "access"
                        )
                    };

                var credentials = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256
                );

                var newAccessToken =
                    new JwtSecurityToken(
                        issuer: jwtSettings["Issuer"],
                        audience: jwtSettings["Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(
                            double.Parse(
                                jwtSettings["ExpireMinutes"]!
                            )
                        ),
                        signingCredentials: credentials
                    );

                var newAccessTokenString =
                    tokenHandler.WriteToken(newAccessToken);

                return Ok(new
                {
                    accessToken = newAccessTokenString
                });
            }
            catch
            {
                return Unauthorized(new
                {
                    message = "Refresh token không hợp lệ hoặc đã hết hạn"
                });
            }
        }
    }
}
