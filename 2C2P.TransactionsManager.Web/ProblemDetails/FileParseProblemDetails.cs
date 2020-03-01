using _2C2P.TransactionsManager.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace _2C2P.TransactionsManager.Web.ProblemDetails
{
    public class FileParseProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public FileParseProblemDetails(FileParseException exception)
        {
            this.Title = exception.Message;
            this.Status = StatusCodes.Status400BadRequest;
            this.Detail = "See errorRows property for details";
            this.Type = "https://2c2p.transactionsmanager/validation-error";
            this.Extensions.Add("errorRows", exception.ErrorResults);
        }
    }
}