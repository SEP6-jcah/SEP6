namespace Sep6Client.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public string Birthday { get; set; }
        public string Biography { get; set; }
        public string Department { get; set; }
        public Movie[] KnownFor { get; set; }
    }
}