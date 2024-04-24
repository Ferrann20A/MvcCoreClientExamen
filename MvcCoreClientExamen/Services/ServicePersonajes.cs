using MvcCoreClientExamen.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcCoreClientExamen.Services
{
    public class ServicePersonajes
    {
        private MediaTypeWithQualityHeaderValue header;
        private string UrlPersonajes;

        public ServicePersonajes(IConfiguration configuration)
        {
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
            this.UrlPersonajes = configuration.GetValue<string>("ApiUrls:ApiPersonajes");
        }

        //METODO PARA NO REPETIR CODIGO Y SER MAS EFICIENTE
        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlPersonajes);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "api/personajes";
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task<Personaje> FindPersonajeAsync(int id)
        {
            string request = "api/personajes/" + id;
            Personaje per = await this.CallApiAsync<Personaje>(request);
            return per;
        }

        public async Task<List<string>> GetSeriesAsync()
        {
            string request = "api/personajes/series";
            List<string> series = await this.CallApiAsync<List<string>>(request);
            return series;
        }

        public async Task<List<Personaje>> GetPersonajesBySerieAsync(string serie)
        {
            string request = "api/personajes/personajesserie/" + serie;
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task InsertPersonaje(int id, string nombre, string imagen, string serie)
        {
            using(HttpClient client = new HttpClient())
            {
                string request = "api/personajes";
                client.BaseAddress = new Uri(this.UrlPersonajes);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                Personaje per = new Personaje
                {
                    IdPersonaje = id,
                    Nombre = nombre,
                    Imagen = imagen,
                    Serie = serie
                };
                string json = JsonConvert.SerializeObject(per);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }

        public async Task UpdatePersonajeAsync(int id, string nombre, string imagen, string serie)
        {
            using(HttpClient client = new HttpClient())
            {
                string request = "api/personajes";
                client.BaseAddress = new Uri(this.UrlPersonajes);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                Personaje per = new Personaje
                {
                    IdPersonaje = id,
                    Nombre = nombre,
                    Imagen = imagen,
                    Serie = serie
                };
                string json = JsonConvert.SerializeObject(per);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }

        public async Task DeletePersonajeAsync(int id)
        {
            using(HttpClient client = new HttpClient())
            {
                string request = "api/personajes/deletepersonaje/" + id;
                client.BaseAddress = new Uri(this.UrlPersonajes);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }
    }
}
