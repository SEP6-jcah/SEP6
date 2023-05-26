using System.Collections.Generic;

namespace Sep6Client.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Aka { get; set; }
        public string OriginalName { get; set; }
        public int Gender { get; set; }
        public string Birthday { get; set; }
        public string Biography { get; set; }
        public string Job { get; set; }
        public CreditList KnownFor { get; set; }
    }
}