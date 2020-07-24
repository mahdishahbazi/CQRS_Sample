using BuiltIn.CQRS.Base;
using BuiltIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuiltIn.CQRS
{
    public class GetUserCartQuery : IQuery<List<CartItem>>
    {
        public int UserId { get; set; }
        public GetUserCartQuery()
        {

        }
    }

    public class GetUserCartQueryHandler 
        : IQueryHandler<GetUserCartQuery, List<CartItem>>
    {
        public List<CartItem> Handle(GetUserCartQuery command)
        {
            return new List<CartItem>()
            {
                new CartItem("T Sehart",2),
                new CartItem("Game", 2),
                new CartItem("PC", 1)
            };
        }
    }
}
