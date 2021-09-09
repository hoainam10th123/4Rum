using AutoMapper;
using ForumAppCore.DTOs;
using ForumAppCore.Entities;
using ForumAppCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private IMapper _mapper;
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        //api/account/register?username=Test&password=hoainam10th with Register(string username, string password)
        public async Task<ActionResult<UserDto>> Register(RegisterDto register)
        {
            if (await UserExists(register.UserName))
                return BadRequest("Username đã tồn tại");

            var user = _mapper.Map<AppUser>(register);

            user.UserName = register.UserName.ToLower();

            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "User");
            if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

            var userDto = new UserDto
            {
                UserName = user.UserName,
                DisplayName = user.DisplayName,
                DateOfBirth = user.DateOfBirth.GetValueOrDefault(),
                LastActive = user.LastActive,
                Token = await _tokenService.CreateTokenAsync(user),
                PhotoUrl = null
            };

            return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users
                //.Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());

            if (user == null)
                return Unauthorized("Invalid Username");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Invalid password");

            var userDto = new UserDto
            {
                UserName = user.UserName,
                DisplayName = user.DisplayName,
                DateOfBirth = user.DateOfBirth.GetValueOrDefault(),
                LastActive = user.LastActive,
                Token = await _tokenService.CreateTokenAsync(user),
                PhotoUrl = user.PhotoUrl
            };
            return Ok(userDto);
        }

        [HttpPost("login-social")]
        public async Task<ActionResult<UserDto>> LoginSocial(LoginSocialDto loginDto)
        {
            var user = await _userManager.Users
                .SingleOrDefaultAsync(x => x.UserName == loginDto.Email);
            // email = username
            if (user != null)//có rồi thì đăng nhập bình thường
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Email, false);

                if (!result.Succeeded) return Unauthorized("Invalid password");

                var userDto = new UserDto
                {
                    UserName = user.UserName,
                    DisplayName = user.DisplayName,
                    DateOfBirth = user.DateOfBirth.GetValueOrDefault(),
                    LastActive = user.LastActive,
                    Token = await _tokenService.CreateTokenAsync(user),
                    PhotoUrl = user.PhotoUrl
                };
                return Ok(userDto);
            }
            else//Chưa có thì tạo mới user
            {
                var appUser = new AppUser
                {
                    UserName = loginDto.Email,
                    Email = loginDto.Email,
                    DisplayName = loginDto.Name,
                    PhotoUrl = loginDto.PhotoUrl,
                    DateOfBirth = DateTime.Now
                };

                var result = await _userManager.CreateAsync(appUser, loginDto.Email);//password là email

                if (!result.Succeeded) return BadRequest(result.Errors);

                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

                var userDto = new UserDto
                {
                    UserName = appUser.UserName,
                    DisplayName = appUser.DisplayName,
                    DateOfBirth = appUser.DateOfBirth.GetValueOrDefault(),
                    LastActive = appUser.LastActive,
                    Token = await _tokenService.CreateTokenAsync(appUser),
                    PhotoUrl = loginDto.PhotoUrl
                };

                return Ok(userDto);
            }            
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}
