using System.Threading;
using System.Threading.Tasks;
using _2C2P.TransactionsManager.Domain.Service.Abstractions;
using _2C2P.TransactionsManager.Dto;
using Microsoft.AspNetCore.Mvc;

namespace _2C2P.TransactionsManager.Controllers
{
    public class UploadTransactionFileController : Controller
    {
        private readonly IUploadService _uploadService;

        public UploadTransactionFileController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Upload(UploadDocumentDto doc)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }

            using (var stream = doc.FormFile.OpenReadStream())
            {
                var serviceResult =
                    await _uploadService.UploadTransactionsFileAsync(stream, doc.GetExtension(),
                        CancellationToken.None);

                if (serviceResult.HasErrors)
                {
                    return BadRequest(serviceResult.Errors);
                }
            }

            return View();
        }
    }
}