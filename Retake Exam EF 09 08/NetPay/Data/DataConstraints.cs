using AutoMapper.Execution;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.Intrinsics.X86;
using System.Threading;

namespace NetPay.Data
{
    public static class DataConstraints
    {
        // Household
        public const byte HouseholdContactPersonMinLenght = 5;
        public const byte HouseholdContactPersonMaxLenght = 50;
        public const byte HouseholdEmailMinLenght = 6;
        public const byte HouseholdEmailMaxLenght = 80;
        public const byte HouseholdPhoneNumberMaxLenght = 15;

        public const string RegexHouseholdPhonePattern = @"^\+\d{3}/\d{3}-\d{6}$";


        // Expense
        public const byte ExpenseNameMinLenght = 5;
        public const byte ExpenseNameMaxLenght = 50;
        public const double ExpenAmountMinRange = 0.01;
        public const double ExpenAmountMaxRange = 100_000;


        // Service
        public const byte ServiceNameMinLenght = 5;
        public const byte ServiceNameMaxLenght = 30;

        // Supplier
        public const byte SupplierNameMinLenght = 3;
        public const byte SupplierNameMaxLenght = 60;
        
    }
}
