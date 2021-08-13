using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffManagement.API.Dtos
{
    public class StaffDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int StaffType { get; set; }
        
        public string Section { get; set; }

        public string Building { get; set; }

        public string SubjectHandled { get; set; }


    }
}
