using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Slutuppgift_Karim_Mohamed.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<AuthorRegistration>? Registrations { get; set; }
    }
}
