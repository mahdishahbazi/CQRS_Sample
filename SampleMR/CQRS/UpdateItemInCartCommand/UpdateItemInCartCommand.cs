using MediatR;
using SampleMR.Models;
using SampleMR.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SampleMR.CQRS.UpdateItemInCartCommand
{
    public class UpdateItemInCartCommand : IRequest
    {
        public int UserId { get; set; }
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateItemInCartCommandHandler
        : IRequestHandler<UpdateItemInCartCommand>
    {
        public FakeCartService FakeCartService { get; }

        public UpdateItemInCartCommandHandler(FakeCartService fakeCartService)
        {
            FakeCartService = fakeCartService;

        }
        public async Task<Unit> Handle(UpdateItemInCartCommand request, CancellationToken cancellationToken)
        {
            await FakeCartService.UpdateItem(request.ItemId, request.Quantity);
            return Unit.Value;
        }

    }
}
