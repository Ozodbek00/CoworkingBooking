﻿using CoworkingBooking.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoworkingBooking.Domain.Entities
{
    public class Order : Auditable
    {
        public DateTime StartAt { get; set; }
        public DateTime LeaveAt { get; set; }
        public long StudentId { get; set; }
        public Student Student { get; set; }
        public long BranchId { get; set; }
        public Branch Branch { get; set; }
        public long FloorId { get; set; }
        public Floor Floor { get; set; }
        public long TableId { get; set; }
        public Table Table { get; set; }
        public long ChairId { get; set; }
        public Chair Chair { get; set; }

    }
}