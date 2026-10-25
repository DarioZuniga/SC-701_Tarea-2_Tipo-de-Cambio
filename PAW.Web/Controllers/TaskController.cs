
using Task = PAW.Models.Task;
using Microsoft.AspNetCore.Mvc;

namespace PAW.Web.Controllers;

public class TaskController : Controller
{
    public IActionResult Index()
    {
        return View(); // <-- Volvemos a llamar a la vista
    }
}