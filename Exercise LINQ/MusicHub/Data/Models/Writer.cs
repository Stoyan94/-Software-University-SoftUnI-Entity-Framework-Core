using System.ComponentModel.DataAnnotations;

namespace MusicHub.Data.Models;

public class Writer
{
    public int Id { get; set; }

    [MaxLength(ValidationsConstants.WriterNameMaxLength)]
    public string Name { get; set; } = null!;

    public string? Pseudonym { get; set; }
}
