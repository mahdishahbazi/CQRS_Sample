using MediatR;
using SampleMR.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SampleMR.CQRS.AddToCartCommand
{
    public class AddToCartCommand : IRequest<Guid>
    {
        public string Item { get; set; }
        public int Quantity { get; set; }
    }

    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, Guid>
    {
        public FakeCartService FakeCartService { get; }

        public AddToCartCommandHandler(FakeCartService fakeCartService)
        {
            FakeCartService = fakeCartService;
        }
        public Task<Guid> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var id = FakeCartService.AddItem(new Models.CartItem
            {
                Item = request.Item,
                Quantity = request.Quantity
            });
            Console.WriteLine($"Add To Cart {request.Item} {request.Quantity}");
            return Task.FromResult(id);
        }
    }
}
