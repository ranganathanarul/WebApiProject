using Amazon.DynamoDBv2.DataModel;
//using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSwagger.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private static readonly string[] empStatusValue = new[]
        {
            "Active", "Deactive", "InProgress", "Onboarded", "Status", "Backups", "Delete"
        };

        private readonly ILogger<EmployeeController> _logger;
        private readonly IDynamoDBContext _dynamoDBContext;

        public EmployeeController(IDynamoDBContext dynamoDBContext, ILogger<EmployeeController> logger)
        {
            _dynamoDBContext = dynamoDBContext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            var student = await _dynamoDBContext.ScanAsync<tblEmployee>(default).GetRemainingAsync();
            return Ok(student);
        }

        [HttpGet("{empId}")]
        public async Task<IEnumerable<tblEmployee>> GetValues(int empId = 3)
        {
            return await _dynamoDBContext
                             .QueryAsync<tblEmployee>(empId)
                             .GetRemainingAsync();
        }


        [HttpPost]
        public async Task<IActionResult> PostValue(string empStatus)
        {
            var empDataRequest = GenerateDummyEmployeeData(empStatus);
            if (empDataRequest != null) return BadRequest($"Employee with status { empStatus } Already Exists");
            foreach (var item in empDataRequest)
            {
                await _dynamoDBContext.SaveAsync(item);
            }
            return Ok(empDataRequest);
        }

        [HttpDelete("{empId}")]
        public async Task<IActionResult> DeleteValue(int empId)
        {
            var empDataRequest = await _dynamoDBContext.LoadAsync<tblEmployee>(empId);
            if (empDataRequest == null) return NotFound();
            await _dynamoDBContext.DeleteAsync(empDataRequest);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(int empId)
        {
            var empDataRequest = await _dynamoDBContext.LoadAsync<tblEmployee>(empId);
            if (empDataRequest == null) return NotFound();
            await _dynamoDBContext.SaveAsync(empDataRequest);
            return Ok(empDataRequest);
        }

        [HttpGet]
        private static IEnumerable<tblEmployee> GenerateDummyEmployeeData(string empStatus)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new tblEmployee
            {
                empId = 1,
                empNumber = 1003 + rng.Next(),
                empCity="Danbury",
                empFName="Anitha " + rng.Next(),
                empLName="Ranganathan " + +rng.Next(),
                empPhone="9253485413",
                isempActive=true,
                empStatus = empStatusValue[rng.Next(empStatusValue.Length)]
            })
            .ToArray();
        }
     }
}
