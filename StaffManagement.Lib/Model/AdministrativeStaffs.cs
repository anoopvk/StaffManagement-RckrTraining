using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaffManagement.Lib.Model;

namespace Staffmanagement
{
    public class AdministrativeStaffs: IStaffs
    {
        
        //string nameOfStaff;
        //string sectionOfStaff;
        public List<AdministrativeStaff> adminstaffs = new List<AdministrativeStaff>();

        public void AddStaff(Staff s)
        {
            adminstaffs.Add((AdministrativeStaff)s);
        }

        public string ViewStaff(int searchingStaffId = -1) 
        {
            string result = "";
            if (adminstaffs.Count==0)
            {
                return ("....empty....");
            }

            if (searchingStaffId == -1)
            {
                //display all staff
                result +="\n-------------------------------------------------------------\n";
                foreach (AdministrativeStaff astaff in adminstaffs)
                {
                    result+=astaff.GetDetails();
                    result += "\n";
                }
                result+="-------------------------------------------------------------\n";
                return result;
            }
            else
            {
                // view specific staff
                AdministrativeStaff tempStaff = adminstaffs.Find(x => x.id == searchingStaffId);
                if (tempStaff == null)
                {
                    result+="not found in administrative staff list";
                    return result;
                }

                result+="\n-------------------------------------------------------------\n";
                result+= tempStaff.GetDetails();
                result+="\n-------------------------------------------------------------\n";
                return result;
            }
        }

        public bool UpdateStaff(int id,Staff s) 
        {
            if (adminstaffs.Count == 0)
            {
                //Console.WriteLine("....empty....");
                return false;
            }
            AdministrativeStaff tempStaff = adminstaffs.Find(x => x.id == id);
            if (tempStaff == null)
            {
                //Console.WriteLine("not found in administrative staff list");
                return false;
            }
            adminstaffs[adminstaffs.IndexOf(tempStaff)] = (AdministrativeStaff)s;
            return true;
            //Console.WriteLine("enter name");
            //tempStaff.name = Console.ReadLine();
            //Console.WriteLine("enter section");
            //tempStaff.section = Console.ReadLine();
            //Console.WriteLine(" Administrative staff details updated! ");
        }

        public bool DeleteStaff(int id) 
        {
            if (adminstaffs.Count == 0)
            {
                //Console.WriteLine("....empty....");
                return false;
            }
            AdministrativeStaff tempStaff = adminstaffs.Find(x => x.id == id);
            if (tempStaff == null)
            {
                //Console.WriteLine("not found in administrative staff list");
                return false;
            }
            adminstaffs.Remove(tempStaff);
            return true;
        }
    }
}
