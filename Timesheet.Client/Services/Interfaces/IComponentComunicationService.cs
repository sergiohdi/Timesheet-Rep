using System;
using Timesheet.Client.Models;

namespace Timesheet.Client.Services.Interfaces
{
    public interface IComponentComunicationService
    {
        // Susbcribers
        event Action<Notification> Subscriber;
        event Action<bool> ChangeWidthSubscriber;

        // Methods
        void SendNotification(Notification notification);
        void SendChangeWidth(bool value);
    }
}
