﻿using System.ComponentModel.DataAnnotations;

namespace eTickets.V8.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }
        public Movie Movie { get; set; }

        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }

    }
}
