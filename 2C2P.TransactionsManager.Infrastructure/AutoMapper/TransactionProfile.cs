using _2C2P.TransactionsManager.Domain.Model;
using _2C2P.TransactionsManager.Infrastructure.Csv;
using _2C2P.TransactionsManager.Infrastructure.Xml;
using AutoMapper;

namespace _2C2P.TransactionsManager.Infrastructure.AutoMapper
{
    public class TransactionProfile: Profile
    {
        public TransactionProfile()
        {
            CreateMap<CsvTransactionRecord, Transaction>()
                .ForMember(t => t.Status, opt => opt.MapFrom(src => (TransactionStatus) (int) src.Status));

            CreateMap<XmlTransactionRecord, Transaction>()
                .ForMember(t => t.Status, opt => opt.MapFrom(src => (TransactionStatus) (int) src.Status))
                .ForMember(t => t.Amount, opt => opt.MapFrom(src => src.PaymentDetails.Amount))
                .ForMember(t => t.CurrencyCode, opt => opt.MapFrom(src => src.PaymentDetails.CurrencyCode));
        }
    }
}
