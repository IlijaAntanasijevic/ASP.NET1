using App.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class EmailConfirmation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; }
        public DateTime Expire {  get; set; }

        public virtual User User { get; set; }
    }
}
