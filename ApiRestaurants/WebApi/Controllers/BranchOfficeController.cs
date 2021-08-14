using DTOs.Restaurant;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Errors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchOfficeController : ControllerBase
    {
        private readonly IBranchOfficeService _branchOfficeService;

        public BranchOfficeController(IBranchOfficeService branchOfficeService)
        {
            _branchOfficeService = branchOfficeService;
        }
        // GET: api/<BranchOfficeController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<BranchOfficeController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<BranchOfficeController>
        [HttpPost("BranchOffice")]
        public async Task<IActionResult> Create(BranchOfficeRequestDto branchOfficeRequestDto)
        {
            var response = await _branchOfficeService.Create(branchOfficeRequestDto);

            return Ok(response);
        }

        // PUT api/<BranchOfficeController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<BranchOfficeController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
