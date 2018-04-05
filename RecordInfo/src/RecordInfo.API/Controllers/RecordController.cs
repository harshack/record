using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Linq.Expressions;
using RecordInfo.Services;
using RecordInfo.DTO;

namespace RecordInfo.API.Controllers
{
    
    /// <summary>
    /// Record controller which has GET / POST features 
    /// </summary>
    [Route("api/records")]
    public class RecordController : Controller
    {
        private ILogger<RecordController> _logger;
        private IRecordService _recordService;
        public RecordController(ILogger<RecordController> logger,
            IRecordService record)
        {
            _logger = logger;
            _recordService = record;
        }

        /// <summary>
        /// GET Feature to retrieve and sort record by gender.
        /// </summary>
        /// <returns></returns>
        [HttpGet("gender")]
        public async Task<IActionResult> GeRecordSortedByGender()
        {
            try
            {
                List<RecordDto> records = _recordService.Get().OrderBy(a => a.Gender).ToList(); 

                if (records == null)
                {
                    _logger.LogInformation($"Yielded No records during Gender sort ");
                    return NotFound();
                }

                return Ok(records);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception during Gender sort.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        /// <summary>
        /// GET Feature to retrieve and sort record by DOB. 
        /// </summary>
        /// <returns></returns>
        [HttpGet("birthdate")]
        public async Task<IActionResult> GeRecordSortedByBirthDate()
        {
            IEnumerable<RecordDto> records = _recordService.Get().OrderBy(a => a.DOB).ToList();

            if (records == null)
            {
                _logger.LogInformation($"Yielded No records during birth date sort ");
                return NotFound();
            }

            return Ok(records);
        }

        /// <summary>
        /// GET Feature to retrieve and sort record by Name. 
        /// </summary>
        /// <returns></returns>
        [HttpGet("name")]
        public async Task<IActionResult> GeRecordSortedByName()
        {
            IEnumerable<RecordDto> records = _recordService.Get().OrderBy(a => a.FirstName).ThenBy(a => a.LastName).ToList();

            if (records == null)
            {
                _logger.LogInformation($"Yielded No records during name sort ");
                return NotFound();
            }

            return Ok(records);
        }

        /// <summary>
        /// POST Feature to create record. This is tightly coupled input body. 
        /// </summary>
        /// <param name="StructureRecord"></param>
        /// <returns></returns>
        [HttpPost("createstructuredrecords")]
        public async Task<IActionResult> CreateRecords([FromBody] RecordDto StructureRecord)
        {
            if (StructureRecord == null)
            {
                _logger.LogInformation($"bad data durinng  creating structured records ");
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _recordService.Add(StructureRecord);
            //RecordsDataStore.Current.Records.Add(finalRecord);

            return Ok(true);
        }

        /// <summary>
        /// POST Feature to create record. This is loosely coupled input body.
        /// </summary>
        /// <param name="flatrecord"></param>
        /// <returns></returns>
        [HttpPost("createflatrecords")]
        public async Task<IActionResult> CreateRecords([FromBody]string flatrecord)
        {
            if (flatrecord == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (flatrecord.Contains(","))
            {
                string[] comma = flatrecord.Split(',');
                InsertRecord(comma);
            }

            if (flatrecord.Contains("\t"))
            {
                string[] tabs = flatrecord.Split('\t');
                InsertRecord(tabs);
            }

            if (flatrecord.Contains("|"))
            {
                string[] pipes = flatrecord.Split('|');
                InsertRecord(pipes);
            }

            return Ok();

        }

        /// <summary>
        /// Private method to send request to service layer
        /// </summary>
        /// <param name="User"></param>
        private void InsertRecord(string[] User)
        {

            var finalRecord = new RecordDto()
            {
                FirstName = User[0],
                LastName = User[1],
                DOB = Convert.ToDateTime(User[2].ToString()),
                Color = User[4],
                Gender = User[3]
            };

            _recordService.Add(finalRecord);

        }
    }
}
