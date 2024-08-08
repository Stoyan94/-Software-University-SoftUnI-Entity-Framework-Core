namespace Footballers.DataProcessor
{
    using Footballers.Data;
    using Footballers.Data.Models;
    using Footballers.Data.Models.Enums;
    using Footballers.DataProcessor.ImportDto;
    using Footballers.Utilities;
    using Newtonsoft.Json;
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

            foreach (var coachDto in coachesAndFootballersDtos)
            {
                if (!IsValid(coachDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                // check here later
                string nationality = coachDto.Nationality;
                bool isNationalityInvalid = string.IsNullOrEmpty(nationality);

                if (isNationalityInvalid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Coach c = new Coach()
                {
                    Name = coachDto.Name,
                    Nationality = nationality
                };

                foreach (var footballerDto in coachDto.Footballers)
                {
                    if (!IsValid(footballerDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime footballerContractStartDate;
                    bool isFootballerContractStartDateValid = DateTime.TryParseExact(footballerDto.ContractStartDate,
                        "dd/MM/yyyy", CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out footballerContractStartDate);
                    if (!isFootballerContractStartDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime footballerContractEndDate;
                    bool isFootballerContractEndDateValid = DateTime.TryParseExact(footballerDto.ContractEndDate,
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

                    Footballer f = new Footballer()
                    {
                        Name = footballerDto.Name,
                        ContractStartDate = footballerContractStartDate,
                        ContractEndDate = footballerContractEndDate,
                        BestSkillType = (BestSkillType)footballerDto.BestSkillType,
                        PositionType = (PositionType)footballerDto.PositionType
                    };

                    c.Footballers.Add(f);
                }
                coachesImportToDb.Add(c);
                sb.AppendLine(String.Format(SuccessfullyImportedCoach, c.Name, c.Footballers.Count));
            }
            context.Coaches.AddRange(coachesImportToDb);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportTeams(FootballersContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            var teamsDots = JsonConvert.DeserializeObject<ImportTeamsDto[]>(jsonString);

            List<Team> teamsToAddDb = new List<Team>();

            foreach (var teamCurrentDto in teamsDots)
            {
                if (!(IsValid(teamCurrentDto)))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Team teamToAdd = new Team()
                {
                    Name = teamCurrentDto.Name,
                    Nationality = teamCurrentDto.Nationality,
                    Trophies = teamCurrentDto.Trophies
                };

                if (teamToAdd.Trophies == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                foreach (var footballerId in teamCurrentDto.Footballers.Distinct())
                {
                    Footballer footbaler = context.Footballers.Find(footballerId);
                    if (footbaler == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    teamToAdd.TeamsFootballers.Add(new TeamFootballer()
                    {
                        Footballer = footbaler
                    });
                }
                teamsToAddDb.Add(teamToAdd);
                sb.AppendLine(String.Format(SuccessfullyImportedTeam, teamToAdd.Name, teamToAdd.TeamsFootballers.Count));
            }
            context.Teams.AddRange(teamsToAddDb);
            context.SaveChanges();

            return sb.ToString();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
