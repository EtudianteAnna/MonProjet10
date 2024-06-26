namespace MicroFrontEnd.Models
{
    public class Patient
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(); // Initialise avec un GUID par défaut
        public string FirstName { get; set; } = string.Empty; // Initialise avec une chaîne vide
        public string LastName { get; set; } = string.Empty; // Initialise avec une chaîne vide
        public DateTime BirthDate { get; set; } = DateTime.Now; // Initialise avec la date actuelle
        public string Gender { get; set; } = string.Empty; // Initialise avec une chaîne vide
        public string Address { get; set; } = string.Empty; // Initialise avec une chaîne vide
        public string PhoneNumber { get; set; } = string.Empty; // Initialise avec une chaîne vide
    }
}

