﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Timesheet.Api.Models
{
    public partial class ApprovalStatus
    {
        public ApprovalStatus()
        {
            Approvals = new HashSet<Approvals>();
            TimesheetControl = new HashSet<TimesheetControl>();
        }

        public int Approvalstatusid { get; set; }
        public string Appstatusname { get; set; }

        public virtual ICollection<Approvals> Approvals { get; set; }
        public virtual ICollection<TimesheetControl> TimesheetControl { get; set; }
    }
}