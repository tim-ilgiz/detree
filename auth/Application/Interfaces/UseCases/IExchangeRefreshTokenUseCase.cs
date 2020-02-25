using Application.Dto.UseCaseRequests;
using Application.Dto.UseCaseResponses;

namespace Application.Interfaces.UseCases
{
    public interface IExchangeRefreshTokenUseCase : IUseCaseRequestHandler<ExchangeRefreshTokenRequest, ExchangeRefreshTokenResponse>
    {
    }
}
