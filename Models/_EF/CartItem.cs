using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models.EF;

namespace WebApplication.Models._EF
{
    [Serializable]
    public class CartItem
    {
        public Sach sach  { get; set; }

        public int Quantity { get; set; }

    }
}