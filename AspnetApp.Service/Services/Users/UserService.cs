using AspnetApp.Data.IGenericRepositories;
using AspnetApp.Domain.Entities;
using AspnetApp.Domain.Enums;
using AspnetApp.Service.DTOs.Users;
using AspnetApp.Service.Exceptions;
using AspnetApp.Service.Extensions;
using AspnetApp.Service.IServices.Users;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Service.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> userRepository;
        private readonly IMapper mapper;

        public UserService(IGenericRepository<User> userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;   
        }

        public async ValueTask<bool> ChangePasswordAsync(UserForChangePasswordDTO userForChangePasswordDTO)
        {
            var user = await userRepository.GetAsync(u => u.Email == userForChangePasswordDTO.Email);

            if (user == null)
                throw new AspnetAppException(404, "User not found");

            if (user.Password != userForChangePasswordDTO.OldPassword.Encrypt())
                throw new AspnetAppException(400, "Password Incorrect");


            user.Password = userForChangePasswordDTO.NewPassword.Encrypt();

            userRepository.Update(user);
            await userRepository.SaveChangesasync();
            return true;
        }

        public async ValueTask<UserForViewDTO> CreateAsync(UserForCreationDTO userForCreationDTO)
        {
            var alreadyExistUser = await userRepository.GetAsync(u => u.Email == userForCreationDTO.Email);

            if (alreadyExistUser != null)
                throw new AspnetAppException(400, "Email already exist");

            userForCreationDTO.Password = userForCreationDTO.Password.Encrypt();

            var user = await userRepository.CreateAsync(mapper.Map<User>(userForCreationDTO));
            await userRepository.SaveChangesasync();

            return mapper.Map<UserForViewDTO>(user);
        }

        public async ValueTask<bool> DeletesAsync(int id)
        {
            var isDeleted = await userRepository.DeleteAsync(id);
            
            if (!isDeleted)
                throw new AspnetAppException(404, "User not found");
            return true;
        }

        public async ValueTask<IEnumerable<UserForViewDTO>> GetAllAsync(Expression<Func<User, bool>> func = null)
        {
            var users = userRepository.GetAll(func);

            return mapper.Map<IEnumerable<UserForViewDTO>>(users);
        }

        public async ValueTask<UserForViewDTO> GetAsync(Expression<Func<User, bool>> func)
        {
            var user = await userRepository.GetAsync(func);

            if (user is null)
                throw new AspnetAppException(404, "User not found");

            return mapper.Map<UserForViewDTO>(user);
        }

        public async ValueTask<UserForViewDTO> UpdatesAsync(int id, UserForUpdateDTO userForUpdateDTO)
        {
            var existUser = await userRepository.GetAsync(
                u => u.Id == id);

            if (existUser == null)
                throw new AspnetAppException(404, "User not found");

            var alreadyExistUser = await userRepository.GetAsync(
                u => u.Email == userForUpdateDTO.Email && u.Id != id);

            if (alreadyExistUser != null)
                throw new AspnetAppException(400, "Email already exists");


            existUser.LastModifiedTime = DateTime.UtcNow;
            existUser = userRepository.Update(mapper.Map(userForUpdateDTO, existUser));
            
            await userRepository.SaveChangesasync();

            return mapper.Map<UserForViewDTO>(existUser);
        }
        public async ValueTask<bool> ChangeRoleAsync(int id, UserRole userRole)
        {
            var existUser = await userRepository.GetAsync(u => u.Id == id);
            if (existUser == null)
                throw new AspnetAppException(404, "User not found");

            existUser.Role = userRole;

            userRepository.Update(existUser);
            await userRepository.SaveChangesasync();

            return true;
        }
    }
}
