using System;
using System.Collections.Generic;
using System.Text;
using Application.Dto.UseCaseRequests;
using Application.Dto.UseCaseResponses;

namespace Application.Interfaces.UseCases
{
    public interface IRegisterUserUseCase : IUseCaseRequestHandler<RegisterUserRequest, RegisterUserResponse>
    {
    }
}
