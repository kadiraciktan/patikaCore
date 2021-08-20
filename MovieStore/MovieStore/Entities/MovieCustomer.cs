
namespace MovieStore.Entities;
public class MovieCustomer
{
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int GenreId { get; set; }
    public Genre Genre { get; set; }
}
