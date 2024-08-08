namespace Footballers.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstraints;
    public class ImportTeamsDto
    {
        [Required]
        [MinLength(TeamNameMinLenght)]
        [MaxLength(TeamNameMaxLenght)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(TeamNationalityMinLenght)]
        [MaxLength(TeamNationalityMaxLenght)]
        public string Nationality { get; set; } = null!;

        [Required]
        public int Trophies { get; set; }

        public int[] Footballers { get; set; } = null!;
    }
}
