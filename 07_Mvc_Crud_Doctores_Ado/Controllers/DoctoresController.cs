using _07_Mvc_Crud_Doctores_Ado.Models;
using _07_Mvc_Crud_Doctores_Ado.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _07_Mvc_Crud_Doctores_Ado.Controllers
{
    public class DoctoresController : Controller
    {
        RepositoryDoctores repo;

        public DoctoresController()
        {
            this.repo = new RepositoryDoctores();
        }

        public IActionResult Index()
        {
            List<Doctor> doctores = this.repo.GetDoctores();

            return View(doctores);
        }

        public IActionResult Create()
        {
            List<Doctor> doctores = this.repo.GetDoctores();

            List<Hospital> hospitales = this.repo.GetHospitales();
            ViewData["Hospitales"] = hospitales;

            string maxDoctorNum = this.repo.GetMaxNumDoctor();
            ViewData["Max"] = maxDoctorNum;

            return View(doctores);
        }

        [HttpPost]
        public IActionResult Create(Doctor doctor)
        {
           this.repo.CreateDoctores(doctor.HospitalCod, doctor.DoctorNum, doctor.Apellido, doctor.Especialidad, doctor.Salario);

            return RedirectToAction("Index");
        }

        public IActionResult Details(string doctorNum)
        {
            Doctor doctor = this.repo.FindDoctor(doctorNum);

            return View(doctor);
        }

        public IActionResult Delete(string doctorNum)
        {
            this.repo.DeleteDoctores(doctorNum);

            return RedirectToAction("Index");
        }

        public IActionResult Update(string doctorNum)
        {
            Doctor doctor = this.repo.FindDoctor(doctorNum);

            return View(doctor);
        }

        [HttpPost]
        public IActionResult Update(Doctor doctor)
        {
            this.repo.UpdateDoctor(doctor.HospitalCod, doctor.Apellido, doctor.Especialidad, doctor.Salario, doctor.DoctorNum);

            return RedirectToAction("Index");
        }
    }
}
