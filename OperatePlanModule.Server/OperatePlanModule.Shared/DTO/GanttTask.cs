using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatePlanModule.Shared.DTO
{
    public class GanttTask
    {
        public int IdI { get; set; }
        public int IdJ { get; set; }
        public string Machine { get; set; }
        public string Part { get; set; }
        public string Start { get; set; }
        public DateTime StartDate { get; set; }
        public string End { get; set; }
        public DateTime EndDate { get; set; }

    }
    public class GanttList
    {
        public List<GanttTask> GanttTasks { get; set; }
        public string Keys { get; set; }
        public double AllTime { get; set; }
        public string MethodName { get; set; }
    }
}
