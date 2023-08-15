namespace RegistrationTask.Core.Application.SendNotificationRealTime
{
    public class SignalRMessage
    {
        public string Type { get; set; }
        public string Information { get; set; }
        public int UserId { get; internal set; }
    }
}