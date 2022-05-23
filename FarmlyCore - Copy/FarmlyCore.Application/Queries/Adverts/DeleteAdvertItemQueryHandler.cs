using AutoMapper;
using FarmlyCore.Application.Requests.Adverts;
using FarmlyCore.Infrastructure.FarmlyDbContext;
using FarmlyCore.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace FarmlyCore.Application.Queries.Adverts
{
    public class DeleteAdvertItemQueryHandler : IQueryHandler<DeleteAdvertItemRequest, DeleteAdvertItemResult>
    {
        private readonly FarmlyEntityDbContext _farmlyEntityDataContext;
        private readonly IMapper _mapper;

        public DeleteAdvertItemQueryHandler(IMapper mapper, FarmlyEntityDbContext farmlyEntityDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _farmlyEntityDataContext = farmlyEntityDbContext ?? throw new ArgumentNullException(nameof(farmlyEntityDbContext));
        }

        public async Task<DeleteAdvertItemResult> HandleAsync(DeleteAdvertItemRequest request, CancellationToken cancellationToken = default)
        {
            var advertItem = await _farmlyEntityDataContext.AdvertItems.Where(e => e.Id == request.AdvertItemId).FirstOrDefaultAsync();

            if (advertItem == null)
            {
                return DeleteAdvertItemResult.WithProblem(DeleteAdvertItemResult.ProblemDetails.AdvertItemNotFound);
            }

            _farmlyEntityDataContext.AdvertItems.Remove(advertItem);

            await _farmlyEntityDataContext.SaveChangesAsync();

            return DeleteAdvertItemResult.WithSuccess();
        }
    }
}