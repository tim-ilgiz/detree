using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Category : AuditableEntity
    {
        public long Id { get; set; }

        public string CategoryName { get; set; }

        public long Parent { get; set; }

        public string Status { get; set; }
    }
}
