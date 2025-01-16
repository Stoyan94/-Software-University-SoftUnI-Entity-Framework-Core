using System.Globalization;
using EventMi.Web.Services.Data.Contracts;
using EventMi.Web.ViewModels.Event;
using EventMiMVC.Web.Data;
using EventMiMVC.Web.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EventMi.Web.Services.Data
{
    public class EventService : IEventService
    {
        private readonly EventMiDbContext dbContext;

        public EventService(EventMiDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task AddEvenet(AddEventFormModel eventFormModel, DateTime startDate, DateTime endDate)
        {
            Event newEvent = new Event
            {
                Name = eventFormModel.Name,
                StartDate = startDate,
                EndDate = endDate,
                Place = eventFormModel.Place,
                IsActive = true
            };

            await dbContext.Events.AddAsync(newEvent);
            await dbContext.SaveChangesAsync();
        }

        public async Task<EditEventFormModel> GetEventById(int id)
        {
            Event? eventDb = await dbContext.Events
                .FirstOrDefaultAsync(e => e.Id == id);

            if (eventDb == null)
            {
                throw new InvalidOperationException("Event not found");
            }

            if (!eventDb.IsActive!.Value)
            {
                throw new InvalidOperationException();
            }

            EditEventFormModel eventForm = new EditEventFormModel
            {
                Name = eventDb.Name,
                StartDate = eventDb.StartDate.ToString("G"),
                EndDate = eventDb.EndDate.ToString("G"),
                Place = eventDb.Place
            };

           

            
            return eventForm;
        }

        public async Task EditEventById(int id, EditEventFormModel eventFormModel, DateTime startDate, DateTime endDate)
        {
            Event eventToEdit = await dbContext.Events
            .FirstAsync(e => e.Id == id);

            if (!eventToEdit.IsActive!.Value)
            {
                throw new InvalidOperationException();
            }

            eventToEdit.Name = eventFormModel.Name;
            eventToEdit.StartDate = startDate;
            eventToEdit.EndDate = endDate;
            eventToEdit.Place = eventFormModel.Place;

           await dbContext.SaveChangesAsync();
        }
    }
}
