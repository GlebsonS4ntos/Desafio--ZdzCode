using Desafio.BackEnd.Entities;

namespace Desafio.BackEnd.Dtos
{
    public class PanelistDto
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Job { get; set; }
    }
}
