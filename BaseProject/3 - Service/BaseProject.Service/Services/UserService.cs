using AutoMapper;
using BaseProject.Data.Interface;
using BaseProject.Domain.DTO.UserDTO;
using BaseProject.Domain.Identity;
using BaseProject.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Service.Services
{
    public class UserService : NotificationService, IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(IUnitOfWork uow, IUserRepository userRepository,
                            IMapper mapper, UserManager<User> userManager,
                            SignInManager<User> signInManager)
        {
            _uow = uow;
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<UserDTO> Add(CreateUserDTO userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                user.UserName = user.FirstName;
                var result = await _userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded) return _mapper.Map<UserDTO>(user);

                AddNotification("Error on create user.");
                return null;
            }
            catch (Exception ex)
            {
                AddNotification("Error on create user.");
                return null;
            }
        }

        public async Task<UserDTO> Update(UserDTO userDto)
        {
            try
            {
                var user = await _userRepository.GetById(userDto.Id);
                if (user == null)
                {
                    AddNotification("Error on update user.");
                    return null;
                }

                _mapper.Map(userDto, user);

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, userDto.Password);

                _userRepository.Update<User>(user);
                await _uow.Commit();
                return _mapper.Map<UserDTO>(await _userRepository.GetById(userDto.Id));
            }
            catch (Exception ex)
            {
                AddNotification("Error on update user.");
                return null;
            }
        }

        public Task<bool> Delete(Guid id)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                AddNotification("Error on delete user.");
                return null;
            }
        }

        public async Task<List<UserDTO>> GetAll()
        {
            try
            {
                var users = _userRepository.GetAll<User>();
                return _mapper.Map<List<UserDTO>>(users);
            }
            catch (Exception ex)
            {
                AddNotification("Error on get all user.");
                return null;
            }
        }

        public async Task<UserDTO> Get(Guid id)
        {
            try
            {
                var user = await _userRepository.GetById(id);
                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception ex)
            {
                AddNotification("Error on get user.");
                return null;
            }
        }

        public async Task<UserDTO> GetByEmail(string email)
        {
            try
            {
                var user = await _userRepository.GetByEmail(email);
                if (user == null)
                {
                    AddNotification("Error on get user.");
                    return null;
                }

                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception ex)
            {
                AddNotification("Error on get user.");
                return null;
            }
        }

        public async Task<SignInResult> CheckUserPassword(LoginUserDTO loginUserDto)
        {
            try
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(user =>
                    user.Email.ToLower() == loginUserDto.Email.ToLower());

                if (user == null) return null;

                return await _signInManager.CheckPasswordSignInAsync(user, loginUserDto.Password, false);

            }
            catch (Exception e)
            {
                AddNotification("Error on check user password.");
                return null;
            }
        }
    }
}
