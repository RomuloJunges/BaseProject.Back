using AutoMapper;
using BaseProject.Data.Interface;
using BaseProject.Domain.DTO.UserDTO;
using BaseProject.Domain.Identity;
using BaseProject.Service.Interfaces;
using BaseProject.Service.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Service.Services
{
    public class UserService : IUserService
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

        public async Task<UserDTO> Add(UserDTO userDto)
        {
            try
            {
                UserValidator validator = new(Domain.Enum.ValidationType.Insert);
                await validator.ValidateAndThrowAsync(userDto);

                var user = _mapper.Map<User>(userDto);
                user.UserName = user.FirstName;
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (result.Succeeded) return _mapper.Map<UserDTO>(user);
                throw new Exception("Error on create user.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error on create user. Message: " + ex.Message);
            }
        }

        public async Task<UserDTO> Update(UserDTO userDto)
        {
            try
            {
                UserValidator validator = new(Domain.Enum.ValidationType.Update);
                await validator.ValidateAndThrowAsync(userDto);

                var user = await _userRepository.GetById(userDto.Id);

                if (user == null)
                    throw new Exception("Error on update user.");

                _mapper.Map(userDto, user);

                _userRepository.Update<User>(user);

                if (await _uow.Commit() > 0)
                    return _mapper.Map<UserDTO>(await _userRepository.GetById(userDto.Id));

                throw new Exception("Error on update user.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error on update user. Message: {ex.Message}");
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var user = await _userRepository.GetById(id);
                if (user == null)
                    throw new Exception("Error on delete user.");

                _userRepository.Delete<User>(user);
                if (await _uow.Commit() > 0) return true;
                return false;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error on delete user. Message: {ex.Message}");
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
                throw new Exception($"Error on get all user. Message: {ex.Message}");
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
                throw new Exception($"Error on get user. Message: {ex.Message}");
            }
        }

        public async Task<UserDTO> GetByEmail(string email)
        {
            try
            {
                var user = await _userRepository.GetByEmail(email);
                if (user == null)
                    throw new Exception("Error on get user.");

                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error on get user. Message: {ex.Message}");
            }
        }

        public async Task<SignInResult> CheckUserPassword(LoginUserDTO loginUserDto)
        {
            try
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(user =>
                    user.Email.ToLower() == loginUserDto.Email.ToLower());

                if (user == null)
                    throw new Exception("Email or password invalid.");

                return await _signInManager.CheckPasswordSignInAsync(user, loginUserDto.Password, false);

            }
            catch (Exception ex)
            {
                throw new Exception($"Error on check user password. Message: {ex.Message}");
            }
        }
    }
}
