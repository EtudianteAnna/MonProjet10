using System;
using System.ComponentModel.DataAnnotations;

namespace ApiGateway.DTO
{
    public class PatientDTO
    {
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
