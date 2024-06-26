﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Models;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;
using Timesheet.Shared.Utils;

namespace Timesheet.Api.Repositories.EF_Implementations;

public class TimesheetControlRepository : ITimesheetControlRepository
{
    private readonly TimesheetContext _db;
    private readonly IMapper _mapper;
    public TimesheetControlRepository(TimesheetContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public IEnumerable<TimesheetControlDto> GetTimesheetControl()
    {
        IQueryable<TimesheetControl> result = _db.TimesheetControl.AsNoTracking();
        return _mapper.Map<IEnumerable<TimesheetControlDto>>(result.ToList());
    }

    public bool UpdateApprovalStatus(int[] ids)
    {
        _db.TimesheetControl.Where(x => ids.Contains(x.TimesheetPeriodId))
            .ExecuteUpdate(s => 
                s.SetProperty(x => x.ApprovalStatusId, (int)ApprovalStatusOption.Approved));

        return true;
    }

    public IEnumerable<TimesheetControlDto> GetTimesheetControlById(int[] ids)
    {
        IEnumerable<TimesheetControl> result = _db.TimesheetControl.AsNoTracking()
                                                                   .Where(x => ids.Contains(x.TimesheetPeriodId))
                                                                   .ToList();
        return _mapper.Map<IEnumerable<TimesheetControlDto>>(result);
    }

    public TimesheetControlDto GetTimesheetControlByPeriodAndUserId(DateTime period, int userId)
    {
        TimesheetControl record = _db.TimesheetControl.AsNoTracking().FirstOrDefault(x =>
                                                                            x.TimesheetPeriod == period.Date && 
                                                                            x.UserId == userId);

        return _mapper.Map<TimesheetControlDto>(record);
    }

    public TimesheetControlDto CreateTimesheetControlRecord(TimesheetControlDto record)
    {
        TimesheetControl timesheetControl = _mapper.Map<TimesheetControl>(record);
        _db.TimesheetControl.Add(timesheetControl);

        return _db.SaveChanges() > 0
            ? _mapper.Map<TimesheetControlDto>(timesheetControl)
            : null;
    }

    public IEnumerable<TimesheetControlApprovalDto> GetTimesheetControlRequests(DateTime startDate, DateTime endDate, int supervisorId = 0)
    {
        IEnumerable<TimesheetControlApprovalDto> timesheetControlRequests = (from a in _db.TimesheetControl
                                                                            join u in _db.User on a.UserId equals u.Id
                                                                            join asi in _db.ApprovalStatus on a.ApprovalStatusId equals asi.Approvalstatusid
                                                                             where (a.StartDate.Date >= startDate.Date && a.StartDate <= endDate.Date) ||
                                                                                 (a.EndDate.Date >= startDate.Date && a.EndDate.Date <= endDate.Date)
                                                                             select new TimesheetControlApprovalDto
                                                                            {
                                                                                TimesheetPeriodId = a.TimesheetPeriodId,
                                                                                TimesheetPeriod = a.TimesheetPeriod,
                                                                                UserId = a.UserId,
                                                                                UserName = $"{u.LastName}, {u.FirstName} ({u.LoginName})",
                                                                                StartDate = a.StartDate,
                                                                                EndDate = a.EndDate,
                                                                                ApprovalStatusId = a.ApprovalStatusId,
                                                                                StatusName = asi.Appstatusname
                                                                            }).ToList();
                                                                                
        return timesheetControlRequests;
    }

    public bool ProcessTimesheetsRequests(int[] ids, int status)
    {
        _db.TimesheetControl.Where(x => ids.Contains(x.TimesheetPeriodId))
                     .ExecuteUpdate(s => s.SetProperty(x => x.ApprovalStatusId, status));

        return true;
    }

    public IEnumerable<TimesheetControlApprovalDto> GetTimesheetsApprovalRecords(int[] ids)
    {
        IEnumerable<TimesheetControl> timeTimesheetsList = _db.TimesheetControl.AsNoTracking().Where(x => ids.Contains(x.TimesheetPeriodId)).ToList();
        return _mapper.Map<IEnumerable<TimesheetControlApprovalDto>>(timeTimesheetsList);
    }

    public bool UpdateTimesheetContolRecords(int timesheetControlId, int status)
    {
        _db.TimesheetControl.Where(
            x => x.TimesheetPeriodId == timesheetControlId)
            .ExecuteUpdate(s => s.SetProperty(x => x.ApprovalStatusId, status));

        return true;
    }

    public TimesheetControlDto GetTimesheetControlRecord(DateTime period, int userId)
    {
        TimesheetControl result = _db.TimesheetControl.AsNoTracking()
                                                      .FirstOrDefault(x => x.TimesheetPeriod.Date == period.Date && x.UserId == userId);
        return _mapper.Map<TimesheetControlDto>(result);
    }

    public List<TimesheetControlDto> GetFutureTimesheetControlRecords(DateTime currentPeriod, int userId)
    {
        List<TimesheetControl> result = _db.TimesheetControl
            .AsNoTracking()
            .Where(x => x.UserId == userId && x.TimesheetPeriod > currentPeriod.Date)
            .ToList();
        return _mapper.Map<List<TimesheetControlDto>>(result);
    }

    public bool UpdateUserTemplate(int[] ids, int newUserTemplate)
    {
        _db.TimesheetControl.Where(x => ids.Contains(x.TimesheetPeriodId))
            .ExecuteUpdate(s =>
                s.SetProperty(x => x.UserTemplateId, newUserTemplate));

        return true;
    }
}
