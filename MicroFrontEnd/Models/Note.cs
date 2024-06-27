using System;

namespace MicroFrontEnd.Models
{
    public class Note
    {
        public string? Id { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}