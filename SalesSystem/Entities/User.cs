using System;

namespace SalesSystem.Entities
{
    public class User:Interfaces.IId
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDay { get; set; }

    }
}
