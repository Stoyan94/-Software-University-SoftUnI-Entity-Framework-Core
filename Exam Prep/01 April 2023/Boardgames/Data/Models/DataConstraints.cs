namespace Boardgames.Data.Models
{
    public static class DataConstraints
    {
        // BoardGame
        public const byte BoardGameNameMinLenght = 10;
        public const byte BoardGameNameMaxLenght = 20;
        public const double BoardGameRatingMinRange = 1.00;
        public const double BoardGameRatingMaxRange = 10.00;
        public const int BoardGameYearPublishedMinRange = 2018;
        public const int BoardGameYearPublishedMaxRange = 2023;


        // Seller
        public const byte SellerNameMinLenght = 5;
        public const byte SellerNameMaxLenght = 20;
        public const byte SellerAddressMinLenght = 2;
        public const byte SellerAddressMaxLenght = 30;

        public const string SellerWebsiteRegexPattern = @"(www\.[a-zA-Z0-9\-]{2,256}\.com)";


        //Creator
        public const byte CreatorFirstNameMinLenght = 2;
        public const byte CreatorFirstNameMaxLenght = 7;

        public const byte CreatorLastNameMinLenght = 2;
        public const byte CreatorLastNameMaxLenght = 7;
    }
}
