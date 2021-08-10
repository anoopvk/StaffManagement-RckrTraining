using StaffManagement;
using StaffManagement.Data;
using StaffManagement.Lib.Model;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Staffmanagement
{

    class Program
    {
        static void Main(string[] args)
        {
            bool runFlag = true;
            while (runFlag){
                Console.WriteLine("enter 1 to add a staff ");
                Console.WriteLine("enter 2 to add a staff in bulk ");
                Console.WriteLine("enter 3 to view delails of all staff ");
                Console.WriteLine("enter 4 to view details of a staff");
                Console.WriteLine("enter 5 to update details of a staff");
                Console.WriteLine("enter 6 to update details of a staff in Bulk");
                Console.WriteLine("enter 7 to delete a staff");
                Console.WriteLine("enter 8 to exit");
                int choice = Convert.ToInt32(Console.ReadLine());

                //Console.WriteLine("the num =  " + choice);
                switch (choice)
                {
                    case 1:
                        //add staff
                        ConsoleStaffManager.AddNewStaff();
                        break;

                    case 2:
                        //add staff
                        ConsoleStaffManager.AddNewStaffInBulk();
                        break;

                    case 3:
                        //view details of all
                        ConsoleStaffManager.ViewAllStaff();
                        break;

                    case 4:
                        //view details of one staff
                        ConsoleStaffManager.ViewOneStaff();
                        break;

                    case 5:
                        //update
                        ConsoleStaffManager.UpdateStaff();
                        break;

                    case 6:
                        //update
                        ConsoleStaffManager.UpdateStaffInBulk();
                        break;

                    case 7:
                        //delete
                        ConsoleStaffManager.DeleteStaff();
                        break;

                    case 8:
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