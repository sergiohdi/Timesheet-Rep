using System;
using Timesheet.Client.Models;
using Timesheet.Client.Services.Interfaces;

namespace Timesheet.Client.Services.Implementations;

public class ComponentComunicationService : IComponentComunicationService
{
    public event Action<Notification> Subscriber;
    public event Action<bool> ChangeWidthSubscriber;
    public event Action OpenTimeOffPopUp;
    public event Action RefreshTimesheet;

    public void SendChangeWidth(bool value)
    {
        ChangeWidthSubscriber?.Invoke(value);
    }

    public void SendNotification(Notification notification)
    {
        Subscriber?.Invoke(notification);
    }

    public void SendOpenPopUpAction()
    {
        OpenTimeOffPopUp.Invoke();
    }

    public void SendRefreshTimesheetAction() 
    {
        RefreshTimesheet.Invoke();
    }
}
