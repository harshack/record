using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecordInfo.DTO;

namespace RecordInfo.Data
{ 
    /// <summary>
    /// Static data layer
    /// </summary>
    public class RecordData : IRecordData
    {
        public static RecordData Current { get; } = new RecordData();
        public List<RecordDto> Records { get; set; }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="User"></param>
        public void InsertRecord(string[] User)
        {
            var record = Current.Records.LastOrDefault();


            var finalRecord = new RecordDto()
            {
                Id = ++record.Id,
                FirstName = User[0],
                LastName = User[1],
                DOB = Convert.ToDateTime(User[2].ToString()),
                Color = User[3],
                Gender = User[4]
            };

            RecordData.Current.Records.Add(finalRecord);
        }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="User"></param>

        public void InsertRecord(RecordDto User)
        {
            var record = Current.Records.LastOrDefault();


            User.Id = ++record.Id;

            RecordData.Current.Records.Add(User);
        }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>

        public List<RecordDto> GetAll()
        {
            return RecordData.Current.Records.AsQueryable().ToList();

        }

    /// <summary>
    /// 
    /// </summary>
        public RecordData()
        {
            // init dummy data
            Records = new List<RecordDto>()
            {
                new RecordDto()
                {
                    FirstName="Default" ,
                    LastName ="Harsha" ,
                    DOB = new DateTime(1981, 7, 20) ,
                    Color ="Brown" ,
                    Gender = "Male"
                },
                new RecordDto()
                {
                    FirstName="Default" ,
                    LastName ="Duvi" ,
                    DOB = new DateTime(2011, 11, 17) ,
                    Color ="Brown" ,
                    Gender = "Female"
                }
                ,
                new RecordDto()
                {

                    FirstName="Default" ,
                    LastName ="Abhi" ,
                    DOB = new DateTime(1960, 11, 17) ,
                    Color ="Wheatish" ,
                    Gender = "Male"
                }
                ,
                new RecordDto()
                {

                    FirstName="Default" ,
                    LastName ="Buvi" ,
                    DOB = new DateTime(1970, 11, 17) ,
                    Color ="Wheatish" ,
                    Gender = "Male"
                }
            };

        }
    }
}
