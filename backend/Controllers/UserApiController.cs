using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using backend.Models;
using backend.Repositories;

namespace backend.Controllers
{

[ApiController]
[Route("[controller]")]
    public class UserApiController : ControllerBase
    {
        private readonly ILogger<UserApiController> _logger;
        private readonly IUserRepository _userRepository;
        public UserApiController(ILogger<UserApiController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        // public IActionResult Index()
        // {
        //     return View();
        // }

        // public IActionResult Login()
        // {
        //     return View();
        // }

        [HttpPost]
            [Route("Login")]
        public IActionResult Login(UserModel user)
        {
            if (_userRepository.Login(user))
            {
                return Ok("Index");
            }
            else
            {
                return Ok("wrong");
            }

        }

        // public IActionResult Register()
        // {
        //     ViewBag.msg = null;
        //     return View();
        // }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(UserModel user)
        {
            if (!_userRepository.IsUser(user.c_email))
            {

                _userRepository.AddUser(user);

            }
            else
            {

                return Ok("User already exists");

            }
            return Ok("Login");
        }

        
    }
}