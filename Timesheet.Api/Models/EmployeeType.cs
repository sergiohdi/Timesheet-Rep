﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Timesheet.Api.Models
{
    public partial class EmployeeType
    {
        public EmployeeType()
        {
            User = new HashSet<User>();
        }

        public int Emptypeid { get; set; }
        public string Employeetypeid { get; set; }
        public string Employeetypename { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}