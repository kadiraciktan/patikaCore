
namespace MovieStore.Entities;
public class Order
{
    public int Id { get; set; }

    public int CustomerId { get; set; }
    public  Customer Customer { get; set; }

    public int MovieId { get; set; }
    public Movie Movie { get; set; }

    public float Price { get; set; }
    public DateTime BoughtTime { get; set; }
    public bool IsActive { get; set; } = true;

    public virtual ICollection<OrderCustomer> OrderCustomer { get; set; }

    
}
