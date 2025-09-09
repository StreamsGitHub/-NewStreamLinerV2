using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerViewModelLayer.ModelDTO
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Project_Name { get; set; }
        public DateTime Project_Date { get; set; }
        public bool Closed { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
