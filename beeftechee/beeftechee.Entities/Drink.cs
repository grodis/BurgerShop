using System.ComponentModel.DataAnnotations;

namespace beeftechee.Entities
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Litre { get; set; }



        [DataType(DataType.Currency)]
        public decimal Price { get; set; }


        public string ImageUrl { get; set; }
    }
}
