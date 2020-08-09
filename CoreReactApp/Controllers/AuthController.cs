using System;
using System.Collections.Generic;
using System.Linq;
using CoreReactApp.Auth;
using CoreReactApp.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreReactApp.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _userService;

        public AuthController(IAuthService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(string login, string password)
        {
            var response = _userService.Authenticate(login, password);
            if (response == null)
            {
                throw new ApplicationException("Неверный логин или пароль");
            }
            return Ok(response);
        }

        [HttpPost("checkAccess")]
        [Authorize]
        public IActionResult CheckAccess([FromBody] List<RoleEnum> roles)
        {
            //если роли не указаны, проверку что пользователь просто авторизован, выполнит атрибут
            if (!roles.Any())
            {
                return Ok(true);
            }
            //если проверка не пройдена, возвращаю не Ok(false), а Forbid
            return roles.Any(x => HttpContext.User.IsInRole(x.ToString()))
                ? Ok(true)
                : (IActionResult)Forbid();
        }
    }
}
