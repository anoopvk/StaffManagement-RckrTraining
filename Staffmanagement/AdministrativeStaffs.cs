using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaffManagement.Lib.Model;

namespace Staffmanagement
{
    class AdministrativeStaffs: IStaffs
    {
        
        //string nameOfStaff;
        //string sectionOfStaff;
        List<AdministrativeStaff> adminstaffs = new List<AdministrativeStaff>();

        public void AddStaff(int newid)
        {
            Console.WriteLine("enter name");
            string nameOfStaff = Console.ReadLine();
            Console.WriteLine("enter section");
            string sectionOfStaff = Console.ReadLine();
            AdministrativeStaff newStaff = new AdministrativeStaff(newid, nameOfStaff, sectionOfStaff);
            adminstaffs.Add(newStaff);
            Console.WriteLine("staff successfully created!!! \n");
        }

        public void ViewStaff(int searchingStaffId = -1) 
        {
            if (adminstaffs.Count==0)
            {
                Console.WriteLine("....empty....");
                return;
            }

            if (searchingStaffId == -1)
            {
                //display all staff
                Console.WriteLine("\n-------------------------------------------------------------");
                foreach (AdministrativeStaff astaff in adminstaffs)
                {
                    astaff.PrintDetails();
                }
                Console.WriteLine("-------------------------------------------------------------\n");

            }
            else
            {
                // view specific staff
                AdministrativeStaff tempStaff = adminstaffs.Find(x => x.id == searchingStaffId);
                if (tempStaff == null)
                {
                    Console.WriteLine("not found in administrative staff list");
                    return;
                }

                Console.WriteLine("\n-------------------------------------------------------------");
                tempStaff.PrintDetails();
                Console.WriteLine("-------------------------------------------------------------\n");
            }
        }

        public void UpdateStaff(int id) 
        {
            if (adminstaffs.Count == 0)
            {
                Console.WriteLine("....empty....");
                return;
            }
            AdministrativeStaff tempStaff = adminstaffs.Find(x => x.id == id);
            if (tempStaff == null)
            {
                Console.WriteLine("not found in administrative staff list");
                return;
            }
            Console.WriteLine("enter name");
            tempStaff.name = Console.ReadLine();
            Console.WriteLine("enter section");
            tempStaff.section = Console.ReadLine();
            Console.WriteLine(" Administrative staff details updated! ");
        }

        public void DeleteStaff(int id) 
        {
            if (adminstaffs.Count == 0)
            {
                Console.WriteLine("....empty....");
                return;
            }
            AdministrativeStaff tempStaff = adminstaffs.Find(x => x.id == id);
            if (tempStaff == null)
            {
                Console.WriteLine("not found in administrative staff list");
                return;
            }
            adminstaffs.Remove(tempStaff);
        }
    }
}
