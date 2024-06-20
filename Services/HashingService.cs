using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Dagagino.Services
{
    public class HashingService
    {
        /* -------------------------------------------------------------------------- */
        /*                                Hash Password                               */
        /* -------------------------------------------------------------------------- */
        public (string, string) HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16); // (128 / 8) divide by 8 to convert bits to bytes
            string saltString = Convert.ToBase64String(salt);
            return (saltString, HashPassword(saltString, password));
        }

        public string HashPassword(string saltString, string password)
        {
            /* ---------------------- Convert Salt String To Bytes ---------------------- */
            byte[] salt = Convert.FromBase64String(saltString);

            /* ---- derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations) ---- */
            string hashed = Convert.ToBase64String(
                    KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8
                    )
                );

            /* ------------------------- Return Salt with Hashed ------------------------ */
            return hashed;
        }

        /* -------------------------------------------------------------------------- */
        /*                              Compare Passwords                             */
        /* -------------------------------------------------------------------------- */
        public bool ValidatePassword(string saltString, string hashedPassword, string password)
        {
            string hashPassword = HashPassword(saltString, password);
            return hashPassword == hashedPassword;
        }
    }
}