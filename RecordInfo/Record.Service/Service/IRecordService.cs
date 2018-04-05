using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecordInfo.DTO;

namespace RecordInfo.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRecordService
    {
        Boolean Add(RecordDto record);

        Boolean Add(string[] User);

        List<RecordDto> Get();
    }
}
