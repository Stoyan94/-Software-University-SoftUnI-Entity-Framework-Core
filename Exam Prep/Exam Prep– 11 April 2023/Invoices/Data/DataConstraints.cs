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
        public const byte AdressCityMinLength = 5;
        public const byte AdressCityMaxLength = 15;
        public const byte AdressCountryMinLength = 5;
        public const byte AdressCountryMaxLength = 15;

        // Invoice
        public const int InvoiceNumberMinLength = 1000000;
        public const int InvoiceNumberMaxLength = 1500000;

        // Client
        public const byte ClientNameMinLength = 10;
        public const byte ClientNameMaxLength = 25;
        public const byte ClientNumberVatMinLength = 10;
        public const byte ClientNumberVatMaxLength = 15;

    }
}
