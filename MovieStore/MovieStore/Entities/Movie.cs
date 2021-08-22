using MovieStore.Application.ActorOperations.Queries.GetActors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Entities
{
    public class Movie
    {


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime Year { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public int DirectorId { get; set; }
        public Director Director { get; set; }


        public bool IsActive { get; set; } = true;

        public float Price { get; set; }

        public virtual ICollection<MovieActor> MovieActors { get; set; }


        public virtual ICollection<MovieCustomer> MovieCustomers { get; set; }

        public virtual ICollection<OrderCustomer> OrderCustomers { get; set; }



    }
}
