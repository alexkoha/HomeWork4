namespace LINQ
{
    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string CollegName { get; set; }
        public int Id { get; set; }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Id { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return string.Format($"Name : {Name}\nAge : {Age}\nId : {Id}\nAddress : {Address}");
        }
    }
}