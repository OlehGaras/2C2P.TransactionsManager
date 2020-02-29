using System.Collections.Generic;
using System.Linq;

namespace _2C2P.TransactionsManager.Domain.Service
{
    public class ServiceResult<T>: ServiceResult
    {
        public T Result { get; set; }
    }

    public class ServiceResult
    {
        public List<string> Errors { get; set; } = new List<string>();
        public bool HasErrors => Errors != null && Errors.Any();
    }
}