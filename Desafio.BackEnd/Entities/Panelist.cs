namespace Desafio.BackEnd.Entities
{
    public class Panelist
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Job {  get; set; }   
        public List<Event> Events { get; set; } 
    }
}
