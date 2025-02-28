﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class CustomerMovement : IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int? SaleId { get; set; }
        public byte ProcessType { get; set; }
        public Decimal Amount { get; set; }
        public string? ProductInformation { get; set; }
        public string? Note { get; set; }
        public bool Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CreateUserId { get; set; }
        public int UpdateUserId { get; set; }
        public bool Deleted { get; set; }
    }
}
