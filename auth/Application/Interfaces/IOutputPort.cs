using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IOutputPort<in TUseCaseResponse>
    {
        void Handle(TUseCaseResponse response);
    }
}
