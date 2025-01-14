using EventMi.Web.ViewModels.Event;

namespace EventMi.Web.Services.Data.Contracts
{ 
    public interface IEventService
    {
        Task<bool> AddEvenet(AddEventFormModel eventFormModel);
    }
}
