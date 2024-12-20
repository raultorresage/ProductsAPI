﻿using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Attributes;
using ProductsAPI.Models;
using ProductsAPI.Services;

namespace ProductsAPI.Controllers;

[ApiController]
[Route("api/user")]
[Consumes("application/json")]
[Produces("application/json")]
[Tracker]
public class UserController(UserServices userServices) : ControllerBase
{
    [HttpPost("login", Name = "LogInUser")]
    public async Task<IActionResult> LogIn([FromBody] User vU)
    {
        var u = await userServices.LogIn(vU);
        if (u == null) return NotFound("User not found");
        return Ok(userServices.GenerateJwtToken(vU));
    }

    [HttpPost("register", Name = "RegisterUser")]
    public async Task<IActionResult> Register([FromBody] User vU)
    {
        var u = await userServices.Register(vU);
        if (u == null) return BadRequest("UAE");
        return Ok(u);
    }
}

// Custom attribute on endpoint like a middleware to explicit log the endpoint is using
// On especific endpoint, on a method
// Middleware that look to the route and if it has this attribute prints something