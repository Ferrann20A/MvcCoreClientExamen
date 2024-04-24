using Microsoft.AspNetCore.Mvc;
using MvcCoreClientExamen.Models;
using MvcCoreClientExamen.Services;

namespace MvcCoreClientExamen.Controllers
{
    public class PersonajesController : Controller
    {
        private ServicePersonajes service;

        public PersonajesController(ServicePersonajes service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Personajes()
        {
            List<Personaje> personajes = await this.service.GetPersonajesAsync();
            return View(personajes);
        }

        public async Task<IActionResult> BuscadorPersonajes()
        {
            List<string> series = await this.service.GetSeriesAsync();
            ViewData["series"] = series;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BuscadorPersonajes(string serie)
        {
            List<string> series = await this.service.GetSeriesAsync();
            ViewData["series"] = series;
            List<Personaje> personajesEncontrados = await this.service.GetPersonajesBySerieAsync(serie);
            return View(personajesEncontrados);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeletePersonajeAsync(id);
            return RedirectToAction("Personajes");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Personaje per)
        {
            await this.service.InsertPersonaje(per.IdPersonaje, per.Nombre, per.Imagen, per.Serie);
            return RedirectToAction("Personajes");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Personaje per = await this.service.FindPersonajeAsync(id);
            return View(per);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Personaje per)
        {
            await this.service.UpdatePersonajeAsync(per.IdPersonaje, per.Nombre, per.Imagen, per.Serie);
            return RedirectToAction("Personajes");
        }
    }
}
