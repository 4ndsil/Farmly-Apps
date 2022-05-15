using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmlyCore.Application.Requests.Adverts
{
    public class DeleteAdvertItemRequest
    {
        public DeleteAdvertItemRequest(int advertItemId)
        {
            AdvertItemId = advertItemId;
        }

        public int AdvertItemId { get; }
    }

    public class DeleteAdvertItemResult
    {
        private DeleteAdvertItemResult() { }

        private DeleteAdvertItemResult(ProblemDetails problem) { Problem = problem; }

        public static DeleteAdvertItemResult WithSuccess() => new DeleteAdvertItemResult();

        public static DeleteAdvertItemResult WithProblem(ProblemDetails problem) => new DeleteAdvertItemResult(problem);

        public ProblemDetails? Problem { get; }

        public enum ProblemDetails
        {
            AdvertItemNotFound
        }
    }
}