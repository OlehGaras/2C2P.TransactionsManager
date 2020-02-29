﻿using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace _2C2P.TransactionsManager.Infrastructure.Csv
{
    public class CsvTransactionDtoMapping : CsvMapping<CsvTransactionRecord>
    {
        public CsvTransactionDtoMapping()
        {
            MapProperty(0, x => x.TransactionId, new MaxLengthStringConverter(50));
            MapProperty(1, x => x.Amount, new DecimalConverter());
            MapProperty(2, x => x.CurrencyCode, new Iso4217CurrencyConverter());
            MapProperty(3, x => x.TransactionDate, 
                new DateTimeConverter("dd/MM/yyyy hh:mm:ss"));
            MapProperty(4, x => x.Status, 
                new EnumConverter<CsvTransactionStatus>(true));
        }
    }
}