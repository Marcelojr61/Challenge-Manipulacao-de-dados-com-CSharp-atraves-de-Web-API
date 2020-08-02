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
    public class AccelerationController : ControllerBase
    {
        private IAccelerationService _service;
        private IMapper _mapper;

        public AccelerationController(IAccelerationService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{Id}")]
        public ActionResult<AccelerationDTO> Get(int Id)
        {
            return Ok
                (_mapper.Map<Acceleration, AccelerationDTO>
                (_service.FindById(Id)));
        }

        [HttpGet]
        public ActionResult<IEnumerable<AccelerationDTO>> GetAll(int? companyId = null)
        {
            if(companyId != null){
                
                return Ok
                    (_mapper.Map<IEnumerable<Acceleration>, IEnumerable<AccelerationDTO>>
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
        public ActionResult<AccelerationDTO> Post([FromBody] AccelerationDTO value)
        {
            return Ok
                (_mapper.Map<Acceleration, AccelerationDTO>
                (_service.Save
                (_mapper.Map<AccelerationDTO, Acceleration>
                (value))));
        }

    }
}
