using Castle.Core.Resource;
using NetPay.Data;
using NetPay.Data.Models;
using NetPay.Data.Models.Enums;
using NetPay.DataProcessor.ImportDtos;
using NetPay.Utilities;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace NetPay.DataProcessor
{
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data format!";
        private const string DuplicationDataMessage = "Error! Data duplicated.";
        private const string SuccessfullyImportedHousehold = "Successfully imported household. Contact person: {0}";
        private const string SuccessfullyImportedExpense = "Successfully imported expense. {0}, Amount: {1}";

        public static string ImportHouseholds(NetPayContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder(); ;

            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRootName = "Households";

            var houseHoldsDesDto = xmlHelper.Deserialize<ImportHouseholdsDto[]>(xmlString, xmlRootName);

            List<Household> householdsToAddToDb = new List<Household>();

            foreach (var houseHolderDto in houseHoldsDesDto)
            {
                if (!IsValid(houseHolderDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Household houseHoldToAddInList = new Household()
                {
                    ContactPerson = houseHolderDto.ContactPerson,
                    PhoneNumber = houseHolderDto.Phone,
                    Email = houseHolderDto.Email
                };


                bool isDuplicationInContext = context.Households.Any(c => c.ContactPerson == houseHoldToAddInList.ContactPerson) ||
                                             context.Households.Any(c => c.Email == houseHoldToAddInList.Email) ||
                                             context.Households.Any(c => c.PhoneNumber == houseHoldToAddInList.PhoneNumber);

                bool isDuplicationInHouseholds = householdsToAddToDb.Any(c => c.ContactPerson == houseHoldToAddInList.ContactPerson) ||
                householdsToAddToDb.Any(c => c.Email == houseHoldToAddInList.Email) ||
                householdsToAddToDb.Any(c => c.PhoneNumber == houseHoldToAddInList.PhoneNumber);

                if (isDuplicationInContext || isDuplicationInHouseholds)
                {
                    sb.AppendLine(DuplicationDataMessage);
                    continue;
                }

                householdsToAddToDb.Add(houseHoldToAddInList);
                sb.AppendLine(String.Format(SuccessfullyImportedHousehold, houseHoldToAddInList.ContactPerson));
            }
            context.Households.AddRange(householdsToAddToDb);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportExpenses(NetPayContext context, string jsonString)
        {
            StringBuilder sb  = new StringBuilder();

            var expensesDesDto = JsonConvert.DeserializeObject<ImportExpensesDto[]>(jsonString);

            List<Expense> expensesToAddInDb = new List<Expense>();

            foreach (var expensesDto in expensesDesDto)
            {
                if (!IsValid(expensesDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (context.Expenses.Any(e => e.ServiceId == expensesDto.ServiceId) || 
                    context.Expenses.Any(e => e.HouseholdId == expensesDto.HouseholdId) ||
                    expensesDto.HouseholdId == 0 || expensesDto.ServiceId == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (expensesDto.Amount <= 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isIssueDateValid = DateTime
                    .TryParseExact(expensesDto.DueDate, "yyyy-MM-dd", 
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime issueDate);

                if (!isIssueDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (!Enum.TryParse(expensesDto.PaymentStatus, out PaymentStatus status))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Expense expenseToAddToList = new Expense()
                {
                    ExpenseName = expensesDto.ExpenseName,
                    Amount = expensesDto.Amount,
                    DueDate = issueDate,
                    PaymentStatus = status,
                    HouseholdId = expensesDto.HouseholdId,
                    ServiceId = expensesDto.ServiceId
                    
                };

                expensesToAddInDb.Add(expenseToAddToList);
                sb.AppendLine(String.Format(SuccessfullyImportedExpense, expenseToAddToList.ExpenseName,expenseToAddToList.Amount));
            }
            context.Expenses.AddRange(expensesToAddInDb);
            context.SaveChanges();

            return sb.ToString();
        }

        public static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            foreach(var result in validationResults)
            {
                string currvValidationMessage = result.ErrorMessage;
            }

            return isValid;
        }
    }
}
