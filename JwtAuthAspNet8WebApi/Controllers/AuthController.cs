﻿using JwtAuthAspNet8WebApi.Core.Dtos;
using JwtAuthAspNet8WebApi.Core.Entities;
using JwtAuthAspNet8WebApi.Core.Interfaces;
using JwtAuthAspNet8WebApi.Core.OtherObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthAspNet8WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("seed-roles")]

        public async Task<IActionResult> SeedRoles()
        {
           var seedRoles = await _authService.SeedRolesAsync();
           return Ok(seedRoles);
        }

        //Rotue >>> Register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto registerDto)
        {
           var registerResult = await _authService.RegisterAsync(registerDto);
           if(registerResult.IsSucceed)
            return Ok(registerResult);

           return BadRequest(registerResult);
        }


        //Rotue >>> Login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto loginDto)
        {
            var loginResult = await _authService.LoginAsync(loginDto);
            if (loginResult.IsSucceed)
                return Ok(loginResult);

            return Unauthorized(loginResult);
        }

        

        //Route >>> make user >> admin
        [HttpPost]
        [Route("make-admin")]
        public async Task<IActionResult> MakeAdmin([FromBody] UpdatePermissionDto updatePermissionDto)
        {
           var operationResult = await _authService.MakeAdminAsync(updatePermissionDto);
            if(operationResult.IsSucceed)
                return Ok(operationResult);

            return BadRequest(operationResult);
        }


        //Route >>> make user >> owner
        [HttpPost]
        [Route("make-owner")]
        public async Task<IActionResult> MakeOwner([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var operationResult = await _authService.MakeOwnerAsync(updatePermissionDto);
            if (operationResult.IsSucceed)
                return Ok(operationResult);

            return BadRequest(operationResult);
        }

    }
}
