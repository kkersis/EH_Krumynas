﻿namespace EKrumynas.DTOs.ShoppingCart
{
    public class BouquetCartItemGetDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public BouquetGetDto Bouquet { get; set; }
        public ProductGetDto Product { get; set; }
    }
}
