namespace Core.Application.Services.HangfireJobs
{
    public interface IHangFireJobs
    {
        void DeleteNotApprovedRejectedRequests();
    }
}
