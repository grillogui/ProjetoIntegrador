using System.ComponentModel.DataAnnotations;

namespace ProjectEcommerce.src.dtos
{
    /// <summary>
    /// <para>Resume: Mirror class responsible for create a new product</para>
    /// <para>Created by: Ítalo Penha</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 05/05/2022</para>
    /// </summary>
    public class NewProductDTO
    {

        [Required, StringLength(30)]
        public string Name { get; set; }

        [Required]
        public float Price { get; set; } 

        [Required]
        public string Image { get; set; }

        [Required, StringLength(30)]
        public string Description { get; set; }

        [Required]
        public float Quantity { get; set; }

        public NewProductDTO(string name, float price, string image, string description, float quantity)
        {
            Name = name;
            Price = price;
            Image = image;
            Description = description;
            Quantity = quantity;
        }
    }

    /// <summary>
    /// <para>Resume: Mirror class responsible for update a new product</para>
    /// <para>Created by: Ítalo Penha</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 05/05/2022</para>
    /// </summary>

    public class UpdateProductDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Name { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public string Image { get; set; }

        [Required, StringLength(30)]
        public string Description { get; set; }

        [Required]
        public float Quantity { get; set; }

        public UpdateProductDTO(int id, string name, float price, string image, string description, float quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Image = image;
            Description = description;
            Quantity = quantity;
        }
    }
}