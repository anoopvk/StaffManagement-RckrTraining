using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StaffManagement.API.Dtos
{
    public class CreateStaffDto
    {
        [Required]
        public string Name { get; set; }

        public int StaffType { get; set; }

        public string Section { get; set; }

        public string Building { get; set; }

        public string SubjectHandled { get; set; }
    }
}
