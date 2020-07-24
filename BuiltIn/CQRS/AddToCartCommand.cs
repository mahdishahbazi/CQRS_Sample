using BuiltIn.CQRS.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuiltIn.CQRS
{
    public class AddToCartCommand : ICommand
    {
        public string Item { get; set; }
        public int Quantity { get; set; }

        public AddToCartCommand()
        {

        }
    }

    public class AddToCartCommandHandler : ICommandHandler<AddToCartCommand>
    {
        public void Handle(AddToCartCommand command)
        {
            System.Console.WriteLine($"AddT o Cart {command.Item} {command.Quantity}");
        }
    }
}
