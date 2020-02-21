using System;
using Application.Common.Interfaces;

namespace Infrastructure.Common.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}