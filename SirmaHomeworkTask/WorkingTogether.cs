using System;
using System.Collections.Generic;
using System.Linq;

namespace SirmaHomeworkTask
{
    public static class WorkingTogether
    {
        public static LeadingPair Calculate(IEnumerable<Employee> employeesOnProject)
        {
            var result = new LeadingPair();
            var overlapDate = TimeSpan.Zero;
                
            var onProject = employeesOnProject.ToList();
            for (var i = 0; i < onProject.Count(); i++)
            {
                if (onProject.Count() <= 1)
                {
                    return null;
                }

                for (var j = 0; j < onProject.Count(); j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    if (onProject[i].DateFrom == onProject[j].DateFrom &&
                        onProject[i].DateTo == onProject[j].DateTo)
                        overlapDate = onProject[i].DateTo - onProject[i].DateFrom;

                    if (onProject[i].DateFrom > onProject[j].DateFrom &&
                        onProject[i].DateTo > onProject[j].DateTo &&
                        onProject[i].DateFrom < onProject[j].DateTo)
                        overlapDate = onProject[j].DateTo - onProject[i].DateFrom;

                    if (onProject[i].DateFrom < onProject[j].DateFrom &&
                        onProject[i].DateTo > onProject[j].DateFrom)
                        overlapDate = onProject[j].DateTo - onProject[j].DateFrom;

                    if (onProject[i].DateFrom < onProject[j].DateFrom &&
                        onProject[i].DateTo > onProject[j].DateFrom && onProject[i].DateTo < onProject[j].DateTo)
                        overlapDate = onProject[i].DateTo - onProject[j].DateFrom;
                   
                    if (overlapDate > result.TimeWorkedTogether)
                    {
                        result.EmpId1 = onProject[i].EmpId;
                        result.EmpId2 = onProject[j].EmpId;
                        result.ProjectId = onProject[i].ProjectId;
                        result.TimeWorkedTogether = overlapDate;
                    }
                    overlapDate = TimeSpan.Zero;
                }
            }
            return result;
            
        }
    }

    public class LeadingPair
    {
        public int EmpId1 { get; set; }
        public int EmpId2 { get; set; }
        public int ProjectId { get; set; }
        public TimeSpan TimeWorkedTogether { get; set; }        
    }
}