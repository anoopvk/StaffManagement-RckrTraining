using StaffManagement.Lib.Model;
using StaffManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaffManagement.Interface;

namespace StaffManagement
{

    static class ConsoleStaffManager
    {
        private static int s_newId = 1;
        private static IStaffRepository staffRepository = new InMemoryStaffRepository();

        static string GetNameOfStaff()
        {
            Console.WriteLine("enter name");
            return Console.ReadLine();
        }


        static string GetSectionOfStaff()
        {
            Console.WriteLine("enter Section");
            return Console.ReadLine();
        }


        static List<string> GetSubjectsHandledOfStaff()
        {
            List<string> subjectsHandled = new List<string>();
            Console.WriteLine("enter number of subjects handled");
            int numberOfSubjectsHandled = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter subject names : ");
            for (int i = 0; i < numberOfSubjectsHandled; i++)
            {
                subjectsHandled.Add(Console.ReadLine());
            }
            return subjectsHandled;
        }


        static string GetBuildingOfStaff()
        {
            Console.WriteLine("enter Building");
            return Console.ReadLine();
        }


        static Staff CreateStaffThroughUserInput(int newid)
        {
            String nameOfStaff = GetNameOfStaff();


            Console.WriteLine("enter 1 for Administrative staff ");
            Console.WriteLine("enter 2 for Teaching staff ");
            Console.WriteLine("enter 3 for Support staff");
            int staffTypeKey = Convert.ToInt32(Console.ReadLine());

            if (staffTypeKey == 1) // administrative staff
            {
                string sectionOfStaff = GetSectionOfStaff();
                return (new AdministrativeStaff(newid, nameOfStaff, sectionOfStaff));
            }
            else if (staffTypeKey == 2) // teaching staff
            {
                List<string> subjectsHandled = GetSubjectsHandledOfStaff();
                return (new TeachingStaff(newid, nameOfStaff, subjectsHandled));
            }
            else if (staffTypeKey == 3) // support staff
            {
                string supportStaffBuilding = GetBuildingOfStaff();
                return (new SupportStaff(newid, nameOfStaff, supportStaffBuilding));
            }
            else
            {
                Console.WriteLine("invalid choice");
                return null;
            }
        }


        public static int GetIdFromUser()
        {
            int tempId;
            Console.WriteLine("enter the id of the staff");
            tempId = Convert.ToInt32(Console.ReadLine());
            return tempId;
        }


        public static void AddNewStaff()
        {
            Staff newStaff = CreateStaffThroughUserInput(s_newId);
            staffRepository.AddStaff(newStaff);
            s_newId++;
        }
        public static void ViewAllStaff()
        {
            List<Staff> listOfAllStaff = staffRepository.GetAllStaff();
            if (listOfAllStaff == null)
            {
                Console.WriteLine("empty");
            }
            foreach (Staff s in listOfAllStaff)
            {
                Console.WriteLine(s);
            }
        }
        public static void ViewOneStaff()
        {
            int userInputIdForSearching = GetIdFromUser();
            Staff staffFromSearch = staffRepository.GetStaff(userInputIdForSearching);
            if (staffFromSearch == null)
            {
                Console.WriteLine("not found");
            }
            Console.WriteLine(staffFromSearch);
        }

    }
}
