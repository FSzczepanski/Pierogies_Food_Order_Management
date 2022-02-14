namespace CleanArchitecture.Domain.ValueObjects
{
    using System;
    using Common;

    public class Photo : AuditableEntity
    {
        public Guid ParentId { get; set; }
        public byte[] FileData { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public string PhotoName { get; set; }
    }
}