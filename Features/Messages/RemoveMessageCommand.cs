using MediatR;
using NoDaysOffApp.Data;
using NoDaysOffApp.Model;
using NoDaysOffApp.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace NoDaysOffApp.Features.Messages
{
    public class RemoveMessageCommand
    {
        public class Request : BaseRequest, IRequest<Response>
        {
            public int Id { get; set; }
            public Guid CorrelationId { get; set; }
        }

        public class Response { }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Handler(NoDaysOffAppContext context, IEventBus bus)
            {
                _context = context;
                _bus = bus;
            }

            public async Task<Response> Handle(Request request)
            {
                var message = await _context.Messages.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                message.IsDeleted = true;
                await _context.SaveChangesAsync();
                _bus.Publish(new RemovedMessageMessage(request.Id, request.CorrelationId, request.TenantUniqueId));
                return new Response();
            }

            private readonly NoDaysOffAppContext _context;
            private readonly IEventBus _bus;
        }
    }
}
