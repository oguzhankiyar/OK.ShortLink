using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OK.ShortLink.Api.Requests;
using OK.ShortLink.Common.Models;
using OK.ShortLink.Core.Managers;
using System.Collections.Generic;

namespace OK.ShortLink.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get(int page = 1)
        {
            List<UserModel> users = _userManager.GetUsers(15, page);

            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            UserModel user = _userManager.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserRequest request)
        {
            UserModel user = _userManager.CreateUser(CurrentUserId.Value,
                                                     request.Username,
                                                     request.Password,
                                                     request.IsActive);

            if (user == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, null);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] EditUserRequest model)
        {
            bool isEdited = false;

            if (string.IsNullOrEmpty(model.Password))
            {
                isEdited = _userManager.EditUserActivation(CurrentUserId.Value, model.Id, model.IsActive.Value);
            }
            else if (model.IsActive == null)
            {
                isEdited = _userManager.EditUserPassword(CurrentUserId.Value, model.Id, model.Password);
            }
            else
            {
                isEdited = _userManager.EditUser(CurrentUserId.Value, model.Id, model.Password, model.IsActive.Value);
            }

            if (!isEdited)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool isDeleted = _userManager.DeleteUser(CurrentUserId.Value, id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}