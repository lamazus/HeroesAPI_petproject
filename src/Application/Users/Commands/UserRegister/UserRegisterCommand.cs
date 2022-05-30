
namespace Application.Users.Commands.UserRegister
{
    public class UserRegisterCommand : IRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAuthenticationService _authService;
        public UserRegisterCommandHandler(IApplicationDbContext context, IAuthenticationService authService)
        {
            _context = context;
            _authService = authService;
        }
        public async Task<Unit> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {

            if (await _authService.IsUsernameExists(request.Username) || await _authService.IsEmailReserved(request.Email))
                            throw new UserRegistrationException(request.Username);

            _authService.EncryptPassword(request.Password, out byte[] passwordHash, out byte[] passwordSalt);


            var newUser = new User
            {
                Username = request.Username.ToLower(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = UserRoles.user
                
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync(cancellationToken);


            return Unit.Value;
        }
    }
}
