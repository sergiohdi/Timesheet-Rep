﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Timesheet.Api.Models
{
    public partial class Department
    {
        public Department()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Comments { get; set; }
        public bool? DisabledSetting { get; set; }
        public string CostCenterGroup { get; set; }
        public int? ParentId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedIp { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedIp { get; set; }
        public string CreatedMacAddress { get; set; }
        public string UpdatedMacAddress { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}