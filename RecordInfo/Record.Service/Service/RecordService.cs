using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using RecordInfo.Data;
using RecordInfo.DTO;

namespace RecordInfo.Services
{
    /// <summary>
    ///  Service which can take operate different repository
    /// </summary>
    public class RecordService : IRecordService
    {
        private IRecordData _datalayer;
        public RecordService(IRecordData record)
        {
            _datalayer = record;
        }
        public Boolean Add(RecordDto record)
        {
            _datalayer.InsertRecord(record);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public Boolean Add(string[] User)
        {
            var _record = new RecordDto()
            {
                FirstName = User[0],
                LastName = User[1],
                DOB = Convert.ToDateTime(User[2].ToString()),
                Color = User[3],
                Gender = User[4]
            };

            _datalayer.InsertRecord(_record);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<RecordDto> Get()
        {
            return  _datalayer.GetAll();
        }
    }
}
