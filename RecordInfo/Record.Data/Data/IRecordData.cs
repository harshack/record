using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecordInfo.DTO;

namespace RecordInfo.Data
{
    public interface IRecordData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="User"></param>
         void InsertRecord(RecordDto User);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="User"></param>
        void InsertRecord(string[] User);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<RecordDto> GetAll();
    }
}
