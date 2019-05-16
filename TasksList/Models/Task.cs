using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksList.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Weight { get; set; }
    }
}
