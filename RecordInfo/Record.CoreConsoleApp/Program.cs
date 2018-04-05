using System;
using RecordInfo.DTO;
using System.IO;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using RecordInfo.Services;
using Microsoft.Extensions.Configuration;
using RecordInfo.Data;

namespace Record.CoreConsoleApp
{
    /// <summary>
    /// Console App
    /// </summary>
    class Program
    {
        public static IConfiguration Configuration { get; set; }
        IRecordService _service;
        
        /// <summary>
        /// Entry Main point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            IRecordData _data = new RecordData();


            RecordService _service = new RecordService(_data);

            while (true)

            {

                Console.WriteLine("Please select Your Options!");

                Console.WriteLine("Hello , you can choose 1-3 to continue with default data.");

                Console.WriteLine("If you want to upload your own data then visit optoin 4");

                Console.WriteLine("Press  1 for sorting by gender (females before males) then by last name ascending.");

                Console.WriteLine("Press  2 for sorting by birth date, ascending.");

                Console.WriteLine("Press  3 for sorting by last name, descending.");

                Console.WriteLine("Press  4 for uploading new records");

                Console.WriteLine("\n");

                ConsoleKeyInfo result = Console.ReadKey();

                Console.WriteLine("\n");

                IEnumerable<RecordDto> records = _service.Get();  //RecordData.Current.Records.ToList();

                switch (result.KeyChar)
                {
                    case '1':
                        {
                            records = _service.Get().OrderBy(a => a.Gender).ThenBy(a => a.LastName).ToList();

                            break;
                        }
                    case '2':
                        {
                            records = _service.Get().AsQueryable().OrderBy(a => a.DOB).ToList();

                            break;
                        }
                    case '3':
                        {
                            records = _service.Get().AsQueryable().OrderByDescending(a => a.LastName).ToList();

                            break;

                        }

                    case '4':
                        {
                            UploadFiles();
                            records = _service.Get().ToList();
                            break;

                        }
                    default:
                        Console.WriteLine("Wrong Selection , Display Record by FirstName Ascending");

                        records = RecordData.Current.Records.AsQueryable().OrderBy(a => a.FirstName).ToList();

                        break;
                }

                Display(records);

                Console.WriteLine("Going for another round");
                Console.WriteLine("\n");
            }
        }

        /// <summary>
        /// Temporary Display on console
        /// </summary>
        /// <param name="sortedRecords"></param>
        public static void Display(IEnumerable<RecordDto> sortedRecords)
        {
            foreach(var record in sortedRecords)
            {
                Console.WriteLine(record.FirstName + "  " + record.LastName + "    " + record.DOB.ToString() + "    " + record.Color + "  " + "    " + record.Gender + "  ");
            }
        }

        /// <summary>
        /// Service call to add new record.
        /// </summary>
        /// <param name="User"></param>
        private void InsertRecord(string[] User)
        {
            _service.Add(User);
        }

        /// <summary>
        /// Upload Feature ; feed any file which is available under console root directory
        /// </summary>
        public static void UploadFiles()
        {
            Console.WriteLine("\n");

            Console.WriteLine("Please select Your Options!");

            Console.WriteLine("Press  1 for Uploading comma Limited File");

            Console.WriteLine("Press  2 for Uploading tab limited File");

            Console.WriteLine("Press  3 for Uploading Pipe limted File");

            Console.WriteLine("Press  4 for Mixed Limited File");

            ConsoleKeyInfo result = Console.ReadKey();

            Console.WriteLine("\n");

            var FileName = string.Empty;

            switch (result.KeyChar)
            {
                case '1':
                    {
                        FileName = Directory.GetCurrentDirectory() + "\\" + Configuration["commafile"];
                           
                        break;
                    }
                case '2':
                    {
                        FileName = Directory.GetCurrentDirectory() + "\\" + Configuration["tabfile"];
                        break;
                    }
                case '3':
                    {
                        FileName = Directory.GetCurrentDirectory() + "\\" + Configuration["pipefile"];
                        break;

                    }

                case '4':
                    {
                        FileName = Directory.GetCurrentDirectory() + "\\" + Configuration["flatfile"];
                        break;

                    }
                default:
                    FileName = Directory.GetCurrentDirectory() + "\\" + Configuration["flatfile"]; 
                    break;
            }


            int counter = 0;
            string line;

            StreamReader file = new StreamReader(new FileStream(FileName, FileMode.Open, FileAccess.Read));

            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(line);

                if (line.Contains(","))
                {
                    string[] comma = line.Split(',');
                    RecordData.Current.InsertRecord(comma);
                    
                }

                if (line.Contains("\t"))
                {
                    string[] tabs = line.Split('\t');
                    RecordData.Current.InsertRecord(tabs);
                }

                if (line.Contains("|"))
                {
                    string[] pipes = line.Split('|');
                    RecordData.Current.InsertRecord(pipes);
                }

                counter++;
            }

            file.Dispose();

            Console.WriteLine("\n");
        }
    }
}
