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
            return View();
        }
    }
}
