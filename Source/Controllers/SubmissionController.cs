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
    public class SubmissionController : ControllerBase
    {

        private ISubmissionService _service;
        private IMapper _mapper;

        public SubmissionController(ISubmissionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("/higherScore")]
        public ActionResult<SubmissionDTO> GetHigherScore(int? challengeId = null)
        {
           
            if (challengeId != null)
            {
               return Ok
               (_service.FindHigherScoreByChallengeId
               ((int)challengeId));
            }
            else
            {
                return NoContent();
            }
        }

        public ActionResult<IEnumerable<SubmissionDTO>> GetAll(int? challengeId = null, int? accelerationId = null)
        {
  
            if (challengeId != null && accelerationId != null)
            {
                return Ok
                    (_mapper.Map<IEnumerable<Submission>,IEnumerable<SubmissionDTO>>
                    (_service.FindByChallengeIdAndAccelerationId
                    ((int)challengeId, (int)accelerationId)
                    .ToList()));
            }
            else
            {
                return NoContent();
            }
        }


        [HttpPost]
        public ActionResult<SubmissionDTO> Post([FromBody] SubmissionDTO value)
        {
                return Ok
                (_mapper.Map<Submission, SubmissionDTO>
                (_service.Save
                (_mapper.Map<SubmissionDTO,Submission>
                (value))));
        }
    }
}
