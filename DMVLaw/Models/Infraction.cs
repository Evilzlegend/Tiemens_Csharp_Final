﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DMVLaw.Models
{
    public partial class Infraction
    {
        public Infraction()
        {
            DriverInfractions = new HashSet<DriverInfraction>();
        }

        public int InfractionId { get; set; }
        public string InfractionType { get; set; }

        public virtual ICollection<DriverInfraction> DriverInfractions { get; set; }
    }
}