using Blood_Client.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Blood_Client.Controllers
{
    public class DoctorController : Controller
    {

        string apiUrl = "";
        IConfiguration _configuration;

      public  DoctorController(IConfiguration configuration)
        {
            this._configuration = configuration;
            this.apiUrl= _configuration.GetValue<string>("WebAPIBaseUrl");
        

        }
        public IActionResult Index()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync(apiUrl + "/Doctors").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var output = JsonConvert.DeserializeObject<List<DoctorDTO>>(response.Content.ReadAsStringAsync().Result);
                        return View(output);
                    }
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
     public IActionResult Create(DoctorDTO entity)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = client.PostAsJsonAsync(apiUrl + "/Doctors",entity).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var output = JsonConvert.DeserializeObject<DoctorDTO>(response.Content.ReadAsStringAsync().Result);
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View();
        }


    }
}
