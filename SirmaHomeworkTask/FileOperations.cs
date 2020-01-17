using System;
using System.Collections.Generic;
using System.IO;

namespace SirmaHomeworkTask

{
    public static class FileOperations
    {
        public static IEnumerable<Employee> ReadFile(string path, string dateFormat)
        {
            
            var employees = new List<Employee>();
            
            try
            {
                var inFile = File.ReadLines(path);

                foreach (var line in inFile)
                {
                    var curEmployee = new Employee();

                    if (line.StartsWith("EmpID")) continue;
                    var curLine = line.Split(',');
                    curEmployee.EmpId = int.Parse(curLine[0]);
                    curEmployee.ProjectId = int.Parse(curLine[1]);
                    
                    
                    if( ! DateTime.TryParseExact(curLine[2], dateFormat,
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None, out _)){
                        dateFormat = "yyyy-MM-dd";
                    }

                    curLine[2] = curLine[2].Trim();
                    curLine[3] = curLine[3].Trim();
                    curEmployee.DateFrom = DateTime.ParseExact(curLine[2], dateFormat, System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None);
                    
                    // if there is a valid datetime use it, else use today 
                    var test = DateTime.TryParse(curLine[3], out _);
                    curEmployee.DateTo = test ? DateTime.ParseExact(curLine[3], dateFormat, System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None) : DateTime.Now;

                    employees.Add(curEmployee);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(@"Could not read file");
                Console.WriteLine(e.Message);
            }
            
            return employees;
        }
    }


    public class Employee 
    {
        public int EmpId { get; set; }
        public int ProjectId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
         
    }
}

