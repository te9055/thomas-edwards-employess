using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using static SirmaHomeworkTask.FileOperations;
using static SirmaHomeworkTask.WorkingTogether;

namespace SirmaHomeworkTask
{
    public static class Program
    {
        public static LeadingPair App (string fileName, string dateFormat)
        {
            var employees = ReadFile(fileName, dateFormat);

            var enumerable = employees as Employee[] ?? employees.ToArray();
            if (!enumerable.Any()) return null; 
            
            var uniqueProjects = ReadFile(fileName, dateFormat) 
                .GroupBy(i => i.ProjectId)
                .Select(g => g.First())
                .ToList();
            
            var leadingPairEachProject = uniqueProjects.Select(projectId => 
                from employee 
                    in enumerable 
                where employee.ProjectId == projectId.ProjectId 
                select employee).Select(Calculate).Where(curPair => curPair != null).ToList();

            var result = leadingPairEachProject.OrderByDescending(x => x.TimeWorkedTogether).First();

            Console.WriteLine(" Employee number " + result.EmpId1 +  " and Employee number " + 
                              result.EmpId2 + " have worked together the longest.\n They have worked for " +  
                              result.TimeWorkedTogether.Days + " Days together on project " + result.ProjectId);

            
            return result;
        }

    }
}