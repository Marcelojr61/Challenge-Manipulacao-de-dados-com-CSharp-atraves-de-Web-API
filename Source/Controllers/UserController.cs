using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _service;
        private IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;

                
        }

        // GET api/user
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAll(string accelerationName = null, int? companyId = null)
        {
            if (accelerationName != null)
            {
                return Ok
                    (_mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>
                    (_service.FindByAccelerationName
                    (accelerationName)));
            }
            else if (companyId != null)
            {
                return Ok
                    (_mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>
                    (_service.FindByCompanyId
                    ((int)companyId)));
            } else 
            {
                return NoContent();
            }
        }

        // GET api/user/{id}
        [HttpGet("{id}")]
        public ActionResult<UserDTO> Get(int id)
        {
            return Ok
                (_mapper.Map<User, UserDTO>
                (_service.FindById
                (id)));
        }

        // POST api/user
        [HttpPost]
        public ActionResult<UserDTO> Post([FromBody] UserDTO value)
        {
            return Ok
                (_mapper.Map<User, UserDTO>
                (_service.Save
                (_mapper.Map<UserDTO, User>
                (value))));
        }

}
}
