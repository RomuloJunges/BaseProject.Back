using BaseProject.Domain.DTO.UserDTO;
using Microsoft.AspNetCore.Identity;

namespace BaseProject.Service.Interfaces
{
    public interface IUserService : INotificationService
    {
        Task<UserDTO> Add(CreateUserDTO userDto);
        Task<UserDTO> Update(UserDTO userDto);
        Task<bool> Delete(Guid id);
        Task<List<UserDTO>> GetAll();
        Task<UserDTO> Get(Guid id);
        Task<UserDTO> GetByEmail(string email);
        Task<SignInResult> CheckUserPassword(LoginUserDTO loginUserDto);
    }
}
