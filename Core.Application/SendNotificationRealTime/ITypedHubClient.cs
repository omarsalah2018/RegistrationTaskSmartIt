using Microsoft.AspNet.SignalR.Messaging;

namespace RegistrationTask.Core.Application.SendNotificationRealTime
{
    public interface ITypedHubClient
    {
        Task BroadCastMessage(SignalRMessage message);

    }
}
