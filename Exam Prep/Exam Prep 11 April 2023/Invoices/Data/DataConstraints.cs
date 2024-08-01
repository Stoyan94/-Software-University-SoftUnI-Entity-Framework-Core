using Invoices.Data.Models.Enums;

namespace Invoices.Data
{
    public static class DataConstraints
    {
        // Product
        public const byte ProductNameMinLength = 9;
        public const byte ProductNameMaxLength = 30;
        public const string ProductPriceMinValue = "5.00";
        public const string ProductPriceMaxValue = "1000.00";

        // Adress
        public const byte AddressStreetNameMinLength = 10;
        public const byte AddressStreetNameMaxLength = 20;
        public const byte AddressCityMinLength = 5;
        public const byte AddressCityMaxLength = 15;
        public const byte AddressCountryMinLength = 5;
        public const byte AddressCountryMaxLength = 15;

        // Invoice
        public const int InvoiceNumberMinLength = 1_000_000_000;
        public const int InvoiceNumberMaxLength = 1_500_000_000;
        public const int InvoiceCurrencyTypeMinValue = (int)CurrencyType.BGN;
        public const int InvoiceCurrencyTypeMaxValue = (int)CurrencyType.USD;

        // Client
        public const byte ClientNameMinLength = 10;
        public const byte ClientNameMaxLength = 25;
        public const byte ClientNumberVatMinLength = 10;
        public const byte ClientNumberVatMaxLength = 15;

    }
}
