using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XamarinClient.Models;

namespace XamarinClient.Services
{
    class WebService
    {
        private HttpClient _httpclient;

        public WebService()
        {
            this._httpclient = new HttpClient();
            this._httpclient.BaseAddress = new Uri("http://localhost:50088/api/");
        }

        public async Task<List<Devise>> getAllDevisesAsync()
        {
            List<Devise> devises = null;
            HttpResponseMessage response = await _httpclient.GetAsync("Devise");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                devises = JsonConvert.DeserializeObject<List<Devise>>(content);
            }
            return devises;
        }
    }
}
     