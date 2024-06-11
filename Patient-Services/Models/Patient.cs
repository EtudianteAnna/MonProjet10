namespace Patient_Services.Models
{
    public class Patient
    {
        public int Id { get; set; } //sans ID doublons et pour différencier les lignes dans la BDD
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }

        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        
    }
}