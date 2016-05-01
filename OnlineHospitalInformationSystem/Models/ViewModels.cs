

using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineHospitalInformationSystem.Models
{
    public class ViewModels
    {
    }

    public class TestViewModel
    {
        public string TestName { get; set; }
        public string Hospital { get; set; }
        public int Cost { get; set; }
    }

    public class HospitalViewModel
    {
        public int HospitalId { get; set; }
       [Display(Name = "Hospital Name")]
        public string Hospitalname { get; set; }
        public string Location { get; set; }
        public string HospitalType { get; set; }
        public string Description { get; set; }
        [Display(Name = "Hospital Image")]
        public byte[] HospitalImage { get; set; }
    }
    public class DoctorDetailsViewModel
    {
        public string Name { get; set; }
        public string Degree { get; set; }
        public string Instution { get; set; }
        public int Visit { get; set; }
        public string DoctorName { get; set; }
        public string Specilization { get; set; }

        public string Time { get; set; }
        public string Days { get; set; }
        public string Gender { get; set; }
    }

    public class DateTimeInfo
    {
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string Day { get; set; }
        public string Time { get; set; }
    }
    public class DepartmentViewModel
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public int HospitalId { get; set; }

    }


    public class PatientSerialBookingViewModel
    {
          [Display(Name = "Serial No.")]
        public int SerialNo { get; set; }
         [Display(Name = "Patient Name")]
        public string PatientName { get; set; }
     
         public int PatientId { get; set; }
        

        [Display(Name = "Patient Type")]
        public string PatientType { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Visit Date")]
        [DataType(DataType.Date)]

        public DateTime VisitDateTime { get; set; }
      [Display(Name = "Consulting Doctor")]
        public string Doctor { get; set; }

        public int DoctorId { get; set; }

    }



    public class PatientSerialBookingDetailsViewModel : PatientSerialBookingViewModel
    {
        [Display(Name = " Age")]
        public int Age { get; set; }
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }
        [Display(Name = "Booking DateTime")]
        public DateTime BookingDateTime { get; set; }
        [Display(Name = "Sex")]
        public string Sex { get; set; }
    }
    public class TestDetailsModel
    {
        public string TestName { get; set; }
        public int Coast { get; set; }
        public string Room { get; set; }
        public string FloorNo { get; set; }
        public string Hospital { get; set; }
    }

    public class DoctorBaseViewModel
    {
        public int DepartmentId { get; set; }
        public int HospitalId { get; set; }
        public string Name { get; set; }
    }
    public class DoctorViewModelByHospital : DoctorBaseViewModel
    {

        
        public string Department { get; set; }     

    }

    public class DoctorViewModelByDepartmentAndHospital : DoctorBaseViewModel
    {
        public int Visit { get; set; }
        public string Gender { get; set; }
    }
}