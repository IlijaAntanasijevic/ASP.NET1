using System;

namespace Domain.Core
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }

    }

    public abstract class ApplicationUser
    {
        public int UserId { get; set; }
    }
}
