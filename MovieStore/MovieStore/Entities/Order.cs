
namespace MovieStore.Entities;
public class Order
{
    public int Id {  get; set; }
    public int CustomerId { get; set; }
    public virtual Customer Customer {  get; set; }
    public int MovieId { get; set; }
    public virtual Movie Movie {  get; set; }
    public float Price { get; set; }
    public DateTime BoughtTime { get; set; }
   
}
