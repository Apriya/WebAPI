using System.ComponentModel.DataAnnotations;

namespace Assessment.Models
{
    public class Books
    {
        [Key]
        public int Id { get; set; }
        public string? Publisher { get; set; }
        public string? Title { get; set; }
        public string? AuthorFirstName { get; set; }
        public string? AuthorLastName { get; set; }
        public short? PublishYear { get; set; }
        public decimal Price { get; set; }
        public string MlaCitation
        {
            get
            {
                string authors = string.Concat(AuthorFirstName, AuthorLastName);
                return $"{authors}. \"{Title}.” {Publisher}, {PublishYear}.";
            }
        }
        public string ChicagoCitation
        {
            get
            {
                string authors = string.Concat(AuthorFirstName,AuthorLastName);
                return $"{authors}. {Title}. {Publisher}, {PublishYear}.";
            }
        }
    }
}
