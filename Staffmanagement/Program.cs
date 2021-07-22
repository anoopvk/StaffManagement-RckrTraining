using StaffManagement.Data;
using StaffManagement.Lib.Model;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Staffmanagement
{

    class Program
    {
        public static int GetIdFromUser()
        {
            int tempId;
            Console.WriteLine("enter the id of the staff");
            tempId = Convert.ToInt32(Console.ReadLine());
            return tempId;
        }
        public static Staff createStaffThroughUserInput(int newid)
        {
            int staffTypeKey;
            int numberOfSubjectsHandled;
            List<string> subjectsHandled = new List<string>();
            string supportStaffBuilding;
            String nameOfStaff;
            String sectionOfStaff;

            Console.WriteLine("enter name");
            nameOfStaff = Console.ReadLine();


            Console.WriteLine("enter 1 for Administrative staff ");
            Console.WriteLine("enter 2 for Teaching staff ");
            Console.WriteLine("enter 3 for Support staff");
            staffTypeKey = Convert.ToInt32(Console.ReadLine());

            if (staffTypeKey == 1) // administrative staff
            {
                Console.WriteLine("enter section");
                sectionOfStaff = Console.ReadLine();
                return (new AdministrativeStaff(newid, nameOfStaff, sectionOfStaff));

            }
            else if (staffTypeKey == 2) // teaching staff
            {
                Console.WriteLine("enter number of subjects handled");
                numberOfSubjectsHandled = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("enter subject names : ");
                for (int i = 0; i < numberOfSubjectsHandled; i++)
                {
                    subjectsHandled.Add(Console.ReadLine());
                }
                return (new TeachingStaff(newid, nameOfStaff, subjectsHandled));
            }
            else if (staffTypeKey == 3) // support staff
            {
                Console.WriteLine("enter building");
                supportStaffBuilding = Console.ReadLine();
                return (new SupportStaff(newid, nameOfStaff, supportStaffBuilding));
            }
            else
            {
                Console.WriteLine("invalid choice");
                return null;
            }
        }



        static void Main(string[] args)
        {

            TeachingStaff teacher = new TeachingStaff(1, "abc",new List<string>() {"a","b","c"});
            teacher.SubjectsHandled.Add("d");

            //var a = default(int);
            
            int newid = 1;
            int userInputIdForSearching;
            bool ret;
            Staff newStaff;

            InMemoryStaffRepository staffRepository = new InMemoryStaffRepository();

            bool runFlag = true;
            while (runFlag){
                Console.WriteLine("enter 1 to add a staff ");
                Console.WriteLine("enter 2 to view delails of all staff ");
                Console.WriteLine("enter 3 to view details of a staff");
                Console.WriteLine("enter 4 to update details of a staff");
                Console.WriteLine("enter 5 to delete a staff");
                Console.WriteLine("enter 6 to exit");
                int choice = Convert.ToInt32(Console.ReadLine());

                //Console.WriteLine("the num =  " + choice);
                switch (choice)
                {
                    case 1:
                        //add staff
                        newStaff = createStaffThroughUserInput(newid);
                        if (newStaff != null)
                        {
                            staffRepository.AddStaff(newStaff);
                            newid++;
                        }
                        break;


                    case 2:
                        //view details of all
                        List<Staff> listOfAllStaff = staffRepository.GetAllStaff();
                        if (listOfAllStaff==null) 
                        {
                            Console.WriteLine("empty");
                            break;
                        }
                        
                        foreach (Staff s in listOfAllStaff)
                        {
                            Console.WriteLine(s);
                        }
                        break;


                    case 3:
                        //view details of one staff
                        userInputIdForSearching = GetIdFromUser();
                        Staff staffFromSearch = staffRepository.GetStaff(userInputIdForSearching);
                        if (staffFromSearch == null)
                        {
                            Console.WriteLine("not found");
                            break;
                        }
                        //staffFromSearch.id = 99;
                        
                        Console.WriteLine(staffFromSearch);
                        break;


                    case 4:
                        //update
                        userInputIdForSearching = GetIdFromUser();
                        newStaff = createStaffThroughUserInput(userInputIdForSearching);
                        ret = false;

                        if (newStaff != null)
                        {
                            ret = staffRepository.UpdateStaff(userInputIdForSearching, newStaff);
                        }

                        if (ret)
                        {
                            Console.WriteLine("update success");
                        }
                        else
                        {
                            Console.WriteLine("update failed");
                        }

                        break;


                    case 5:
                        //delete
                        userInputIdForSearching = GetIdFromUser();
                        ret = staffRepository.DeleteStaff(userInputIdForSearching);
                        if (ret){
                            Console.WriteLine("deleted successfully");
                        }
                        else
                        {
                            Console.WriteLine("delete failed");
                        }
                        break;

                    case 6:
                        //exit
                        runFlag = false;
                        break;

                    default:
                        Console.WriteLine("invalid!");
                        break;

                }
            }
            

        }
    }
}