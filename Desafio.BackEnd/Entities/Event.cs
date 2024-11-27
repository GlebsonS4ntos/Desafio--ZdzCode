using System.Text.Json.Serialization;

namespace Desafio.BackEnd.Entities
{
    public class Event
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Place {  get; set; }
        public long PanelistId { get; set; }
        [JsonIgnore]
        public virtual Panelist Panelist { get; set; }
    }
}
