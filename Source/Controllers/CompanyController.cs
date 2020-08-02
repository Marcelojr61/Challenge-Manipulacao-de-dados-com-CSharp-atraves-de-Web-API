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
    public class CompanyController : ControllerBase
    {
        private ICompanyService _service;
        private IMapper _mapper;

        public CompanyController(ICompanyService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<CompanyDTO> Get (int id)
        {
             return Ok
                (_mapper.Map<Company, CompanyDTO>
                (_service.FindById
                (id)));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CompanyDTO>> GetAll(int? accelerationId = null, int? userId = null)
        {

            if (accelerationId != null)
            {
                return Ok
                    (_mapper.Map<IEnumerable<Company>, IEnumerable<CompanyDTO>>
                    (_service.FindByAccelerationId
                    ((int)accelerationId))
                    .ToList());
            }
            else if (userId != null)
            {
                return Ok
                    (_mapper.Map<IEnumerable<Company>, IEnumerable<CompanyDTO>>
                    (_service.FindByUserId
                    ((int)userId))
                    .ToList());
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public ActionResult<CompanyDTO> Post([FromBody] CompanyDTO value)
        {
            return Ok
                (_mapper.Map<Company, CompanyDTO>
                (_service.Save
                (_mapper.Map<CompanyDTO,Company>
                (value))));
        }
    }
}
