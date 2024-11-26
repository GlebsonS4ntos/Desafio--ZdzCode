using Desafio.BackEnd.Entities;

namespace Desafio.BackEnd.Dtos
{
    public class EventDto
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Date { get; set; }
        public string? Place { get; set; }
        public long? PanelistId { get; set; }
        public Panelist? Panelist { get; set; }
    }
}
