using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        private IChallengeService _service;
        private IMapper _mapper;

        public ChallengeController(IChallengeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ChallengeDTO>> GetAll(int? accelerationId = null, int? userId = null)
        {

            if (accelerationId != null && userId != null)
            { 
                return Ok
                    (_mapper.Map<IEnumerable<Models.Challenge>, IEnumerable<ChallengeDTO>>
                    (_service.FindByAccelerationIdAndUserId
                    ((int)accelerationId, (int)userId)));
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public ActionResult<ChallengeDTO> Post([FromBody] ChallengeDTO value)
        {
            return Ok
                (_mapper.Map<Models.Challenge, ChallengeDTO>
                (_service.Save
                (_mapper.Map<ChallengeDTO, Models.Challenge>
                (value))));
        }








    }
}
