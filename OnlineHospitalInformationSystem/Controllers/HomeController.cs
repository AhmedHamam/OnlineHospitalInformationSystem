
using System;
using System.Linq;
using System.Web.Mvc;
using OnlineHospitalInformationSystem.Models;

namespace OnlineHospitalInformationSystem.Controllers
{

    public class HomeController : Controller
    {
        readonly HospitalDbContext _db = new HospitalDbContext();

       
        public ActionResult Index()
        {
            var hospitals = _db.Hospitals.Select(m => new HospitalViewModel { HospitalId = m.HospitalId, Hospitalname = m.HospitalName, Location = m.Location,HospitalType = m.HospitalType,HospitalImage = m.ImageData }).ToList();

            return View(hospitals);
        }
    
        public PartialViewResult SearchPeople(string searchTerm)
        {

            var data = _db.Hospitals.Where(f => f.HospitalName.Contains(searchTerm)).ToList();
            return PartialView("_SearchView", data);
        }



        
     
     

        public ActionResult DisplayHospitalInformation(int id)
        {
            ViewBag.hospitalName = _db.Hospitals.Find(id).HospitalName;
            DisplayDoctorsByHospital(id);
            DisplayTestsByHospital(id);
            DisplayDepartmentsByHospital(id);
            ViewBag.AboutCurrentHospital= _db.Hospitals.Single(x => x.HospitalId.Equals(id)).HospitalShortDescription;
          
            ViewBag.hospitalId = id;

            return View();
        }
        [HttpPost]
        public ActionResult DisplayHospitalInformation(int id, string searchTerm, int? departmentSerrchAttemptId, int? doctorSerrchAttemptId, int? testSerrchAttemptId)
        {
            if (testSerrchAttemptId!=null)
            {
                if (searchTerm != "")
                {
                    ViewBag.tests = (from t in _db.Tests
                                     join h in _db.Hospitals on t.HospitalId equals h.HospitalId
                                     where t.HospitalId.Equals(id)
                                     where t.TestName.Contains(searchTerm)
                                     select new TestDetailsModel
                                     {
                                         TestName = t.TestName,
                                         Coast = t.Coast

                                     }).ToList();
                }
                else
                {
                    ViewBag.requiredErrorMessageForTestName = "Please enter a test name";
                    DisplayTestsByHospital(id);
                }
                DisplayDepartmentsByHospital(id);
                DisplayDoctorsByHospital(id);
                ViewBag.CurrentTabSetId = 3;
            }

            if (departmentSerrchAttemptId!=null)
            {
                if (searchTerm!="")
                {
                    ViewData["departmentList"] = (from d in _db.Departments
                                                  join dc in _db.Hospitals on d.HospitalId equals dc.HospitalId
                                                  where dc.HospitalId.Equals(id)
                                                  where d.DepartmentName.Contains(searchTerm)
                                                  select new DepartmentViewModel { DepartmentName = d.DepartmentName, DepartmentId = d.DepartmentId, HospitalId = d.HospitalId }).Distinct();
           
                }
                else
                {
                    ViewBag.requiredErrorMessageForDepartment = "Please enter a department name";
                    DisplayDepartmentsByHospital(id);
                }
                ViewBag.CurrentTabSetId = 1;

                DisplayDoctorsByHospital(id);
                DisplayTestsByHospital(id);
            }

            if (doctorSerrchAttemptId != null)
            {
                if (searchTerm != "")
                {
                    ViewBag.doctors = (from d in _db.Doctors
                                       join dpt in _db.Departments on d.DepartmentId equals dpt.DepartmentId


                                       where d.HospitalId.Equals(id)
                                       where d.DoctorName.Contains(searchTerm)
                                       select new DoctorViewModelByHospital
                                       {
                                           Name = d.DoctorName,
                                           HospitalId = d.HospitalId,
                                           DepartmentId = d.DepartmentId,
                                           Department = dpt.DepartmentName
                                       }).Distinct();
                  
                }
                else
                {
                    ViewBag.requiredErrorMessageForDoctorName = "Please enter a doctor name";
                    DisplayDoctorsByHospital(id);
                }
                ViewBag.CurrentTabSetId =2;
                DisplayDepartmentsByHospital(id);
                DisplayTestsByHospital(id);
            }
            if (doctorSerrchAttemptId == null&&departmentSerrchAttemptId==null&&testSerrchAttemptId==null)
            {
                DisplayDoctorsByHospital(id);
                DisplayDepartmentsByHospital(id);
                DisplayTestsByHospital(id);
            }
            ViewBag.hospitalName = _db.Hospitals.Find(id).HospitalName;
          
           
        
            ViewBag.AboutCurrentHospital = _db.Hospitals.Single(x => x.HospitalId.Equals(id)).HospitalShortDescription;
            ViewBag.hospitalId = id;
            return View();
        }

        private void DisplayDoctorsByHospital(int id)
        {
            ViewBag.doctors = (from d in _db.Doctors
                join dpt in _db.Departments on d.DepartmentId equals dpt.DepartmentId
                          
            
            where d.HospitalId.Equals(id)
                               select new DoctorViewModelByHospital
                {
                   Name = d.DoctorName,HospitalId = d.HospitalId,DepartmentId = d.DepartmentId,
                Department = dpt.DepartmentName
                }).Distinct();



        }
        private void DisplayDoctorsByHospitalAndDepartment(int departmentId,int hospitalId)
        {
            ViewBag.doctorsByDepartmentAndHospital = (from d in _db.Doctors
                            
                               where d.DepartmentId.Equals(departmentId)
                               where d.HospitalId.Equals(hospitalId)
                               select new DoctorViewModelByDepartmentAndHospital()
                               {
                                   Name = d.DoctorName,DepartmentId = d.DepartmentId,HospitalId = d.HospitalId,
                               
                                Gender = d.Gender,Visit = d.Visit
                               }).Distinct();



        }
        public PartialViewResult GetDoctorDetails(string doctorname,int hospitalId,int departmentId)
        {
            ViewBag.cur = doctorname;
            ViewBag.currentDoctorDetails = (from d in _db.Doctors
                join deg in _db.Degrees on d.DegreeId equals deg.DegreeId

                join h in _db.Hospitals on d.InstitutionId equals h.HospitalId
             
                where d.HospitalId.Equals(hospitalId)
                                            where d.DoctorName.Equals(doctorname)
                                            where d.DepartmentId.Equals(departmentId)
                select
                    new DoctorDetailsViewModel
                    {
                        Degree = deg.DegreeName,
                        Instution = h.HospitalName
                        
              
                    }).ToList();
            var specilization =from sp in _db.Specilizations
                join d in _db.Doctors on sp.SpecilizationId equals d.SpecilizationId
                where d.DoctorName.Equals(doctorname)
                where d.HospitalId.Equals(hospitalId)
                where d.DepartmentId.Equals(departmentId)
                select sp.SpecilizationName;
            foreach (var sp in specilization)
            {

                ViewBag.specilization = sp;
            }
            var doctorId = _db.Doctors.First(x => x.DoctorName.Equals(doctorname)).DoctorId;
            ViewBag.doctorAvailablityInfo = (from day in _db.Days
                where day.DoctorId.Equals(doctorId)
                select new DateTimeInfo {Day = day.DayName, Time = day.Time}).ToList();

            ViewBag.visit = _db.Doctors.Find(doctorId).Visit;
            ViewBag.currentHospital = _db.Hospitals.Find(hospitalId).HospitalName;
            ViewBag.currentHospitalAdddress = _db.Hospitals.Find(hospitalId).Location;
            ViewBag.currentDoctorSex = _db.Doctors.Find(doctorId).Gender;
            return PartialView( "_DoctorDetails");
        }
        private void DisplayDepartmentsByHospital(int id)
        {
            ViewData["departmentList"] = (from d in _db.Departments
                                          join dc in _db.Hospitals on d.HospitalId equals dc.HospitalId
                                          where dc.HospitalId.Equals(id)
                                          select new DepartmentViewModel { DepartmentName = d.DepartmentName, DepartmentId = d.DepartmentId ,HospitalId = d.HospitalId}).Distinct();
           
       
        }

        private void DisplayTestsByHospital(int id)
        {
            ViewBag.tests = (from t in _db.Tests
                             join h in _db.Hospitals on t.HospitalId equals h.HospitalId
                             where t.HospitalId.Equals(id)
                             select new TestDetailsModel
                             {
                                 TestName = t.TestName,
                                 Coast = t.Coast

                             }).ToList();
        }
    
        public ActionResult GetDepartmentDetails(int departmentId, int hospitalId)
        {

           
            ViewBag.CurrentHospitalName = _db.Hospitals.First(m => m.HospitalId.Equals(hospitalId)).HospitalName;
            ViewBag.CurrentHospitalDepartmentName =
                _db.Departments.First(x => x.DepartmentId.Equals(departmentId)).DepartmentName;
            DisplayDoctorsByHospitalAndDepartment(departmentId, hospitalId);
            ViewBag.hospitalId = hospitalId;
            ViewBag.dialodHeader = "Doctor Details";
            return View();
        }

        public ActionResult RecordSerial()
        {
            DisplayHospitalSelectlist();
            DisplayDoctorSelectlist();
            return View();
        }
        [HttpPost]
        public ActionResult RecordSerial(SerialBookingModel objSerialBookingModel)
        {
            if (ModelState.IsValid)
            {
                var doctorId = _db.Doctors.First(m => m.DoctorName.Equals(objSerialBookingModel.Doctor)).DoctorId;
                var checkIsNotNull = _db.PatientBookings.Any(
                    m => m.HospitalId.Equals(objSerialBookingModel.HospitalId) && m.DoctorId.Equals(doctorId));
                int serial = 0; 
                if (checkIsNotNull)
                {
                    serial =
                    _db.PatientBookings.Where(
                        m => m.HospitalId.Equals(objSerialBookingModel.HospitalId) && m.DoctorId.Equals(doctorId))
                        
                        .Max(m => m.SerialNo);
                }
                
            
                var objDoctor = new PatientBooking
                {
                    PatientName = objSerialBookingModel.PatientName,
                    PatientAge = objSerialBookingModel.PatientAge,
                    DoctorId = doctorId,
                    Gender = objSerialBookingModel.Gender,
                    MaritalStatus = objSerialBookingModel.MaritalStatus,
                    PatientType = objSerialBookingModel.PatientType,
                    BookingDateTime = DateTime.Now,
                    VisitDate = objSerialBookingModel.VisitDate,
                    HospitalId = objSerialBookingModel.HospitalId,SerialNo = (serial+1)
                };
                _db.PatientBookings.Add(objDoctor);
                _db.SaveChanges();
                ModelState.Clear();
                ViewBag.successVal = 1;

            }
            DisplayHospitalSelectlist();
            DisplayDoctorSelectlist();
            return View();
        }
        private void DisplayHospitalSelectlist()
        {
            var hospitals = _db.Hospitals.Select(c => new
            {
                hId = c.HospitalId,

                hName = c.HospitalName
            }).ToList();

            ViewBag.hospitals = new SelectList(hospitals, "hId", "hName");
        }
        private void DisplayDoctorSelectlist()
        {
            var doctors = _db.Doctors.Select(c =>c.DoctorName).Distinct();

            ViewBag.doctors = new SelectList(doctors);
        }

        public JsonResult GetDoctorsByHospital(int hospitalId)
        {
            var doctors =
               _db.Doctors.Where(m => m.HospitalId.Equals(hospitalId)).Select(m=>m.DoctorName).Distinct();
            return Json(doctors, JsonRequestBehavior.AllowGet);
        }
    }


}