using AspnetApp.Domain.Entities;
using AspnetApp.Domain.Enums;
using AspnetApp.Service.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Service.IServices.Users
{
    public interface IUserService
    {
        ValueTask<UserForViewDTO> CreateAsync(UserForCreationDTO userForCreationDTO);
        ValueTask<UserForViewDTO> UpdatesAsync(int id,UserForUpdateDTO userForUpdateDTO);
        ValueTask<bool> DeletesAsync(int id);
        ValueTask<UserForViewDTO> GetAsync(Expression<Func<User, bool>> func);
        ValueTask<IEnumerable<UserForViewDTO>> GetAllAsync(Expression<Func<User,bool>> func=null);
        ValueTask<bool> ChangePasswordAsync(UserForChangePasswordDTO userForChangePasswordDTO);
        public ValueTask<bool> ChangeRoleAsync(int id, UserRole userRole);
    }
}
