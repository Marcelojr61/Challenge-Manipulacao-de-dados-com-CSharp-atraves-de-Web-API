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
    public class CandidateController : ControllerBase
    {
        private ICandidateService _service;
        private IMapper _mapper;

        public CandidateController(ICandidateService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{userId}/{accelerationId}/{companyId}")]
        public ActionResult<CandidateDTO> Get(int userId, int accelerationId, int companyId)
        {
            return Ok
                (_mapper.Map<Candidate, CandidateDTO>
                (_service.FindById
                (userId, accelerationId, companyId)));
        }

        public ActionResult<IEnumerable<CandidateDTO>> GetAll(int? accelerationId = null, int? companyId = null)
        {
            if (accelerationId != null)
            {
                return Ok
                    (_mapper.Map<IEnumerable<Candidate>, IEnumerable<CandidateDTO>>
                    (_service.FindByAccelerationId
                    ((int)accelerationId))
                    .ToList());
            }
            else if (companyId != null)
            {
                return Ok
                    (_mapper.Map<IEnumerable<Candidate>, IEnumerable<CandidateDTO>>
                    (_service.FindByCompanyId
                    ((int)companyId))
                    .ToList());
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public ActionResult<CandidateDTO> Post([FromBody] CandidateDTO value)
        {
            return Ok
                (_mapper.Map<Candidate, CandidateDTO>
                (_service.Save
                (_mapper.Map<CandidateDTO, Candidate>
                (value))));
        }

    }
}
