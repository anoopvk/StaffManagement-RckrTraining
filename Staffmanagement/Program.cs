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
        public static AdministrativeStaff GetStaffUsingIDFromUser(List<AdministrativeStaff> staffList)
        {
            int tempId;
            Console.WriteLine("enter the id of the staff");
            tempId = Convert.ToInt32(Console.ReadLine());
            return staffList.Find(x => x.id == tempId);
        }
        public static bool IsAdminStaffEmpty(List<AdministrativeStaff> staffList)
        {
            if (staffList.Count == 0){
                Console.WriteLine("....empty....");
                return true;
            }
            return false;
        }

        static void Main(string[] args)
        {
            //var a = default(int);
            //String nameOfStaff;
            //String sectionOfStaff;
            int newid = 1;
            int tempId;
            //AdministrativeStaff tempStaff;
            //List<AdministrativeStaff> adminstaff = new List<AdministrativeStaff>();
            AdministrativeStaffs adminstaffs = new AdministrativeStaffs();

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
                        adminstaffs.AddStaff(newid);
                        newid++;
                        break;


                    case 2:
                        //view details of all
                        adminstaffs.ViewStaff();
                        break;


                    case 3:
                        //view details of one staff
                        tempId = GetIdFromUser();
                        adminstaffs.ViewStaff(tempId);
                        break;


                    case 4:
                        //update
                        tempId = GetIdFromUser();
                        adminstaffs.UpdateStaff(tempId);
                        break;


                    case 5:
                        //delete
                        tempId = GetIdFromUser();
                        adminstaffs.DeleteStaff(tempId);
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