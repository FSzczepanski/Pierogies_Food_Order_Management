using System;

namespace CleanArchitecture.Domain.Common
{
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class AuditableEntity
    {
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdentityNumber { get; set; }
        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public string LastModifiedBy { get; set; }
    }
}
