using Microsoft.EntityFrameworkCore;

namespace api_mvc_core.Models
{
   public class Dbbloods:DbContext
   {
      public Dbbloods(DbContextOptions<Dbbloods> options) : base(options)
      { }
      public DbSet<Doctor> Doctors { get; set; }
   }


   public class Doctor {
   
      public int Id { get; set; }
      public string Name { get; set; }
      public string RegNumber { get; set; }
      public string Department { get; set; }
      public string Address { get; set; }
      public string City { get; set; }
      public string Nid { get; set; }
      public string PhoneNumber { get; set; }
      
   }
}
