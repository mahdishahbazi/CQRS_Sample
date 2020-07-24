using MediatR;
using SampleMR.Models;
using SampleMR.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SampleMR.CQRS.DeleteItemInCartCommand
{
    public class DeleteItemInCartCommand : IRequest 
    {
        public int UserId { get; set; }
        public Guid ItemId { get; set; }
    }

    public class DeleteItemInCartCommandHandler
        : RequestHandler<DeleteItemInCartCommand>
    {
        public FakeCartService FakeCartService { get; }

        public DeleteItemInCartCommandHandler(FakeCartService fakeCartService)
        {
            FakeCartService = fakeCartService;
        }
        protected override void Handle(DeleteItemInCartCommand request)
        {
            FakeCartService.DeleteItem(request.ItemId);
            Console.WriteLine($"Delete Item {request.ItemId}");
        }
    }
}
