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

        static void Main(string[] args)
        {
            //var a = default(int);
            String nameOfStaff;
            String sectionOfStaff;
            int newid = 1;
            int tempId;
            bool ret;
            //AdministrativeStaff tempStaff;
            //List<AdministrativeStaff> adminstaff = new List<AdministrativeStaff>();
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
                        Console.WriteLine("enter name");
                        nameOfStaff = Console.ReadLine();
                        Console.WriteLine("enter section");
                        sectionOfStaff = Console.ReadLine();
                        staffRepository.AddStaff(new AdministrativeStaff(newid,nameOfStaff,sectionOfStaff));
                        newid++;
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
                        tempId = GetIdFromUser();
                        Staff staffFromSearch = staffRepository.GetStaff(tempId);
                        if (staffFromSearch == null)
                        {
                            Console.WriteLine("not found");
                            break;
                        }
                        staffFromSearch.id = 99;
                        Console.WriteLine(staffFromSearch);
                        break;


                    case 4:
                        //update
                        tempId = GetIdFromUser();
                        Console.WriteLine("enter name");
                        nameOfStaff = Console.ReadLine();
                        Console.WriteLine("enter section");
                        sectionOfStaff = Console.ReadLine();

                        ret = staffRepository.UpdateStaff(tempId, new AdministrativeStaff(newid, nameOfStaff, sectionOfStaff));
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
                        tempId = GetIdFromUser();
                        ret = staffRepository.DeleteStaff(tempId);
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

                }
            }
            

        }
    }
}