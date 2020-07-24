using MediatR;
using SampleMR.Models;
using SampleMR.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SampleMR.CQRS.EmptyCartCommand
{
    public class EmptyCartCommand : IRequest
    {
        public int UserId { get; set; }
        public EmptyCartCommand()
        {

        }
    }

    public class EmptyCartCommandHandler
        : AsyncRequestHandler<EmptyCartCommand>
    {
        public FakeCartService FakeCartService { get; }

        public EmptyCartCommandHandler(FakeCartService fakeCartService)
        {
            FakeCartService = fakeCartService;
        }
        protected override Task Handle(EmptyCartCommand request, CancellationToken cancellationToken)
        {
            FakeCartService.ClearCart();
            Console.WriteLine($"Cart cleared");
            return Task.FromResult(0);
        }
    }
}
