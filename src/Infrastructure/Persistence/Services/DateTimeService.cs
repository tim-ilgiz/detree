using Application.Common.Interfaces;
using detree.Application.Common.Interfaces;
using System;

namespace detree.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}