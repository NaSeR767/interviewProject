using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InterviewBackApp.Models
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            this.Id = Guid.NewGuid();
            this.CreateAt = this.CreateAt ??= DateTime.Now;
        }

        [Key]
        public Guid Id { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        [Column(TypeName = "DateTime", Order = 0)]
        public DateTime? CreateAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

    }
}
