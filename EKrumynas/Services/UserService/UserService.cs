﻿using System;
using System.Text;
using System.Threading.Tasks;
using EKrumynas.Data;
using EKrumynas.DTOs.User;
using EKrumynas.Models;
using EKrumynas.Services.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EKrumynas.Services.UserService
{
	public class UserService : IUserService
	{
        private readonly EKrumynasDbContext _context;
        private readonly UserSession userSession;

        public UserService(EKrumynasDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            userSession = new UserSession(httpContextAccessor);
        }

        public async Task<User> Get()
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == userSession.UserId);
        }

        public async Task<User> Update(UserUpdateDto user)
        {
            User foundUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == userSession.UserId);

            if (user.MergeAll)
            {
                foundUser.FirstName = user.FirstName;
                foundUser.LastName = user.LastName;
                if (user.Username != null) foundUser.Username = user.Username;
                foundUser.ProfileImage = user.ProfileImage;
                foundUser.Country = user.Country;
                foundUser.Street = user.Street;
                foundUser.AddressLine1 = user.AddressLine1;
                foundUser.AddressLine2 = user.AddressLine2;
            } else {
                if (user.FirstName != null) foundUser.FirstName = user.FirstName;
                if (user.LastName != null) foundUser.LastName = user.LastName;
                if (user.Username != null) foundUser.Username = user.Username;
                if (user.ProfileImage != null) foundUser.ProfileImage = user.ProfileImage;
                if (user.Country != null) foundUser.Country = user.Country;
                if (user.Street != null) foundUser.Street = user.Street;
                if (user.AddressLine1 != null) foundUser.AddressLine1 = user.AddressLine1;
                if (user.AddressLine2 != null) foundUser.AddressLine2 = user.AddressLine2;
            }

            if (user.Password != null)
            {
                CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

                foundUser.PasswordHash = passwordHash;
                foundUser.PasswordSalt = passwordSalt;
            }
             
            _context.Users.Update(foundUser);

            await _context.SaveChangesAsync();
            return foundUser;
        }

        public async Task<User> Delete()
        {
            User foundUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == userSession.UserId);

            if (foundUser == null)
            {
                throw new NotImplementedException();
            }

            _context.Remove(foundUser);
            await _context.SaveChangesAsync();

            return foundUser;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}

