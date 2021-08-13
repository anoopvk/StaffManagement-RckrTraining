using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StaffManagement.API.Dtos;
using StaffManagement.Data.DbStorage;
using StaffManagement.Lib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using StaffManagement.API.Helper;


namespace StaffManagement.API.Controllers
{
    [ApiController]
    [Route("api/Staffs")]
    public class StaffsController : Controller
    {
        private readonly DbStaffRepository dbStaffRepository;

        public StaffsController()
        {
            dbStaffRepository = new DbStaffRepository();

        }


        // GET /api/Staffs?staffType=abcd
        [HttpGet]
        public ActionResult GetStaffs(string staffType = null)
        {
            IEnumerable<StaffDto> staffs;
            if (staffType == null)
            {


                staffs = dbStaffRepository.GetAllStaff().Select(staffobject => Helper.Helper.GetStaffDto(staffobject));


            }
            else
            {
                int staffTypeCode;
                if (staffType == "AdministrativeStaff")
                {
                    staffTypeCode = (int)Staff.TypesOfStaff.AdministrativeStaff;
                }
                else if (staffType == "SupportStaff")
                {
                    staffTypeCode = (int)Staff.TypesOfStaff.SupportStaff;
                }
                else if (staffType == "TeachingStaff")
                {
                    staffTypeCode = (int)Staff.TypesOfStaff.TeachingStaff;
                }
                else
                {
                    return BadRequest();
                }

                staffs = dbStaffRepository.GetAllStaff().Select(staffobject => Helper.Helper.GetStaffDto(staffobject)).Where(staffobj => staffobj.StaffType == staffTypeCode);


            }
            if (staffs == null || staffs.ToList().Count == 0)
            {
                return NotFound();
            }
            return Ok(staffs);
        }


        //API/staffs/5
        [HttpGet("{id:int}")]
        public ActionResult GetStaffById(int id)
        {
            Staff staff = dbStaffRepository.GetStaff(id);

            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff);
        }



        [HttpPost]
        public ActionResult PostStaff(CreateStaffDto s)
        {
            if (s.StaffType != (int)Staff.TypesOfStaff.AdministrativeStaff && s.StaffType != (int)Staff.TypesOfStaff.SupportStaff && s.StaffType != (int)Staff.TypesOfStaff.TeachingStaff)
            {
                return BadRequest();
            }

            if (s == null)
            {
                return BadRequest();
            }

            Staff staff = Helper.Helper.GetStaffObject(s);
            dbStaffRepository.AddStaff(staff);
            return CreatedAtAction(nameof(PostStaff), staff);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateStaff(int id, UpdateStaffDto s)
        {
            if (s == null)
            {
                return BadRequest();
            }

            Staff existingStaff = dbStaffRepository.GetStaff(id);
            if (existingStaff == null)
            {
                return NotFound();

            }
            
            existingStaff.Name = s.Name ?? existingStaff.Name;


            if (existingStaff.GetType() == typeof(AdministrativeStaff))
            {
                ((AdministrativeStaff)existingStaff).Section = s.Section ?? ((AdministrativeStaff)existingStaff).Section;
            }
            else if (existingStaff.GetType() == typeof(SupportStaff))
            {
                ((SupportStaff)existingStaff).Building = s.Building ?? ((SupportStaff)existingStaff).Building;
            }
            else if (existingStaff.GetType() == typeof(TeachingStaff))
            {
                ((TeachingStaff)existingStaff).SubjectHandled = s.SubjectHandled ?? ((TeachingStaff)existingStaff).SubjectHandled;
            }


            if (dbStaffRepository.UpdateStaff(id, existingStaff))
            {
                return NoContent();
            }
            return BadRequest();

        }

        [HttpDelete("{id}")]
        public ActionResult DeleteStaff(int id)
        {
            if (dbStaffRepository.DeleteStaff(id))
            {
                return NoContent();
            }
            return NotFound();
        }


    }
}
