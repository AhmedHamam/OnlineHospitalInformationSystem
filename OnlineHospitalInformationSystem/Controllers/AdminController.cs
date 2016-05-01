
using System.Linq;
using System.Web.Mvc;
using OnlineHospitalInformationSystem.Models;
using DepartmentAddModel = OnlineHospitalInformationSystem.Models.DepartmentAddModel;
using DoctorAddModel = OnlineHospitalInformationSystem.Models.DoctorAddModel;
using HospitalAddModel = OnlineHospitalInformationSystem.Models.HospitalAddModel;

namespace OnlineHospitalInformationSystem.Controllers
{
    public class AdminController : Controller
    {
        readonly HospitalDbContext _db = new HospitalDbContext();

        public ViewResult DisplayAdminHomePage()
        {
            ViewData["Degrees"] = _db.Degrees.Select(m => m.DegreeName);
            ViewData["Departments"] = _db.Departments.Select(m => m.DepartmentName);
            ViewData["Tests"] = (from t in _db.Tests
                                 join h in _db.Hospitals on t.HospitalId equals h.HospitalId
                                 select new TestViewModel { TestName = t.TestName, Hospital = h.HospitalName, Cost = t.Coast }).ToList();

            ViewData["Specilizations"] = _db.Specilizations.Select(m => m.SpecilizationName);
            ViewData["Visits"] = _db.Visits.Select(m => m.VisitCost);
            ViewData["Hospitals"] = _db.Hospitals.Select(m => new HospitalViewModel { Hospitalname = m.HospitalName, HospitalType = m.HospitalType, Location = m.Location, Description = m.HospitalShortDescription });
    
            return View();
        }
        public ActionResult AddDegree()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDegree(DegreeAddModel objDegreeAddModel)
        {

            if (ModelState.IsValid)
            {
                var checkIsCurrentDegreeAlreadyExist =
                    _db.Degrees.Any(x => x.DegreeName.Equals(objDegreeAddModel.Degree));
                if (!checkIsCurrentDegreeAlreadyExist)
                {
                    var objDegree = new Degree
                    {
                        DegreeName = objDegreeAddModel.Degree
                    };
                    _db.Degrees.Add(objDegree);
                    _db.SaveChanges();
                    ModelState.Clear();
                }
                else
                {
                    ModelState.AddModelError("", "Degree " + objDegreeAddModel.Degree + " " + "already exist.");
                }

            }
            return View();
        }
        public ActionResult AddDepartment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDepartment(DepartmentAddModel objDepartmentAddModel)
        {

            if (ModelState.IsValid)
            {
                var checkIsCurrentDegreeAlreadyExist =
                    _db.Departments.Any(x => x.DepartmentName.Equals(objDepartmentAddModel.Department));
                if (!checkIsCurrentDegreeAlreadyExist)
                {
                    var objDepartment = new Department
                    {
                        DepartmentName = objDepartmentAddModel.Department
                    };
                    _db.Departments.Add(objDepartment);
                    _db.SaveChanges(); ModelState.Clear();
                }
                else
                {
                    ModelState.AddModelError("", "Department " + "\'" + objDepartmentAddModel.Department + "\'" + " " + "already exist.");
                }

            }
            return View();
        }
        public ActionResult AddHospital()
        {
            var tests = _db.Tests.Select(c => new
            {
                tId = c.TestId,

                tName = c.TestName
            }).ToList();

            ViewBag.tests = new SelectList(tests, "tId", "tName");

            return View();
        }
        [HttpPost]
        public ActionResult AddHospital(HospitalAddModel objHospitalAddModel)
        {

            if (ModelState.IsValid)
            {
                var checkIsCurrentHospitalAlreadyExist =
                    _db.Hospitals.Any(x => x.HospitalName.Equals(objHospitalAddModel.Name));
                if (!checkIsCurrentHospitalAlreadyExist)
                {

                    var data = new byte[objHospitalAddModel.File.ContentLength];
                    objHospitalAddModel.File.InputStream.Read(data, 0, objHospitalAddModel.File.ContentLength);
                    objHospitalAddModel.Image = data;
                    var objHospital = new Hospital
                    {
                        HospitalName = objHospitalAddModel.Name,
                        Location = objHospitalAddModel.Location,
                        HospitalType = objHospitalAddModel.HospitalType,
                        HospitalShortDescription = objHospitalAddModel.Description,
                        ImageData = objHospitalAddModel.Image 
                    };
                    _db.Hospitals.Add(objHospital);
                    _db.SaveChanges(); ModelState.Clear();
                    ViewBag.successVal = 1;
                }
                else
                {
                    ModelState.AddModelError("", "Hospital " + "\'" + objHospitalAddModel.Name + "\'" + " " + "already exist.");
                }

            }
            return View();
        }

        public ActionResult AddTest()
        {

            DisplayHospitalSelectlist();
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

        public ActionResult AddSpecilization()
        {


            return View();
        }
        [HttpPost]
        public ActionResult AddSpecilization(SpecilizationAddModel objSpecilizationAddModel)
        {
            if (ModelState.IsValid)
            {
                var checkIsCurrentSpecilizationAlreadyExist =
                    _db.Specilizations.Any(x => x.SpecilizationName.Equals(objSpecilizationAddModel.Specilization));
                if (!checkIsCurrentSpecilizationAlreadyExist)
                {
                    var objSpecilization = new Specilization
                    {
                        SpecilizationName = objSpecilizationAddModel.Specilization,

                    };
                    _db.Specilizations.Add(objSpecilization);
                    _db.SaveChanges(); ModelState.Clear();
                }
                else
                {
                    ModelState.AddModelError("", "Specilization " + "\'" + objSpecilizationAddModel.Specilization + "\'" + " " + "already exist.");
                }

            }

            return View();
        }
  


        [HttpPost]
        public ActionResult AddTest(TestAddModel objTestAddModel)
        {

            if (ModelState.IsValid)
            {
                int hospitalId = int.Parse(objTestAddModel.HospitalName);
                var checkIsCurrentTestAlreadyExist =
                    _db.Tests.Any(x => x.TestName.Equals(objTestAddModel.Test) && x.HospitalId.Equals(hospitalId));
                if (!checkIsCurrentTestAlreadyExist)
                {
                    var objTest = new Test
                    {
                        TestName = objTestAddModel.Test,
                        Coast = objTestAddModel.Coast,
                        HospitalId = hospitalId
                    };
                    _db.Tests.Add(objTest);
                    _db.SaveChanges(); ModelState.Clear();
                }
                else
                {
                    ModelState.AddModelError("", "Test " + "\'" + objTestAddModel.Test + "\'" + " " + "already exist.");
                }

            }
            DisplayHospitalSelectlist();
            return View();
        }

        public ActionResult AddVisit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddVisit(VisitAddModel objVisitAddModel)
        {

            if (ModelState.IsValid)
            {
                var checkIsCurrentVisitAlreadyExist =
                    _db.Visits.Any(x => x.VisitCost.Equals(objVisitAddModel.Visit));
                if (!checkIsCurrentVisitAlreadyExist)
                {
                    var objVisit = new Visit
                    {
                        VisitCost = objVisitAddModel.Visit
                    };
                    _db.Visits.Add(objVisit);
                    _db.SaveChanges();
                    ModelState.Clear();
                }
                else
                {
                    ModelState.AddModelError("", "Visit " + "\'" + objVisitAddModel.Visit + "\'" + " " + "already exist.");
                }

            }
            return View();
        }

        
        public ActionResult AddDoctor()
        {

            DisplayGovernmentHospitalSelectlist();
            DisplayHospitalSelectlist();
            DisplayDepartmentSelectlist();
            DisplayInstitutionSelectlist();
            DisplayDegreeSelectlist();
            DisplaySpecilizationSelectlist();
        
            return View();
        }

        private void DisplayDegreeSelectlist()
        {

            ViewBag.degrees = _db.Degrees.Select(x => new DegreeSelectList() { DegreeId = x.DegreeId, Degree = x.DegreeName });
           
        }

        private void DisplaySpecilizationSelectlist()
        {

            ViewBag.specilizations = _db.Specilizations.Select(x => new SpecilizationSelectList() { SpecilizationId = x.SpecilizationId, Specilization = x.SpecilizationName });
           
        }

        private void DisplayInstitutionSelectlist()
        {


         ViewBag.institutions = _db.Hospitals.Select(m=>new InstitutionSelectList(){InstitutionId = m.HospitalId,Institution = m.HospitalName});
           
        }

        private void DisplayDepartmentSelectlist()
        {

            var departments = _db.Departments.Select(c => new
            {
                dptId = c.DepartmentId,

                dptName = c.DepartmentName
            }).ToList();

            ViewBag.departments = new SelectList(departments, "dptId", "dptName");
        }
        private void DisplayGovernmentHospitalSelectlist()
        {

            var govHospitals = _db.Hospitals.Where(m=>m.HospitalType.StartsWith("Gov")).Select(c => new
            {
               hospitalId = c.HospitalId,

               hospitalName = c.HospitalName
            }).ToList();

            ViewBag.govHospitals = new SelectList(govHospitals, "hospitalId", "hospitalName");
        }
        [HttpPost]
        public JsonResult AddDoctor(DoctorAddModel objDoctorAddModel)
        {
            var q = from d in objDoctorAddModel.DegreeIdList
                from i in objDoctorAddModel.InstitutionIdList
           
                select new SelectListId
                {
                    DegreeId = d,
                    InstitutionId = i
                };
            var message = "";

            var isCurrentDoctorrecordAlreadyExist =
                _db.Doctors.Any(
                    x =>
                        x.DoctorName.Equals(objDoctorAddModel.Name) && x.HospitalId.Equals(objDoctorAddModel.HospitalId) &&
                        x.DepartmentId.Equals(objDoctorAddModel.DepartmentId));


            if (!isCurrentDoctorrecordAlreadyExist)
            {
                foreach (var idList in q)
                {

                    var degreeCount = objDoctorAddModel.DegreeIdList.Count;
                    var institutionCount = objDoctorAddModel.InstitutionIdList.Count;
                    var isRecordOfDegreeAlreadyExist =
                 _db.Doctors.Any(
                     m =>
                         m.DoctorName.Equals(objDoctorAddModel.Name) &&
                         m.HospitalId.Equals(objDoctorAddModel.HospitalId) && m.DegreeId.Equals(idList.DegreeId));

                    if (degreeCount == institutionCount)
                    {

                        var isRecordOfInstitutionAlreadyExist =
                     _db.Doctors.Any(
                         m =>
                             m.DoctorName.Equals(objDoctorAddModel.Name) &&
                             m.HospitalId.Equals(objDoctorAddModel.HospitalId) && m.InstitutionId.Equals(idList.InstitutionId));

                        if (!isRecordOfDegreeAlreadyExist && !isRecordOfInstitutionAlreadyExist)
                        {
                            var objDoctor = new Doctor
                            {
                                DegreeId = idList.DegreeId,
                                InstitutionId = idList.InstitutionId,
                                SpecilizationId = objDoctorAddModel.SpecilizationId,
                                DoctorName = objDoctorAddModel.Name,
                                HospitalId = objDoctorAddModel.HospitalId,
                                DepartmentId = objDoctorAddModel.DepartmentId,
                                GovernmentHospitalId = objDoctorAddModel.GovHospitalId,
                                Visit = objDoctorAddModel.Visit,
                                Gender = objDoctorAddModel.Gender

                            };

                            _db.Doctors.Add(objDoctor);
                            _db.SaveChanges();
                            message = "New record has been added successfully.";
                        }

                    }
                    if (degreeCount > institutionCount)
                    {
                        if (!isRecordOfDegreeAlreadyExist)
                        {
                            var objDoctor = new Doctor
                            {
                                DegreeId = idList.DegreeId,
                                InstitutionId = idList.InstitutionId,
                                SpecilizationId = objDoctorAddModel.SpecilizationId,
                                DoctorName = objDoctorAddModel.Name,
                                HospitalId = objDoctorAddModel.HospitalId,
                                DepartmentId = objDoctorAddModel.DepartmentId,
                                GovernmentHospitalId = objDoctorAddModel.GovHospitalId,
                                Visit = objDoctorAddModel.Visit,
                                Gender = objDoctorAddModel.Gender

                            };

                            _db.Doctors.Add(objDoctor);
                            _db.SaveChanges();
                            message = "New record has been added successfully.";
                        }
                    }
                    if (institutionCount > degreeCount)
                    {
                        message = "There are more institution than degree.Please correct it.";

                    }



                }
            }
            else
            {
                message = "There is already a record in the database for this doctor.You are trying to enter a duplicate record and this is not possible.";

            }
          
            
            return  Json(new{result=message});


        }

    

    
       
    }
}