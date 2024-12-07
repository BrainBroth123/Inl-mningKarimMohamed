using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Slutuppgift_Karim_Mohamed.Models
{
    public class LoanRegistration
    {
        public int Id { get; set; }

        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
