
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;


namespace OnlineHospitalInformationSystem.Models
{
  
    public class HospitalModels
    {
     
    }
    [Table("Day")]
    public class Day
    {
        [Key]
        public int DayId { get; set; }
        public string DayName { get; set; }
        public int DoctorId { get; set; }
        public string Time { get; set; }
    }
    [Table("TestDetail")]
    public class TestDetail
    {
        [Key]
        public int TestDetailsId { get; set; }
        public int TestId { get; set; }
        public int HospitalId { get; set; }
    }
    [Table("Degree")]
    public class Degree
    {
        [Key]
        public int DegreeId { get; set; }
        public string DegreeName { get; set; }
    }
    [Table("Department")]
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int HospitalId { get; set; }
    }
    [Table("T")]
    public class T
    {
        [Key]
        public int Id { get; set; }
        public int Value { get; set; }
        public int Deg { get; set; }
    }

    [Table("Doctor")]
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Gender { get; set; }
        public int DepartmentId { get; set; }
        public int DegreeId { get; set; }
        public int InstitutionId { get; set; }
        public int SpecilizationId { get; set; }
        public int? GovernmentHospitalId { get; set; }
        public int HospitalId { get; set; }
        public int Visit { get; set; }

    }
    [Table("Hospital")]
    public class Hospital
    {
        [Key]
        public int HospitalId { get; set; }
        public string HospitalName { get; set; }
        public string Location { get; set; }
        public string HospitalType { get; set; }
        public string HospitalShortDescription { get; set; }
        public byte[] ImageData { get; set; }

    }



    [Table("PatientBooking")]
    public class PatientBooking
    {
        [Key]
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int PatientAge { get; set; }
        public string MaritalStatus { get; set; }
        public int DoctorId { get; set; }
        public int HospitalId { get; set; }
        public string Gender { get; set; }
        public DateTime VisitDate { get; set; }
        public string PatientType { get; set; }
        public DateTime BookingDateTime { get; set; }
        public int SerialNo { get; set; }
    }
    [Table("Specilization")]
    public class Specilization
    {
        [Key]
        public int SpecilizationId { get; set; }
        public string SpecilizationName { get; set; }
    }
    [Table("Test")]
    public class Test
    {
        [Key]
        public int TestId { get; set; }
        public string TestName { get; set; }
        public int Coast { get; set; }
        public int HospitalId { get; set; }
    }
    [Table("Time")]
    public class Time
    {
        [Key]
        public int TimeId { get; set; }
        public string TimeRange { get; set; }

    }
    [Table("Visit")]
    public class Visit
    {
        [Key]
        public int VisitId { get; set; }
        public int VisitCost { get; set; }

    }
    [Table("HospitalUserLogin")]
    public class HospitalUserLoginModel
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? LastLoginDateTime { get; set; }
        public int HospitalId { get; set; }
    }
    public class HospitalDbContext : DbContext
    {
        public DbSet<Day> Days { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }

        public DbSet<PatientBooking> PatientBookings { get; set; }
        public DbSet<Specilization> Specilizations { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<TestDetail> TestDetails { get; set; }
        public DbSet<T> Ts { get; set; }
        public DbSet<HospitalUserLoginModel> HospitalUserLoginModels { get; set; }
    }
}