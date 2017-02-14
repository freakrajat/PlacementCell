using PlacementCell.BusinessLogicClasses;
using PlacementCell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PlacementCell.PlacementCellDBModel;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using System.Drawing;

namespace PlacementCell.Controllers
{
    [Authorize]
    [HandleError]
    public class HomeController : Controller
    {
        // GET: Home
        PlacementCellDBContext dbObj = new PlacementCellDBContext();
        SendAutomatedMails sendMailObj = new SendAutomatedMails();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(LoginDBModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                //LoginDBModel loginDB = new LoginDBModel();
                //loginDB = dbObj.LoginTable.First(x => x.UserName == loginModel.UserName);
                HrLoginCheck checkUser = new HrLoginCheck();
                if (checkUser.CheckLogin(loginModel))
                {
                    FormsAuthentication.SetAuthCookie(loginModel.UserName, loginModel.RememberMe);
                    return RedirectToAction("Dashboard");
                }

                ViewBag.Error = "Please Enter Valid Username/Password To Login!";
                return View();

            }
        }

        [AllowAnonymous]
        public ActionResult Registration()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]

        public ActionResult Registration(LoginModelForClass loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Registration");
            }
            else
            {
                var email = loginModel.UserName;
                bool isAlreadyRegistered = dbObj.LoginTable.Any(x => x.UserName.ToLower().Trim().Equals(email));
                if (!isAlreadyRegistered)
                {
                    LoginDBModel loginDB = new LoginDBModel();
                    loginDB.UserName = loginModel.UserName;
                    loginDB.Password = loginModel.Password;
                    dbObj.LoginTable.Add(loginDB);
                    dbObj.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Email Already Registered Continue to Login !!!";
                    return View();
                }
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult ForgotPassword(LoginModelForClass loginModel)
        {
            var email = loginModel.UserName;
            bool isAlreadyRegistered = dbObj.LoginTable.Any(x => x.UserName.ToLower().Trim().Equals(email));
            if (!isAlreadyRegistered)
            {
                ViewBag.Message = "Email ID Not Found";
                return RedirectToAction("ForgotPassword");
            }
            else
            {
                var user = dbObj.LoginTable.First(x => x.UserName.ToLower().Trim().Equals(email));
                string password = user.Password;
                sendMailObj.SendPasswordOnEmail(user);
                return RedirectToAction("Index");

            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        #region Employee


        public ActionResult GetAllEmployees()
        {

            var employeeList = (from c in dbObj.EmployeeTable
                                select new EmployeeModelForClass
                                {
                                    EmployeeID = c.EmployeeID,
                                    CTC = c.CTC,
                                    Designation = c.Designation,
                                    DOB = c.DOB,
                                    DOJ = c.DOJ,
                                    Email = c.Email,
                                    EmployeeName = c.EmployeeName,
                                    Location = c.Location
                                }).ToList();

            return View("GetAllEmployees", employeeList);
        }

        public ActionResult CreateEmployee()
        {
            return View("Employee");
        }
        [HttpPost]
        public ActionResult CreateEmployee(EmployeeModelForClass epm)
        {
            //if (ModelState.IsValid)
            //{
            if (epm.EmployeeID == 0)
            {
                try
                {
                    EmployeeDBModel employeeObject = new EmployeeDBModel();

                    employeeObject.CTC = epm.CTC;
                    employeeObject.Designation = epm.Designation;
                    employeeObject.DOB = epm.DOB;
                    employeeObject.DOJ = epm.DOJ;
                    employeeObject.Email = epm.Email;
                    employeeObject.EmployeeName = epm.EmployeeName;
                    employeeObject.Location = epm.Location;

                    dbObj.EmployeeTable.Add(employeeObject);



                    // dbObj.SaveChanges();

                    ProfileImageModelForClass imageModel = new ProfileImageModelForClass();

                    HttpPostedFileBase file = Request.Files["ImageData"];
                    imageModel.EmployeeImage = ConvertToBytes(file);

                    ProfileImageDBModel dbMo = new ProfileImageDBModel();
                    dbMo.EmployeeImage = imageModel.EmployeeImage;
                    dbMo.Emloyee = employeeObject;

                    dbObj.ProfileImageTable.Add(dbMo);
                    dbObj.SaveChanges();



                    return RedirectToAction("GetAllEmployees");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("GetAllEmployees");
                }
            }
            else
            {
                var empToEdit = dbObj.EmployeeTable.Find(epm.EmployeeID);
                empToEdit.CTC = epm.CTC;
                empToEdit.Designation = epm.Designation;
                empToEdit.DOB = epm.DOB;
                empToEdit.DOJ = epm.DOJ;
                empToEdit.Email = epm.Email;
                empToEdit.EmployeeName = epm.EmployeeName;
                empToEdit.Location = epm.Location;


                ProfileImageModelForClass imageModel = new ProfileImageModelForClass();

                HttpPostedFileBase file = Request.Files["ImageData"];
                imageModel.EmployeeImage = ConvertToBytes(file);



                bool imageisDere = dbObj.ProfileImageTable.Any(x => x.Emloyee.EmployeeID == empToEdit.EmployeeID);


                if (imageisDere == true)
                {
                    var imageDb = dbObj.ProfileImageTable.First(x => x.Emloyee.EmployeeID == empToEdit.EmployeeID);
                    imageDb.EmployeeImage = imageModel.EmployeeImage;
                    imageDb.Emloyee = empToEdit;
                }
                else
                {
                    ProfileImageDBModel dbMo = new ProfileImageDBModel();
                    dbMo.EmployeeImage = imageModel.EmployeeImage;
                    dbMo.Emloyee = empToEdit;
                    dbObj.ProfileImageTable.Add(dbMo);
                }

                dbObj.SaveChanges();
                dbObj.SaveChanges();
                return RedirectToAction("GetAllEmployees");
            }
            //}
            //else
            //{
            //    return View("Employee");
            //}
        }

        public ActionResult EditEmployee(int id)
        {
            EmployeeModelForClass epm = new EmployeeModelForClass();
            var empToEdit = dbObj.EmployeeTable.Find(id);
            epm.CTC = empToEdit.CTC;
            epm.Designation = empToEdit.Designation;
            epm.DOB = empToEdit.DOB;
            epm.DOJ = empToEdit.DOJ;
            epm.Email = empToEdit.Email;
            epm.EmployeeName = empToEdit.EmployeeName;
            epm.Location = empToEdit.Location;
            epm.EmployeeID = empToEdit.EmployeeID;
            return View("Employee", epm);
        }
        #endregion

        //[Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }

        #region Vacancy Create/Edit


        [HttpGet]
        public ActionResult Vacancy()
        {
            return View();
        }

        public ActionResult EditVacancy(int id)
        {
            var vacancyToEdit = dbObj.VacancyTable.Find(id);
            VacancyModelForClass vacancyObj = new VacancyModelForClass();
            vacancyObj.ExpRequired = vacancyToEdit.ExpRequired;
            vacancyObj.Domain = vacancyToEdit.Domain;
            vacancyObj.Location = vacancyToEdit.Location;
            vacancyObj.NumPosition = vacancyToEdit.NumPosition;
            vacancyObj.PostingDate = vacancyToEdit.PostingDate;
            vacancyObj.Skills = vacancyToEdit.Skills;
            vacancyObj.VacancyTitle = vacancyToEdit.VacancyTitle;
            vacancyObj.VacancyID = id;
            return View("Vacancy", vacancyObj);
        }

        [HttpPost]
        public ActionResult Vacancy(VacancyModelForClass vm)
        {
            //if (ModelState.IsValid == false)
            //{
            //    return RedirectToAction("Vacancy");
            //}
            //else
            //{
            if (vm.VacancyID == 0)
            {
                VacancyModelDB vacancyDb = new VacancyModelDB();
                vacancyDb.ExpRequired = vm.ExpRequired;
                vacancyDb.Domain = vm.Domain;
                vacancyDb.Location = vm.Location;
                vacancyDb.NumPosition = vm.NumPosition;
                vacancyDb.PostingDate = vm.PostingDate;
                vacancyDb.Skills = vm.Skills;
                vacancyDb.VacancyTitle = vm.VacancyTitle;
                dbObj.VacancyTable.Add(vacancyDb);
                dbObj.SaveChanges();
                return RedirectToAction("GetAllAvailableVacancies");
            }
            else
            {
                var vacancyToUpdate = dbObj.VacancyTable.Find(vm.VacancyID);
                vacancyToUpdate.VacancyTitle = vm.VacancyTitle;
                vacancyToUpdate.Location = vm.Location;
                vacancyToUpdate.Skills = vm.Skills;
                vacancyToUpdate.PostingDate = vm.PostingDate;
                vacancyToUpdate.NumPosition = vm.NumPosition;
                vacancyToUpdate.ExpRequired = vm.ExpRequired;
                vacancyToUpdate.Domain = vm.Domain;
                //dbObj.Entry(vacancyToUpdate).State = System.Data.Entity.EntityState.Modified;
                dbObj.SaveChanges();
                return RedirectToAction("GetAllAvailableVacancies");
            }
            // }
        }

        public ActionResult GetAllAvailableVacancies()
        {
            var vacancyList = (from vm in dbObj.VacancyTable
                               select new VacancyModelForClass
                               {
                                   ExpRequired = vm.ExpRequired,
                                   Domain = vm.Domain,
                                   Location = vm.Location,
                                   NumPosition = vm.NumPosition,
                                   PostingDate = vm.PostingDate,
                                   Skills = vm.Skills,
                                   VacancyTitle = vm.VacancyTitle,
                                   VacancyID = vm.VacancyID
                               }).ToList();
            return View(vacancyList);
        }
        #endregion

        #region CandidateProfileCreate/Edit

        public ActionResult CreateProfile()
        {
            var vacancyList = dbObj.VacancyTable.ToList();
            CandidateProfileModelForClass canObj = new CandidateProfileModelForClass();
            canObj.vacancyList = vacancyList;
            return View(canObj);
        }

        public ActionResult EditProfile(int id)
        {
            var vacancyList = dbObj.VacancyTable.ToList();

            var profileToEdit = dbObj.CandidateProfileTable.Find(id);

            var item = (from c in dbObj.CandidateProfileTable
                        where c.CanProfileID == id
                        select new CandidateProfileModelForClass
                        {
                            CandidateName = c.CandidateName,
                            CanProfileID = c.CanProfileID,
                            DOB = c.DOB,
                            Gender = c.Gender,
                            Location = c.Location,
                            Email = c.MailAddress,
                            Percentage10 = c.Percentage10,
                            Percentage12 = c.Percentage12,
                            TotalExp = c.TotalExp,
                            VacancyID = c.VacancyID.VacancyID   //Issue is here
                        }).FirstOrDefault();

            item.vacancyList = vacancyList;
            return View("CreateProfile", item);
        }

        [HttpPost]
        public ActionResult CreateProfile(CandidateProfileModelForClass cpm)
        {
            CandidateProfileDBModel canDb = new CandidateProfileDBModel();
            VacancyModelDB vDB = new VacancyModelDB();
            if (cpm.CanProfileID == 0)
            {

                canDb.CandidateName = cpm.CandidateName;
                canDb.DOB = cpm.DOB;
                canDb.Gender = cpm.Gender;
                canDb.Location = cpm.Location;
                canDb.Percentage10 = cpm.Percentage10;
                canDb.Percentage12 = cpm.Percentage12;
                canDb.MailAddress = cpm.Email;
                canDb.TotalExp = cpm.TotalExp;
                vDB.VacancyID = cpm.VacancyID;
                canDb.VacancyID = dbObj.VacancyTable.First(c => c.VacancyID == cpm.VacancyID);
                //canDb.VacancyID.VacancyID = cpm.VacancyID;     //Issue is here

                dbObj.CandidateProfileTable.Add(canDb);
                dbObj.SaveChanges();
               // return RedirectToAction("GetAllCandidatesProfile");
                return View("ThanksForApplying");
            }
            else
            {
                var candidateToEdit = dbObj.CandidateProfileTable.Find(cpm.CanProfileID);
                candidateToEdit.CandidateName = cpm.CandidateName;
                candidateToEdit.DOB = cpm.DOB;
                candidateToEdit.MailAddress = cpm.Email;
                candidateToEdit.Gender = cpm.Gender;
                candidateToEdit.Location = cpm.Location;
                candidateToEdit.Percentage10 = cpm.Percentage10;
                candidateToEdit.Percentage12 = cpm.Percentage12;
                candidateToEdit.TotalExp = cpm.TotalExp;

                vDB.VacancyID = cpm.VacancyID;
                candidateToEdit.VacancyID = dbObj.VacancyTable.First(c => c.VacancyID == cpm.VacancyID);


                //  candidateToEdit.VacancyID.VacancyID = cpm.VacancyID;   //Issue is here
                dbObj.SaveChanges();
               // return RedirectToAction("GetAllCandidatesProfile");
                return View("ThanksForApplying");
            }
        }

        public ActionResult GetAllCandidatesProfile()
        {

            var candidateList = (from c in dbObj.CandidateProfileTable
                                 select new CandidateProfileModelForClass
         {
             CandidateName = c.CandidateName,
             CanProfileID = c.CanProfileID,
             DOB = c.DOB,
             Gender = c.Gender,
             Location = c.Location,
             Percentage10 = c.Percentage10,
             Percentage12 = c.Percentage12,
             Email = c.MailAddress,
             TotalExp = c.TotalExp,
             VacancyID = c.VacancyID.VacancyID
         }).ToList();
            return View(candidateList);
        }
        #endregion

        #region Create Test schedule

        public ActionResult ScheduleTest()
        {
            PlacementDBTestForClass testObj = new PlacementDBTestForClass();
            testObj.vacancyList = dbObj.VacancyTable.ToList();


            testObj.candidateList = dbObj.CandidateProfileTable.ToList();


            testObj.employeeList = dbObj.EmployeeTable.ToList();

            return View(testObj);
        }



        [HttpPost]
        public ActionResult ScheduleTest(PlacementDBTestForClass pt)
        {
            if (pt.TestID == 0)
            {
                //sendMailObj.SendMailFromApplication(pt);
                PlacementDBTest testDb = new PlacementDBTest();
                testDb.HrInterviewDate = pt.HrInterviewDate;
                testDb.SelectCandidate = dbObj.CandidateProfileTable.First(x => x.CanProfileID == pt.SelectCandidate);
                testDb.TechnicalInterviewDate = pt.TechnicalInterviewDate;
                testDb.TestAdministrator = dbObj.EmployeeTable.First(x => x.EmployeeID == pt.TestAdministrator);
                testDb.TestDate = pt.TestDate;
                testDb.Vacancy = dbObj.VacancyTable.First(c => c.VacancyID == pt.Vacancy);
                dbObj.TestTable.Add(testDb);
                dbObj.SaveChanges();
                return RedirectToAction("ScheduledTests");
            }

            else
            {
                var testDb = dbObj.TestTable.Find(pt.TestID);
                testDb.HrInterviewDate = pt.HrInterviewDate;

                testDb.SelectCandidate = dbObj.CandidateProfileTable.First(x => x.CanProfileID == pt.SelectCandidate);
                //Issue is here
                testDb.TechnicalInterviewDate = pt.TechnicalInterviewDate;
                testDb.TestAdministrator = dbObj.EmployeeTable.First(x => x.EmployeeID == pt.TestAdministrator);
                testDb.TestDate = pt.TestDate;
                testDb.Vacancy = dbObj.VacancyTable.First(c => c.VacancyID == pt.Vacancy);
                dbObj.SaveChanges();
                return RedirectToAction("ScheduledTests");
            }

        }

        public ActionResult ScheduledTests()
        {
            var testList = (from c in dbObj.TestTable
                            join d in dbObj.VacancyTable
                            on c.Vacancy.VacancyID equals d.VacancyID
                            join e in dbObj.EmployeeTable
                            on c.TestAdministrator.EmployeeID equals e.EmployeeID
                            join f in dbObj.CandidateProfileTable
                            on c.SelectCandidate.CanProfileID equals f.CanProfileID

                            select new PlacementDBTestForClass
                            {
                                HrInterviewDate = c.HrInterviewDate,
                                TestDate = c.TestDate,
                                TestID = c.TestID,
                                VacancyTitle = d.VacancyTitle,
                                CandidateName = f.CandidateName,
                                TechnicalInterviewDate = c.TechnicalInterviewDate,
                                EmployeeName = e.EmployeeName

                            }).ToList();



            return View(testList);
        }

        public ActionResult EditScheduleTest(int id)
        {
            var testToEdit = (from c in dbObj.TestTable
                              where c.TestID == id
                              select new PlacementDBTestForClass
                              {
                                  HrInterviewDate = c.HrInterviewDate,
                                  TestDate = c.TestDate,
                                  TestID = c.TestID,
                                  Vacancy = c.Vacancy.VacancyID,
                                  SelectCandidate = c.SelectCandidate.CanProfileID,
                                  TechnicalInterviewDate = c.TechnicalInterviewDate,
                                  TestAdministrator = c.TestAdministrator.EmployeeID,


                              }).FirstOrDefault();
            testToEdit.vacancyList = dbObj.VacancyTable.ToList();
            testToEdit.candidateList = dbObj.CandidateProfileTable.ToList();
            testToEdit.employeeList = dbObj.EmployeeTable.ToList();
            return View("ScheduleTest", testToEdit);

        }
        #endregion

        #region Export/Import Employees


        public ActionResult ExportEmployees()
        {
            ExcelOperations excelObj = new ExcelOperations();
            excelObj.ExportEmployees();
            ViewBag.Message = "Excel created in downloads folder";
            return RedirectToAction("GetAllEmployees");
        }

        public ActionResult ImportEmployees()
        {
            ExcelOperations excelObj = new ExcelOperations();
            excelObj.ImportEmployees();
            ViewBag.Message = "Employees Record Created From Excel";
            return RedirectToAction("GetAllEmployees");
        }

        #endregion

        #region Generate Offer letter
        public ActionResult GenerateOfferLetter()
        {
            EmployeeModelForClass emp = new EmployeeModelForClass();
            emp.CTC = 5000000;
            emp.Designation = "System Engineer";
            emp.EmployeeName = "Amrit Chaurasiya";
            emp.DOB = DateTime.Parse("16-04-1991");
            emp.DOJ = DateTime.Parse("02-01-2017");
            emp.Location = "Pune";
            emp.Email = "rajat.sedate@gmail.com";
            emp.EmployeeID = 1;


            return View(emp);
        }

        public ActionResult MailOfferLetter(EmployeeModelForClass empModel)
        {
            // sendMailObj.ConvertHTMLToPdf(empModel);
            // sendMailObj.SendOfferLetterFromMail(empModel);
            ViewBag.Message = "Offer Letter Successfully Mailed To Employee...";
            return RedirectToAction("GenerateOfferLetter");
        }


        #endregion

       
        public ActionResult MyProfile()
        {
            var employee = dbObj.EmployeeTable.Find(GetIdFromEmail(User.Identity.Name));
            EmployeeModelForClass empModel = new EmployeeModelForClass();

            if (dbObj.ProfileImageTable.Any(x => x.Emloyee.EmployeeID == employee.EmployeeID))
            {
                var image = dbObj.ProfileImageTable.First(x => x.Emloyee.EmployeeID == employee.EmployeeID);
                empModel.Image = image;
            }

            var username = dbObj.LoginTable.First(x => x.UserName == employee.Email);
            empModel.Email = employee.Email;
            empModel.EmployeeName = employee.EmployeeName;
            empModel.EmployeeID = employee.EmployeeID;
            empModel.Designation = employee.Designation;
            empModel.DOB = employee.DOB;
            empModel.DOJ = employee.DOJ;
            empModel.Location = employee.Location;
            empModel.UserName = username;

            if (empModel.Image != null)
            {
                byte[] imageByteData = empModel.Image.EmployeeImage;
                string imageBase64Data = Convert.ToBase64String(imageByteData);
                string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                ViewBag.ImageData = imageDataURL;
            }

            return View(empModel);
        }
        [HttpPost]
        public ActionResult UploadImage(ProfileImageModelForClass imageModel)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            imageModel.EmployeeImage = ConvertToBytes(file);

            ProfileImageDBModel db = new ProfileImageDBModel();
            db.EmployeeImage = imageModel.EmployeeImage;

            dbObj.ProfileImageTable.Add(db);
            dbObj.SaveChanges();
            return View("MyProfile", imageModel);
        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }
        public ActionResult SendMailsToEmployees()
        {
            var emailList = dbObj.EmployeeTable.Select(x => x.Email).ToList();

            MailFieldsModel mailModel = new MailFieldsModel();
            mailModel.Emails = emailList;
            return View(mailModel);
        }

        [HttpPost]
        public ActionResult SendMails(MailFieldsModel mailModel)
        {
            sendMailObj.SendMailToEmployee(mailModel.To, mailModel.Subject, mailModel.Message);
            @ViewBag.Msg = "Mail Successfully Sent !!!";
            return RedirectToAction("SendMailsToEmployees");
        }

        #region Submit Evaluation


        public ActionResult SubmitEvaluation()
        {
            EvaluationModelForClass testObj = new EvaluationModelForClass();

            testObj.CandidateIDList = dbObj.CandidateProfileTable.ToList();

            return View(testObj);
        }
        [HttpPost]
        public ActionResult SubmitEvaluation(EvaluationModelForClass em)
        {
            CandidateEvaluationDBModel evDb = new CandidateEvaluationDBModel();
            evDb.CandidateID = dbObj.CandidateProfileTable.First(x => x.CanProfileID == em.CandidateID);
            evDb.Comments = em.Comments;
            evDb.InterviewResult = em.InterviewResult;
            evDb.DesignationOffered = em.DesignationOffered;
            evDb.DOJ = em.DOJ;
            evDb.LocationOffered = em.LocationOffered;
            evDb.SalaryAgreed = em.SalaryAgreed;
            dbObj.EvaluationTable.Add(evDb);
            dbObj.SaveChanges();

            if (em.InterviewResult.Equals("Selected"))
            {
                var candidateEvaluated = dbObj.CandidateProfileTable.Find(em.CandidateID);

                EmployeeDBModel newEmp = new EmployeeDBModel();
                newEmp.CTC = Convert.ToInt32(em.SalaryAgreed);
                newEmp.EmployeeName = candidateEvaluated.CandidateName;
                newEmp.Location = em.LocationOffered;
                newEmp.DOB = candidateEvaluated.DOB;
                newEmp.DOJ = em.DOJ;
                newEmp.Designation = em.DesignationOffered;
                newEmp.Email = candidateEvaluated.MailAddress;

                dbObj.EmployeeTable.Add(newEmp);
                dbObj.SaveChanges();

                dbObj.CandidateProfileTable.Remove(candidateEvaluated);
                dbObj.SaveChanges();
            }

            return RedirectToAction("GetAllCandidatesProfile");
        }

        public ActionResult SubmitEvaluationFromList(int id)
        {
            EvaluationModelForClass testObj = new EvaluationModelForClass();

            testObj.CandidateIDList = dbObj.CandidateProfileTable.ToList();
            testObj.CandidateID = id;
            return View("SubmitEvaluation", testObj);

        }
        #endregion

        public int GetIdFromEmail(string email)
        {
            int userId = 0;

            var user = dbObj.EmployeeTable.First(x => x.Email.ToLower().Equals(email));
            userId = user.EmployeeID;
            return userId;
        }


    }



}