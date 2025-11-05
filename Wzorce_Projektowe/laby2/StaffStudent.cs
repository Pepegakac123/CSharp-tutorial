using System;

namespace LibraryManagementSystem
{
    // Dziedziczenie - Student dziedziczy po User
    public class Student : User
    {

        public string Class { get; set; }

        public Student() : base()
        {
        }

        public Student(string name, string id, string studentClass) : base(name, id)
        {
            Class = studentClass;
        }
    }

    // Dziedziczenie - Staff dziedziczy po User
    public class Staff : User
    {

        public string Dept { get; set; }
        public Staff() : base()
        {
        }

        public Staff(string name, string id, string dept) : base(name, id)
        {
            Dept = dept;
        }
    }
}