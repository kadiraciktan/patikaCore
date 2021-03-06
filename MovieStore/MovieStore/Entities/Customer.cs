using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Entities
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpireDate { get; set; }
        public List<Genre> FavoriteGenres { get; set; }
        public List<Movie> BougthMovies { get; set; }

        public virtual ICollection<MovieCustomer> Movies { get; set; }

        public virtual ICollection<OrderCustomer> OrderCustomers { get; set; }
    }
}
