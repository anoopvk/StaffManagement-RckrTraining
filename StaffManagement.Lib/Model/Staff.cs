using System;
namespace StaffManagement.Lib.Model
{
    public class Staff
    {
        public int id;
        public string name;

        public Staff(int sId, string sName)
        {
            id = sId;
            name = sName;

        }
        public void UpdateName()
        {
            //String newName;
            Console.WriteLine("enter name");
            name = Console.ReadLine();
        }

    }
}