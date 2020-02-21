using Domain.Common.Audit;

namespace Domain.Entities
{
    public class Card : AuditableEntity
    {
        public long Id { get; set; }

        public string CardName { get; set; }

        public string LinkUrl { get; set; }

        public string Image { get; set; }

        public long ParentId { get; set; }
        public long Click { get; set; }
    }
}