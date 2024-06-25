namespace ApiGateway.DTO
{
    public class NoteDTO
    {
        public string Id { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
