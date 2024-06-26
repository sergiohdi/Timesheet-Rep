﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Timesheet.Api.Models
{
    public partial class Approvals
    {
        public int ApprovalId { get; set; }
        public int UserId { get; set; }
        public int ApprovalStatusId { get; set; }
        public int? TimeOffId { get; set; }
        public int ApprovalType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? Duration { get; set; }
        public string Comments { get; set; }
        public DateTime? Period { get; set; }
        public int? TimesheetControlId { get; set; }

        public virtual ApprovalStatus ApprovalStatus { get; set; }
        public virtual TimeOff TimeOff { get; set; }
        public virtual User User { get; set; }
    }
}