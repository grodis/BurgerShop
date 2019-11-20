using System.ComponentModel.DataAnnotations.Schema;

namespace beeftechee.Entities.Ingredient_Entities
{
    public abstract class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        [NotMapped]
        public string NamePrice
        {
            get { return $"{Name} {Price}€"; }
        }
        
    }
}
