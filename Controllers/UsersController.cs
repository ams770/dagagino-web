using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dagagino.Dto.AccountDtos;
using Dagagino.Interfaces;
using Dagagino.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Dagagino.Dto.LookupDtos.GovernorateDtos;

namespace Dagagino.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController(IUserRepository userRepo, ISystemLookupRepository lookupRepo) : ControllerBase
    {
        private readonly ISystemLookupRepository _lookupRepo = lookupRepo;
        private readonly IUserRepository _userRepo = userRepo;

        /* -------------------------------------------------------------------------- */
        /*                                 Get Profile                                */
        /* -------------------------------------------------------------------------- */
        [HttpGet("Profile")]
        public async Task<IActionResult> Profile()
        {
            try
            {
                var userId = ((ClaimsIdentity)User.Identity!).FindFirst(ClaimTypes.NameIdentifier)!.Value;

                /* ----------------------------- Get User By Id ----------------------------- */
                var user = await _userRepo.GetById(userId);

                /* ------------------- Get User Governorate State ------------------- */
                GovernorateStateDto? userState = null;
                if (user.GovernorateState != null)
                {
                    userState = await _lookupRepo.GetGovernorateState(user.GovernorateState);
                }


                return user != null ? Ok(new { data = user.ToAccountDtoFromUser(userState) }) : NotFound();
            }
            catch (Exception e)
            {
                return NotFound(e.ToString());
            }

        }

        /* -------------------------------------------------------------------------- */
        /*                               Update Account                               */
        /* -------------------------------------------------------------------------- */
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateAccountDto updateDto)
        {
            try
            {
                /* ----------------------------- Find User Id ---------------------------- */
                var userId = ((ClaimsIdentity)User.Identity!).FindFirst(ClaimTypes.NameIdentifier)!.Value;

                /* --------------------------- Update User Profile -------------------------- */
                var user = await _userRepo.UpdateUser(userId, updateDto);
                /* ------------------- Get User Governorate State ------------------- */
                GovernorateStateDto? userState = null;
                if (user.GovernorateState != null)
                {
                    userState = await _lookupRepo.GetGovernorateState(user.GovernorateState);
                }


                return Ok(new { data = user.ToAccountDtoFromUser(userState) });
            }
            catch
            {
                return NotFound();
            }


        }
    }
}