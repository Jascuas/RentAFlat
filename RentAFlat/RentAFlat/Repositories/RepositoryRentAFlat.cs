using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RentAFlat.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace RentAFlat.Repositories
{
    public class RepositoryRentAFlat
    {
        String uriapi;
        private MediaTypeWithQualityHeaderValue headerjson;
        public RepositoryRentAFlat()
        {
            this.uriapi = "https://apirentflatmvc.azurewebsites.net/";
            this.headerjson = new MediaTypeWithQualityHeaderValue("application/json");
        }
        //--
        private HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );
            return client;
        }
        //--
        private async Task<T> CallApi<T>(String peticion, String token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.uriapi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(headerjson);
                if (token != null)
                {
                    client.DefaultRequestHeaders.Add("Authorization", "bearer "+ token);
                }
                HttpResponseMessage response = await client.GetAsync(peticion);
                if (response.IsSuccessStatusCode)
                {
                    T datos = await response.Content.ReadAsAsync<T>();
                    return (T)Convert.ChangeType(datos, typeof(T));
                }
                else
                {
                    return default(T);
                }
            }
        }
        public async Task<String> GetToken(String usuario, String password)
        {
            using (HttpClient client = new HttpClient())
            {
                string peticion = "login";
                client.BaseAddress = new Uri(this.uriapi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(headerjson);
                FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<String, String>("grant_type", "password")
                    ,new KeyValuePair<String, String>("username", usuario)
                    ,new KeyValuePair<String, String>("password", password)
                });
                HttpResponseMessage response = await client.PostAsync(peticion, content);
                if (response.IsSuccessStatusCode)
                {
                    String contenido = await
                    response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(contenido);
                    String token = json.GetValue("access_token").ToString();
                    return token;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<Usuario> GetUsuario(String token)
        {
            Usuario usuario = await this.CallApi<Usuario>("api/PerfilEmpleado/", token);
            return usuario;
        }
        public async Task<List<TiposVivienda>> GetTiposVivienda()
        {
            List<TiposVivienda> tiposViviendas= await this.CallApi<List<TiposVivienda>>("api/GetTiposViviendas/", null);
            return tiposViviendas;
        }
        public async Task<List<Costas>> GetCostas()
        {
            List<Costas> costas = await this.CallApi<List<Costas>>("api/GetNombreCostas/", null);
            return costas;
        }
        public async Task<List<Vivienda>> GetViviendas()
        {
            List<Vivienda> Viviendas = await this.CallApi<List<Vivienda>>("api/GetViviendas/", null);
            return Viviendas;
        }

        //--
        public async Task<List<Cliente>> GetClientes()
        {
            List<Cliente> clientes = await this.CallApi<List<Cliente>>("api/GetClientes/", null);
            return clientes;
        }
        //--
        public async Task<List<Vivienda>> GetViviendasPorFiltro(BusquedaModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                string peticion = "api/GetViviendasByFilter/";
                client.BaseAddress = new Uri(this.uriapi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(headerjson);
                string json = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(peticion, content);
                if (response.IsSuccessStatusCode)
                {
                    List<Vivienda> viviendas =
                    await response.Content.ReadAsAsync<List<Vivienda>>();
                    return viviendas;
                }
                else
                {
                    return null;
                }
            }
        }

        //----
        public async Task InsertarVivienda(Vivienda viv, string token)
        {
            //viv.Cod_casa = 0;
            string json = JsonConvert.SerializeObject(viv, Formatting.Indented);//ponemos formatting.indented
            //debemos convertir el string a byte[]
            byte[] buffervivienda = Encoding.UTF8.GetBytes(json);
            //para poder enviar objetos a nuestro servicio, se utiliza un content.
            //en xamarin es un ByteArrayContent
            ByteArrayContent content = new ByteArrayContent(buffervivienda);
            //debemos indicar el tipo de contenido que enviamos al servicio
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string peticion = "api/InsertarVivienda";
            Uri uri = new Uri(this.uriapi + peticion);

            //cogemos nuestro cliente
            HttpClient client = this.GetHttpClient();
            if (token != null)
            {
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
            }
            //cogemos la respuesta
            HttpResponseMessage response = await client.PostAsync(uri, content);


        }
        public async Task EliminarVivienda(int idvivienda, string token)
        {
            string peticion = "api/EliminarVivienda/" + idvivienda;
            Uri uri = new Uri(this.uriapi + peticion);
            HttpClient client = this.GetHttpClient();

            //cogemos nuestro cliente
            HttpClient client2 = this.GetHttpClient();
            if (token != null)
            {
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
            }

            HttpResponseMessage response = await client.DeleteAsync(uri);
        }

        public async Task ModificarVivienda(Vivienda doc, string token)
        {
            string jsondoctor = JsonConvert.SerializeObject(doc, Formatting.Indented);//ponemos formatting.indented
            //debemos convertir el string a byte[]
            byte[] bufferdoctor = Encoding.UTF8.GetBytes(jsondoctor);
            //para poder enviar objetos a nuestro servicio, se utiliza un content.
            //en xamarin es un ByteArrayContent
            ByteArrayContent content = new ByteArrayContent(bufferdoctor);
            //debemos indicar el tipo de contenido que enviamos al servicio
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string peticion = "api/ModificarVivienda/" + doc.Cod_casa;
            Uri uri = new Uri(this.uriapi + peticion);

            //cogemos nuestro cliente
            HttpClient client = this.GetHttpClient();
            if (token != null)
            {
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
            }
            //cogemos la respuesta
            HttpResponseMessage response = await client.PutAsync(uri, content);
        }
    }
}
