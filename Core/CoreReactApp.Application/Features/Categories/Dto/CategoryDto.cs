using System;
using System.ComponentModel.DataAnnotations;

namespace CoreReactApp.Application.Features.Categories
{
    public sealed class CategoryDto
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
