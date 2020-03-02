using _2C2P.TransactionsManager.Domain.Model;
using Microsoft.AspNetCore.Http;

namespace _2C2P.TransactionsManager.Web.ProblemDetails
{
    public class BusinessRuleValidationProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public BusinessRuleValidationProblemDetails(BusinessRuleValidationException exception)
        {
            this.Title = exception.Message;
            this.Status = StatusCodes.Status422UnprocessableEntity;
            this.Detail = exception.Details;
            this.Type = "https://2c2p.transactionsmanager/validation-error";
        }
    }
}