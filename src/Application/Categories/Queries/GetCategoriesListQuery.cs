using MediatR;

namespace Application.Categories.Queries
{
    public class GetCategoriesListQuery : IRequest<CategoriesListVm>
    {
    }
}