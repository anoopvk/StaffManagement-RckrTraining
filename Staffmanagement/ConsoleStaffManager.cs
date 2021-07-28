using StaffManagement.Lib.Model;
using StaffManagement.Data;
using StaffManagement.Data.FileStorage;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaffManagement.Interface;
using System.Configuration;

namespace StaffManagement
{

    static class ConsoleStaffManager
    {
        
        
        //private static IStaffRepository staffRepository = new InMemoryStaffRepository();
        private static IStaffRepository staffRepository = new XMLStaffRepository();
        //private static IStaffRepository staffRepository = new JSONStaffRepository();
        private static int s_newId = GetInitialNextId();


        
       
        private static int GetInitialNextId()
        {
            List<Staff> staffList = staffRepository.GetAllStaff();
            if (staffList.Count == 0)
            {
                return 1;
            }
            int maximumId = 1;
            foreach (var item in staffList)
            {
                maximumId = Math.Max(maximumId, item.Id);
            }

            return maximumId+1;
        }
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

        static string GetUserResponseForQuestion(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }

        static string GetUpdatedNameFromUser(string oldName)
        {
            string userResponse = GetUserResponseForQuestion($"do you want to change the name of the staff({oldName})? Y/N");
            if (userResponse == "Y")
            {
                return GetNameOfStaff();
            }
            return oldName;
        }

        static string GetUpdatedSectionFromUser(string oldSection)
        {
            string userResponse = GetUserResponseForQuestion($"do you want to change the Section of the staff({oldSection})? Y/N");
            if (userResponse == "Y")
            {
                return GetSectionOfStaff();
            }
            return oldSection;
        }

        static List<string> GetUpdatedSubjectsHandledFromUser(List<string> oldSubjectsHandled)
        {
            string oldSubjectsHandledString = "";
            foreach (var item in oldSubjectsHandled)
            {
                oldSubjectsHandledString += (string)item;
            }
            string userResponse = GetUserResponseForQuestion($"do you want to change the Subjects handled of the staff({oldSubjectsHandledString})? Y/N");
            if (userResponse == "Y")
            {
                return GetSubjectsHandledOfStaff();
            }
            return oldSubjectsHandled;
        }

        static string GetUpdatedBuildingFromUser(string oldBuilding)
        {
            string userResponse = GetUserResponseForQuestion($"do you want to change the Building of the staff({oldBuilding})? Y/N");
            if (userResponse == "Y")
            {
                return GetBuildingOfStaff();
            }
            return oldBuilding;
        }

        public static void UpdateStaff()
        {

            int userInputIdForSearching = GetIdFromUser();
            Staff staffFromSearch = staffRepository.GetStaff(userInputIdForSearching);
            Staff newStaff = staffFromSearch;
            if (staffFromSearch == null)
            {
                Console.WriteLine("not found");
                return;
            }
            Console.WriteLine(staffFromSearch);
            string newName = GetUpdatedNameFromUser(staffFromSearch.Name);
            if (staffFromSearch.GetType() == typeof(AdministrativeStaff))
            {
                AdministrativeStaff adminStaffFromSearch = (AdministrativeStaff)staffFromSearch;
                string newSection = GetUpdatedSectionFromUser(adminStaffFromSearch.Section);
                newStaff = new AdministrativeStaff(userInputIdForSearching, newName, newSection);
            }
            else if (staffFromSearch.GetType() == typeof(TeachingStaff))
            {
                TeachingStaff teachStaffFromSearch = (TeachingStaff)staffFromSearch;
                List<string> newSubjectsHandled = GetUpdatedSubjectsHandledFromUser(teachStaffFromSearch.SubjectsHandled);
                newStaff = new TeachingStaff(userInputIdForSearching, newName, newSubjectsHandled);
            }
            else if (staffFromSearch.GetType() == typeof(SupportStaff))
            {
                SupportStaff supportStaffFromSearch = (SupportStaff)staffFromSearch;
                string newBuilding = GetUpdatedBuildingFromUser(supportStaffFromSearch.Building);
                newStaff = new SupportStaff(userInputIdForSearching, newName, newBuilding);
            }
            else
            {
                Console.WriteLine("invalid type");
                return;
            }


            bool ret = staffRepository.UpdateStaff(userInputIdForSearching, newStaff);


            if (ret)
            {
                Console.WriteLine("update success");
            }
            else
            {
                Console.WriteLine("update failed");
            }
        }


        public static void DeleteStaff()
        {
            int userInputIdForSearching = GetIdFromUser();
            bool ret = staffRepository.DeleteStaff(userInputIdForSearching);
            if (ret)
            {
                Console.WriteLine("deleted successfully");
            }
            else
            {
                Console.WriteLine("delete failed");
            }
        }
    }
}
