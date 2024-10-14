namespace Konteh.BackOfficeApi.Domain
{
    public class Candidate
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Email { get; set; }
        public String Faculty { get; set; }

        public Candidate() { }
        public Candidate(long id, string name, string surname, string email, string faculty)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            Faculty = faculty;
        }



    }
}
