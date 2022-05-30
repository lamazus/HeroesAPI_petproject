using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Users.Commands.UserLogin
{
    public class UserLoginCommand : IRequest<string>
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }

    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAuthenticationService _authService;
        private readonly IConfiguration _config;
        public UserLoginCommandHandler(IApplicationDbContext context, IAuthenticationService authService,
            IConfiguration config)
        {
            _context = context;
            _authService = authService;
            _config = config;
        }
        public async Task<string> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(p => p.Username == request.Username.ToLower());
            if (user == null)
                throw new AuthenticationErrorException();

            if (!_authService.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
                throw new AuthenticationErrorException();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config.GetSection("JWT:IssuerSigningKey").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;

        }
    }
}
