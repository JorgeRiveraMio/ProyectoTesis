using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoTesis.Controllers
{
    public class TestController : Controller
    {
      
        public ActionResult ComenzarTest()
        {
          
            var sessionId=Guid.NewGuid().ToString();
            HttpContext.Session.SetString("SessionId", sessionId);

            //Colocar paginas a que va hacer dirigida
            return View();  
        }


    
    }
}
