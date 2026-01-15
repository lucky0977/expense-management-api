using ExpenseManagement.Application.DTOs.Auth;
using ExpenseManagement.Application.Interfaces;
using ExpenseManagement.Domain.Entities;
using ExpenseManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagement.Infrastructure.Auth
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthService(
            ApplicationDbContext context,
            JwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        // ---------------- REGISTER ----------------
        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var exists = await _context.Users
                .AnyAsync(x => x.Email == request.Email);

            if (exists)
                throw new Exception("User already exists");

            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResponse
            {
                Token = token
            };
        }

        // ---------------- LOGIN ----------------
        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
                throw new Exception("Invalid credentials");

            var isPasswordValid =
                BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isPasswordValid)
                throw new Exception("Invalid credentials");

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResponse
            {
                Token = token
            };
        }
    }
}
