using MediatR;
using SampleMR.Models;
using SampleMR.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SampleMR.CQRS.GetUserCartQuery
{
    public class GetUserCartQuery : IRequest<IList<CartItem>>
    {
        public int UserId { get; set; }
        public GetUserCartQuery()
        {

        }
    }

    public class GetUserCartQueryHandler
        : IRequestHandler<GetUserCartQuery, IList<CartItem>>
    {
        public GetUserCartQueryHandler(FakeCartService fakeCartService)
        {
            FakeCartService = fakeCartService;
        }

        public FakeCartService FakeCartService { get; }

        public Task<IList<CartItem>> Handle(GetUserCartQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(FakeCartService.Items);
        }
    }
}
