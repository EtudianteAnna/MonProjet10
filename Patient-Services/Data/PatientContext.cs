
using Microsoft.EntityFrameworkCore;

using PatientService.Models;
 
namespace PatientService.Data

{

    public class PatientContext : DbContext

    {

        public PatientContext(DbContextOptions<PatientContext> options) : base(options)

        {

        }
 
        public DbSet<Patient> Patients { get; set; }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

            modelBuilder.Entity<Patient>().HasData(

                new Patient

                {

                    Id = "1", // Change to string

                    FirstName = "John",

                    LastName = "Doe",

                    BirthDate = new DateTime(1980, 1, 1),

                    Gender = "Male",

                    Address = "123 Main St",

                    PhoneNumber = "123-456-7890"

                },

                new Patient

                {

                    Id = "2", // Change to string

                    FirstName = "Jane",

                    LastName = "Doe",

                    BirthDate = new DateTime(1985, 5, 5),

                    Gender = "Female",

                    Address = "456 Elm St",

                    PhoneNumber = "987-654-3210"

                }

            );

        }

    }

}
