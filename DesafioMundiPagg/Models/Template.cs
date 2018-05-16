namespace DesafioMundiPagg.Models
{
    public class Template
    {
        public enum Types
        {
            XML,
            JSON
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Content { get; set; }
        public Types Type { get; set; }
    }



}
