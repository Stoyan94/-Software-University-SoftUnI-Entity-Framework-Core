using System.Globalization;
using EventMi.Web.Services.Data.Contracts;
using EventMi.Web.ViewModels.Event;
using EventMiMVC.Web.Data;
using EventMiMVC.Web.Data.Models;

namespace EventMi.Web.Services.Data
{
    public class EventService : IEventService
    {
        private readonly EventMiDbContext dbContext;

        public EventService(EventMiDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<bool> AddEvenet(AddEventFormModel eventFormModel)
        {
            bool isStartDateValid = DateTime.TryParse(eventFormModel.StartDate, CultureInfo.InvariantCulture, 
                DateTimeStyles.None, out DateTime startDate);

            bool isEndDateValid = DateTime.TryParse(eventFormModel.EndDate, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime endDate);

            if (!isStartDateValid || !isEndDateValid)
            {
                return false;
            }

            Event newEvent = new Event
            {
                Name = eventFormModel.Name,
                StartDate = startDate,
                EndDate = endDate,
                Place = eventFormModel.Place
            };

            await dbContext.Events.AddAsync(newEvent);
            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
