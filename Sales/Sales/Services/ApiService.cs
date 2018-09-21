

namespace Sales.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Common.Models;
    using Newtonsoft.Json;

    public class ApiService
    {
        public async Task<Response> GetList<T>(
            string urlBase,
            string prefix,
            string controller)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                //var url = string.Format("{0}{1}", prefix, controller);
                var url = $"{prefix}{controller}";  // nueva forma de concatenar 
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();

                // Verifica si hubo respuesta
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(answer);

                return new Response
                {
                    IsSuccess = true,
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
