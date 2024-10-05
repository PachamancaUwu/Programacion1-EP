using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using examenparcial.Models;
using examenparcial.Data;
using examenparcial.Services;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace examenparcial.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly ApiService _apiService;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, ApiService apiService)
    {
        _logger = logger;
        _context = context;
        _apiService = apiService;
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
    public async Task<IActionResult> Enviar(Remesa objremesa){

        decimal precioBitcoinUSD = await _apiService.ObtenerPrecioBitcoinUSD();
        if (objremesa.TipoMoneda == "BTC") { //conversión de bitcoin a dólares
            objremesa.TasaCambio = precioBitcoinUSD;
            objremesa.MontoFinal = objremesa.MontoEnviado * objremesa.TasaCambio;
        } else {                            //conversión de dólares a bitcoins
            objremesa.TasaCambio = 1 / precioBitcoinUSD;
            objremesa.MontoFinal = objremesa.MontoEnviado * objremesa.TasaCambio;
        }

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
