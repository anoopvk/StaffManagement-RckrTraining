using StaffManagement.API.Dtos;
using StaffManagement.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffManagement.API.Helper
{
    public static class Helper
    {
        public static StaffDto GetStaffDto(Staff staff)
        {
            int staffTypeOfStaff = 0;
            string sectionOfStaff = null;
            string buildingOfStaff = null;
            string subjectHandledOfStaff = null;

            if (staff.GetType() == typeof(AdministrativeStaff))
            {
                staffTypeOfStaff = (int)Staff.TypesOfStaff.AdministrativeStaff;
                sectionOfStaff = ((AdministrativeStaff)staff).Section;
            }

            else if (staff.GetType() == typeof(SupportStaff))
            {
                staffTypeOfStaff = (int)Staff.TypesOfStaff.SupportStaff;
                buildingOfStaff = ((SupportStaff)staff).Building;
            }

            else if (staff.GetType() == typeof(TeachingStaff))
            {
                staffTypeOfStaff = (int)Staff.TypesOfStaff.TeachingStaff;
                subjectHandledOfStaff = ((TeachingStaff)staff).SubjectHandled;

            }

            return new StaffDto()
            {
                Id = staff.Id,
                Name = staff.Name,
                StaffType = staffTypeOfStaff,
                Section = sectionOfStaff,
                Building = buildingOfStaff,
                SubjectHandled = subjectHandledOfStaff
            };


        }

        public static Staff GetStaffObject(CreateStaffDto createStaffDto)
        {
            //need not be unique id if inserting one staff into db as db will autogenerate unique id, otherwise need to figure out how to get unique id here.
            int id = 0;

            if (createStaffDto.StaffType == ((int)Staff.TypesOfStaff.AdministrativeStaff))
            {
                return new AdministrativeStaff(id, createStaffDto.Name, createStaffDto.Section);
            }
            else if (createStaffDto.StaffType == ((int)Staff.TypesOfStaff.SupportStaff))
            {
                return new SupportStaff(id, createStaffDto.Name, createStaffDto.Building);
            }
            else if (createStaffDto.StaffType == ((int)Staff.TypesOfStaff.TeachingStaff))
            {
                return new TeachingStaff(id, createStaffDto.Name, createStaffDto.SubjectHandled);
            }
            else return null;
        }
    }
}
