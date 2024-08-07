namespace Footballers.DataProcessor
{
    using Footballers.Data;
    using Footballers.Data.Models;
    using Footballers.DataProcessor.ImportDto;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCoach
            = "Successfully imported coach - {0} with {1} footballers.";

        private const string SuccessfullyImportedTeam
            = "Successfully imported team - {0} with {1} footballers.";

        public static string ImportCoaches(FootballersContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRootName = "Coaches";

            List<Coach> coachesImportToDb = new List<Coach>();

            var coachesAndFootballersDtos = xmlHelper
                    .Deserialize<ImportCoachesDto[]>(xmlString, xmlRootName);

            foreach (var coachesDto in coachesAndFootballersDtos)
            {
                if (!IsValid(coachesDto))
                {
                    sb.Append(ErrorMessage);
                    continue;
                }

                Coach coachToAdd = new Coach()
                {
                    Name = coachesDto.Name,
                    Nationality = coachesDto.Nationality
                };

                foreach (var footballersDto in coachesDto.Footballers)
                {
                    if (!IsValid(footballersDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime footballerContractStartDate;
                    bool isFootballerContractStartDateValid = DateTime.TryParseExact(footballersDto.ContractStartDate,
                        "dd/MM/yyyy", CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out footballerContractStartDate);
                    if (!isFootballerContractStartDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime footballerContractEndDate;
                    bool isFootballerContractEndDateValid = DateTime.TryParseExact(footballersDto.ContractEndDate,
                        "dd/MM/yyyy", CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out footballerContractEndDate);
                    if (!isFootballerContractEndDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (footballerContractStartDate >= footballerContractEndDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Footballer footballerToAdd = new Footballer()
                    {

                    };
                }
                
            }

            return null;
        }

        public static string ImportTeams(FootballersContext context, string jsonString)
        {
            throw new NotImplementedException();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
