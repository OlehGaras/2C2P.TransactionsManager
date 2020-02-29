using _2C2P.TransactionsManager.Data.EntityFramework.Entities;
using _2C2P.TransactionsManager.Domain.Model;
using AutoMapper;

namespace _2C2P.TransactionsManager.Data.EntityFramework.AutoMapper
{
    public class DomainToEntityProfile: Profile
    {
        public DomainToEntityProfile()
        {
            CreateMap<Transaction, TransactionEntity>()
                .ForMember(t => t.Id, opt => opt.MapFrom(src => src.TransactionId))
                .ForMember(t => t.Status, opt => opt.MapFrom(src => (int) src.Status));

            CreateMap<TransactionEntity, Transaction>()
                .ForMember(t => t.TransactionId, opt => opt.MapFrom(src => src.Id))
                .ForMember(t => t.Status, opt => opt.MapFrom(src => (TransactionStatus)src.Status));
        }
    }
}
