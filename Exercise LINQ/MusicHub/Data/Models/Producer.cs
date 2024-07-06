using System.ComponentModel.DataAnnotations;

namespace MusicHub.Data.Models;

public class Producer
{
    [Key]
    public int Id { get; set; }

    [MaxLength(ValidationsConstants.ProducerNameMaxLength)]
    public string Name { get; set; } = null!;

    public string? Pseudonym { get; set; }

    public string? PhoneNumber { get; set; }
}
