namespace TravelAgency.Data
{
    public static class DataConstraints
    {
        // Customer
        public const int CustomerFullNameMinLenght = 4;
        public const int CustomerFullNameMaxLenght = 60;
        public const int CustomerEmailMinLenght = 6;
        public const int CustomerEmailMaxLenght = 50;
        public const string CustomerPhoneNumberPattern = @"^\+\d{12}$";

        // Guide
        public const byte GuideFullNameMinLenght = 4;
        public const byte GuideFullNameMaxLenght = 60;

        // TourPackage
        public const byte TourPackageNameMinLenght = 2;
        public const byte TourPackageNameMaxLenght = 40;
        public const byte TourPackageDescriptionMaxLenght = 200;
                        
    }
}
