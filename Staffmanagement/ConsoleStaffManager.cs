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
using System.Reflection;

namespace StaffManagement
{

    static class ConsoleStaffManager
    {
        private static IStaffRepository staffRepository = Init();
        private static int s_newId = GetInitialNextId();

        private static IStaffRepository Init()
        {
            var implClass = ConfigurationManager.AppSettings["ImplClass"];
            return (IStaffRepository)Activator.CreateInstance(Type.GetType(implClass));
        }

        
       
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


        static string GetSubjectHandledOfStaff()
        {
            Console.WriteLine("enter subject name : ");
            string subjectHandled = Console.ReadLine();
            
            return subjectHandled;
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
                string subjectsHandled = GetSubjectHandledOfStaff();
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

        public static void AddNewStaffInBulk()
        {
            List<Staff> staffList = new List<Staff>();
            bool flag = true;
            Staff newStaff;
            while (flag)
            {
                newStaff = CreateStaffThroughUserInput(s_newId);
                if (newStaff != null)
                {
                    staffList.Add(newStaff);
                }
                Console.WriteLine("\n do you want to add another staff? (Y/N)");
                string response = Console.ReadLine();
                if (response!="Y" && response != "y")
                {
                    flag = false;
                }

                s_newId++;
            }
            
            staffRepository.AddStaffInBulk(staffList);
            Console.WriteLine("\n Added successfully \n");
        }

        public static void ViewAllStaff()
        {
            List<Staff> listOfAllStaff = staffRepository.GetAllStaff();
            if ((listOfAllStaff == null) || (listOfAllStaff.Count==0))
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

        static string GetUpdatedSubjectHandledFromUser(string oldSubjectsHandled)
        {
            string userResponse = GetUserResponseForQuestion($"do you want to change the Subjects handled of the staff({oldSubjectsHandled})? Y/N");
            if (userResponse == "Y" || userResponse == "y")
            {
                return GetSubjectHandledOfStaff();
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

        static Staff GetUpdatedStaff(int userInputIdForSearching, Staff staffFromSearch)
        {
            Staff newStaff = staffFromSearch;
            if (staffFromSearch == null)
            {
                Console.WriteLine("not found");
                return null;
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
                string newSubjectsHandled = GetUpdatedSubjectHandledFromUser(teachStaffFromSearch.SubjectHandled);
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
                return null;
            }
            return newStaff;
        }

        public static void UpdateStaff()
        {

            int userInputIdForSearching = GetIdFromUser();
            Staff staffFromSearch = staffRepository.GetStaff(userInputIdForSearching);

            Staff newStaff = GetUpdatedStaff(userInputIdForSearching, staffFromSearch);
            if (newStaff == null)
            {
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

        public static void UpdateStaffInBulk()
        {
            List<Staff> allStaffList = staffRepository.GetAllStaff();
            List<int> updatedStaffIndexList =new List<int>();
            if ((allStaffList == null) || (allStaffList.Count == 0))
            {
                Console.WriteLine("staff list is empty");
                return;
            }

            int id;
            bool flag = true;
            List<Staff> staffListForUpdate = new List<Staff>();
            while (flag)
            {

                id = GetIdFromUser();
                int indexOfStaffInList = allStaffList.FindIndex(x => x.Id == id);
                if (indexOfStaffInList != -1)
                {
                    allStaffList[indexOfStaffInList] = GetUpdatedStaff(id, allStaffList[indexOfStaffInList]);
                    if (!updatedStaffIndexList.Contains(indexOfStaffInList))
                    {
                        updatedStaffIndexList.Add(indexOfStaffInList);
                    }
                }
                else
                {
                    Console.WriteLine("not found");
                }
                Console.WriteLine("\n do you want to update another staff? (Y/N)");
                string response = Console.ReadLine();
                if (response != "Y" && response != "y")
                {
                    flag = false;
                }


            }
            foreach (var indexes in updatedStaffIndexList)
            {
                staffListForUpdate.Add(allStaffList[indexes]);
            }
            staffRepository.UpdateStaffInBulk(staffListForUpdate);
            Console.WriteLine("Bulk Inserted successfully");

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
