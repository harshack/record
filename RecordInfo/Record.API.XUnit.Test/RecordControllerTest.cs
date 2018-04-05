using System;
using Xunit;
using RecordInfo.DTO;
using RecordInfo.API;
using RecordInfo.API.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using RecordInfo.Services;
using RecordInfo.Data;

namespace Record.API.XUnit.Test
{

    /// <summary>
    /// 
    /// </summary>
    public class RecordControllerTest
    {
        private readonly RecordController _recordTestController;

        private ILogger<RecordController> _loggerTest;

        RecordService _service = new RecordService(new RecordData());
        public RecordControllerTest()
        {
            
            _recordTestController = new RecordController(_loggerTest, _service);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CreateRecordTest()
        {
            var Message = new RecordDto
            {
                FirstName = "FNTest",
                LastName = "LNTest",
                Color = "White",
                Gender = "MaleTest",
            };


            var result = _recordTestController.CreateRecords(Message).GetAwaiter().GetResult();

            Assert.True(result is OkObjectResult && (bool)(result as OkObjectResult).Value);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void GeRecordByBirthDate()
        {

            var result = _recordTestController.GeRecordSortedByBirthDate().GetAwaiter().GetResult();

            Assert.True(result is OkObjectResult);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void GeRecordByName()
        {

            var result = _recordTestController.GeRecordSortedByName().GetAwaiter().GetResult();

            Assert.True(result is OkObjectResult);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void GeRecordByGender()
        {

            var result = _recordTestController.GeRecordSortedByGender().GetAwaiter().GetResult();

            Assert.True(result is OkObjectResult);
        }
    }
}
