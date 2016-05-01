
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using OnlineHospitalInformationSystem.Models;

namespace OnlineHospitalInformationSystem.Controllers
{
    public class HospitalUserController : Controller
    {
        static readonly HospitalDbContext Db = new HospitalDbContext();
        [Authorize]
        public ViewResult Index(int hospitalId)
        {
            ViewBag.dialodHeader = "Patient Serial Booking Details";
            ViewBag.hospitalId = hospitalId;
            return View(GetPatientsBookingDetails(hospitalId));
        }
        [HttpPost]
        public ViewResult Index(int hospitalId,string doctorName)
        {

            if (doctorName != "" )
            {
                ViewBag.hospitalId = hospitalId;
                ViewBag.dialodHeader = "Patient Serial Booking Details";
                return View(GetPatientsBookingDetails(hospitalId, doctorName));
            }
            ViewBag.hospitalId = hospitalId;
            ViewBag.dialodHeader = "Patient Serial Booking Details";

            return View(GetPatientsBookingDetails(hospitalId));
        }

        private IEnumerable<PatientSerialBookingViewModel> GetPatientsBookingDetails(int hospitalId)
        {
            return (from p in Db.PatientBookings

                    join d in Db.Doctors on p.DoctorId equals d.DoctorId
                    where p.HospitalId.Equals(hospitalId)
                    select new PatientSerialBookingViewModel
                    {
                        PatientName = p.PatientName,
                      
                        PatientType = p.PatientType,
              
                        VisitDateTime = p.VisitDate,DoctorId = p.DoctorId,
                 
                        Doctor = d.DoctorName,SerialNo = p.SerialNo,PatientId = p.PatientId
                    }).ToList();

        }

        public PartialViewResult GetPatientSerialBookingDetails(int patientId, int hospitalId, int doctorId)
        {
             ViewBag.details=( from p in Db.PatientBookings

                    join d in Db.Doctors on p.DoctorId equals d.DoctorId
                               where p.PatientId.Equals(patientId)
                     where p.HospitalId.Equals(hospitalId)
                      where p.DoctorId.Equals(doctorId)
                        select new PatientSerialBookingDetailsViewModel
                    {
                        PatientName = p.PatientName,
                      
                        PatientType = p.PatientType,
              
                        VisitDateTime = p.VisitDate,BookingDateTime = p.BookingDateTime,
                 
                        Doctor = d.DoctorName,MaritalStatus = p.MaritalStatus,
                        Sex = p.Gender,Age = p.PatientAge
                    }).ToList();

            return PartialView("_GetPatientSerialBookingDetails");
        }
        private IEnumerable<PatientSerialBookingViewModel> GetPatientsBookingDetails(int hospitalId,string doctorName)
        {
            return (from p in Db.PatientBookings

                    join d in Db.Doctors on p.DoctorId equals d.DoctorId
                    where p.HospitalId.Equals(hospitalId)
                    where d.DoctorName.Contains(doctorName)
                    select new PatientSerialBookingViewModel
                    {
                        PatientName = p.PatientName,
        
                        PatientType = p.PatientType,
              
                        VisitDateTime = p.VisitDate
                    ,DoctorId = p.DoctorId,
                        Doctor = d.DoctorName,
                        SerialNo = p.SerialNo,
                        PatientId = p.PatientId
                    }).ToList();

        }
        private void DisplayHospitalSelectlist()
        {
            var hospitals = Db.Hospitals.Select(c => new
            {
                hId = c.HospitalId,

                hName = c.HospitalName
            }).ToList();

            ViewBag.hospitals = new SelectList(hospitals, "hId", "hName");
        }
        public ActionResult Login()
        {
            DisplayHospitalSelectlist();
            return View();
        }
        [HttpPost]
        public ActionResult Login(HospitalUserLoginMetaData objHospitalUserLoginMetaData)
        {
            if (ModelState.IsValid)
            {
                if (UserManager.IsLogin(objHospitalUserLoginMetaData.UserName, objHospitalUserLoginMetaData.Password))
                {
                 
                    var ident = new ClaimsIdentity(
             new[] { 
              // adding following 2 claim just for supporting default antiforgery provider
            new Claim(ClaimTypes.NameIdentifier, objHospitalUserLoginMetaData.UserName),
         new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
          
          


          },
             DefaultAuthenticationTypes.ApplicationCookie);

                    HttpContext.GetOwinContext().Authentication.SignIn(
                       new AuthenticationProperties { IsPersistent = false }, ident);
                    UpdateLastLoginDateTime(objHospitalUserLoginMetaData.UserName);
                    return RedirectToAction("Index", "HospitalUser",new{hospitalId=objHospitalUserLoginMetaData.HospitalId});

                }
                else
                {
                    ModelState.AddModelError("","Invalid username and password combination");
                }
            }
            DisplayHospitalSelectlist();
            return View();
        }

        private void UpdateLastLoginDateTime(string userName)
        {
            HospitalUserLoginModel obHospitalUserLoginModel=Db.HospitalUserLoginModels.Single(m => m.UserName.Equals(userName));
      obHospitalUserLoginModel.LastLoginDateTime=DateTime.Now;
            Db.Entry(obHospitalUserLoginModel).State=EntityState.Modified;
            Db.SaveChanges();
        }

   private  class UserManager
        {
            public static bool IsLogin(string username, string password)
            {
             
        
                    return Db.HospitalUserLoginModels.Any(u => u.UserName == username
                        && u.Password == password);
           
            }
       
        }
   private IAuthenticationManager AuthenticationManager
   {
       get
       {
           return HttpContext.GetOwinContext().Authentication;
       }
   }
        public RedirectToRouteResult LoggOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }
	}
}