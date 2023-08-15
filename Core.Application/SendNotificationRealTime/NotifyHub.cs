using Microsoft.AspNetCore.SignalR;

namespace RegistrationTask.Core.Application.SendNotificationRealTime
{
    public class NotifyHub  :Hub<ITypedHubClient>
    {
    }
}
