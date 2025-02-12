﻿//Mechanika logowania. Autentykacja, weryfikacja hasła.
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using photoservice.Models;
using photoservice.Data;

namespace photoservice.Services
{
    public class AuthenticationService
    {
        private readonly PhotoserviceContext _context;

        public AuthenticationService(PhotoserviceContext context)
        {
            _context = context;
        }

        public async Task<ActiveUsersWithRole> LoginAsync(LoginRequest loginRequest) //zamiast tabeli Users użyty zostaje widok ActiveUsersWithRole, żeby pobrać od razu rolę użytkownika
        {
            // Znalezienie użytkownika po mailu
            var user = await _context.ActiveUsersWithRoles
                .Where(u => u.Email == loginRequest.Email)
                .FirstOrDefaultAsync();

            if (user == null)
                return null;

            //dopasowuje po mailu i sprawdza czy użytkownik nie jest kontem usuniętym
            var storedUser = await _context.Users
                .Where(u => u.Email == loginRequest.Email && u.IsDeleted == false)
                .FirstOrDefaultAsync();

            //Weryfikuje hasło
            if (storedUser == null || !VerifyPassword(loginRequest.Password, storedUser.PasswordHash))
                return null;

            return user;
        }

        // Funkcja do porównania hasła podanego przez użytkownika z hashem w bazie. Używa algorytmu HMACSHA512
        private bool VerifyPassword(string password, string storedHash)
        {
            try
            {
                byte[] hashBytes = Convert.FromBase64String(storedHash);
                byte[] salt = hashBytes.Take(16).ToArray();
                byte[] expectedHash = hashBytes.Skip(16).ToArray();

                using (var hmac = new HMACSHA512(salt))
                {
                    byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return computedHash.SequenceEqual(expectedHash);
                }
            }
            catch
            {
                return false;
            }
        }

        //Funkcja do hashowania hasła. Hasło zwracane w formacie Base64
        public string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            using (var hmac = new HMACSHA512(salt))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                byte[] hashBytes = new byte[16 + hash.Length];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, hash.Length);
                return Convert.ToBase64String(hashBytes);
            }
        }

    }
}
