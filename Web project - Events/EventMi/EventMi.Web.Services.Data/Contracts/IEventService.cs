using EventMi.Web.ViewModels.Event;

namespace EventMi.Web.Services.Data.Contracts
{ 
    public interface IEventService
    {
        Task AddEvent(AddEventFormModel eventFormModel, DateTime startDate, DateTime endDate);

        Task<EditEventFormModel> GetEventById(int id);

        Task EditEventById(int id, EditEventFormModel eventFormModel, DateTime startDate, DateTime endDate);

        Task DeleteEventById(int id);

        Task<IEnumerable<AllEventsForm>> GetAllEvents();
    }
}
