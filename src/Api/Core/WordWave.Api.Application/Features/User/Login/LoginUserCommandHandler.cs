using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using MediatR;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WordWave.Api.Application.Interfaces.Repository;
using WordWave.Api.Common.Infrastructure;
using WordWave.Api.Common.Infrastructure.Exceptions;
using WordWave.Api.Common.ViewModels.Queries;
using WordWave.Api.Common.ViewModels.RequestModels;

namespace WordWave.Api.Application.Features.User.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;

    public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.configuration = configuration;
    }

    public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var dbUser = await userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);

        if (dbUser == null)
            throw new DatabaseValidationException("User not found!");

        var pass = PasswordEncryptor.Encrpt(request.Password);
        if (dbUser.Password != pass)
            throw new DatabaseValidationException("Password is wrong!");

        if (!dbUser.EmailConfirmed)
            throw new DatabaseValidationException("Email address is not confirmed yet!");

        var result = mapper.Map<LoginUserViewModel>(dbUser);

        var claims = new Claim[] 
        {
            new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
            new Claim(ClaimTypes.Email, dbUser.EmailAddress),
            new Claim(ClaimTypes.Name, dbUser.UserName),
            new Claim(ClaimTypes.GivenName, dbUser.FirstName),
            new Claim(ClaimTypes.Surname, dbUser.LastName)
        };

        result.Token = GenerateToken(claims);

        return result;
    }

    private string GenerateToken(Claim[] claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthConfig:Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.Now.AddDays(10);

        var token = new JwtSecurityToken(claims: claims,
                                         expires: expiry,
                                         signingCredentials: creds,
                                         notBefore: DateTime.Now);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}