using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2C2P.TransactionsManager.Controllers
{
    public class UploadTransactionFileController : Controller
    {
        public UploadTransactionFileController()
        {
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Upload(IFormFile file)
        {

            await Task.CompletedTask;
            return View();
        }
    }
}