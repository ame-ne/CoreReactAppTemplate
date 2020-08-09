using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreReactApp.Domain.Models
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //TODO: не работает почему-то
        [Key]
        public Guid Id { get; set; }
    }
}
