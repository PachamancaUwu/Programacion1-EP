using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using examenparcial.Models;
using examenparcial.Data;

namespace examenparcial.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Formulario()
    {
        return View();
    }

    public IActionResult Listado()
    {
        //var remesaLista = _context.DataRemesa.ToList();
        //return View(remesaLista);
        return View();
    }

    
    [HttpPost]
    public IActionResult Enviar(Remesa objremesa){
        _logger.LogDebug("Ingreso a Enviar Remesa");
        _context.Add(objremesa);
        _context.SaveChanges();

        ViewData["Message"] = "La remesa se registró con éxito!";

        return View("Formulario");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
