using Bjb.LiquidityGap.Base.Dtos.Auth;
using Bjb.LiquidityGap.Base.Exceptions;
using Bjb.LiquidityGap.Base.Wrappers;
using Bjb.LiquidityGap.Domain.Settings;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechRedemption.UIM.Enums;
using TechRedemption.UIM.Interfaces;
using TechRedemption.UIM.Models;

namespace Bjb.LiquidityGap.Application.Features.Auth.Commands.LoginUIM
{
    public class LoginUIMCommand : LoginUIMRequest, IRequest<Response<AuthResponse>>
    {
    }

    public class LoginUIMCommandHandler : IRequestHandler<LoginUIMCommand, Response<AuthResponse>>
    {
        private readonly IUIMService _uimService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly JWTSettings _jwtSettings;
        private readonly BaseSetting _baseSetting;
        public LoginUIMCommandHandler(IUIMService uimService, IHostingEnvironment hostingEnvironment, JWTSettings jwtSettings, BaseSetting baseSetting)
        {
            _uimService = uimService;
            _hostingEnvironment = hostingEnvironment;
            _jwtSettings = jwtSettings;
            _baseSetting = baseSetting;
        }

        public async Task<Response<AuthResponse>> Handle(LoginUIMCommand request, CancellationToken cancellationToken)
        {
            if (_hostingEnvironment.IsDevelopment())
            {
                var res = await _uimService.LoginDummy(request);
                if (res.Status == StatusEnum.SUCCESS)
                {
                    var claims = new[]
                    {
                        new Claim("UserId" ,  res.Data.UserId),
                        new Claim("UserName" ,  res.Data.Nama),
                        new Claim("IdFungsi" ,  res.Data.IdFungsi)
                    };
                    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
                    var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
                    var jwtSecurityToken = new JwtSecurityToken(
                        issuer: _jwtSettings.Issuer,
                        audience: _jwtSettings.Audience,
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(_baseSetting.TokenExpired),
                        signingCredentials: signingCredentials
                    );
                    AuthResponse authResponse = new AuthResponse
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    };
                    return new Response<AuthResponse>(authResponse) { StatusCode = 200 };
                }
                else
                {
                    throw new BadRequestException(message: $"{"Invalid username or password"}");
                }
            }
            else
            {
                var res = await _uimService.Login(request);
                if (res.Status == StatusEnum.SUCCESS)
                {
                    var claims = new []
                    {
                        new Claim("UserId" ,  res.Data.UserId),
                        new Claim("UserName" ,  res.Data.Nama),
                        new Claim("IdFungsi" ,  res.Data.IdFungsi)
                    };
                    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
                    var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
                    var jwtSecurityToken = new JwtSecurityToken(
                        issuer: _jwtSettings.Issuer,
                        audience: _jwtSettings.Audience,
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(_baseSetting.TokenExpired),
                        signingCredentials: signingCredentials
                    );
                    AuthResponse authResponse = new AuthResponse
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    };
                    return new Response<AuthResponse>(authResponse) { StatusCode = 200 };
                }
                else
                {
                    throw new BadRequestException(message: $"{"Invalid username or password"}");
                }
            }
        }
    }
}
