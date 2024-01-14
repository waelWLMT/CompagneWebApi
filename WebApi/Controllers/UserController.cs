using AutoMapper;
using BL.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi.Dtos;
using WebApi.Helpers;

namespace WebApi.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IStringCryptorDecryptor _stringCryptorDecryptor;
        private readonly IMapper _mapper;
        private IUserService _userService;
        private IRoleService _roleService;
        private ICustomerService _customerService;

        public UserController(IMapper mapper, IUserService  userService,ICustomerService customerService, IStringCryptorDecryptor stringCryptorDecryptor, IRoleService roleService)
        {
            _mapper = mapper;
            _userService = userService;
            _roleService = roleService;
            _stringCryptorDecryptor = stringCryptorDecryptor;
            _customerService = customerService;
        }

        [HttpGet]
        [Route("ActiverDesactivateUser/{userId}/{activate}")]
        public List<UserReadDto> DesactivateUser(int userId, bool activate)
        {
            this._userService.DesactivateUser(userId, activate);

            var users = _userService.GetAll().ToList();
            var userList = _mapper.Map<List<UserReadDto>>(users);
            return userList;
        }

        [HttpGet]
        [Route("MenusByProfile/{roleId}")]
        public List<MenuDto> GetMenusProfileAdmin(int roleId)
        {
            var list = this._roleService.GetMenusByRoleId(roleId).ToList();
            var result = _mapper.Map<List<MenuDto>>(list);

            return result;
        }

        [HttpPost]
        public UserReadDto AddUser(UserCreateDto userCreateDto)
        {
            var userModel = _mapper.Map<User>(userCreateDto);

            userModel.CryptedPassword = _stringCryptorDecryptor.EncryptString(userCreateDto.Password);

            _userService.Insert(userModel);
            _userService.Commit();
            var userReadDto = _mapper.Map<UserReadDto>(userModel);

            return userReadDto;
        }
                
        [HttpGet]
        public List<UserReadDto> GetAllUsers()
        {
            var users = _userService.GetAll().ToList();
            var userList = _mapper.Map<List<UserReadDto>>(users);
            return userList;  
        }

        [HttpGet]
        [Route("{id}")]
        public UserReadDto GetUserById(int id)
        {
            var user = _userService.GetById(id);            
            var userReadDto = _mapper.Map<UserReadDto>(user);

            userReadDto.ClientName = userReadDto.ClientId == 2 ? _customerService.GetById(userReadDto.ClientId).Name : "";

            return userReadDto;
        }

        [HttpPut]
        [Route("{id}")]
        public UserReadDto UpdateCustomer(int id, UserReadDto userReadDto)
        {
            var userModel = _userService.GetById(id);
            
            _mapper.Map(userReadDto, userModel);
            _userService.Commit();

            return userReadDto;           
        }

    }
}
