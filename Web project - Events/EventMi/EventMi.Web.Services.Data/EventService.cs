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
        public async Task AddEvent(AddEventFormModel eventFormModel, DateTime startDate, DateTime endDate)
        {

            Event newEvent = new Event
            {
                Name = eventFormModel.Name,
                StartDate = startDate,
                EndDate = endDate,
                Place = eventFormModel.Place,
                Description = eventFormModel.Description,
                IsActive = true
            };

            await dbContext.Events.AddAsync(newEvent);
            await dbContext.SaveChangesAsync();
        }

        public async Task<EditEventFormModel> GetEventById(int id)
        {
            // Find the event by id
            // If the event is not found, throw an exception
            // If the event is not active, throw an exception
            // Create a new EditEventFormModel object and set its properties to the values of the event
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
                StartDate = eventDb.StartDate.ToString("G", CultureInfo.InvariantCulture),
                EndDate = eventDb.EndDate.ToString("G", CultureInfo.InvariantCulture),
                Place = eventDb.Place,
                Description = eventDb.Description
            };
            
            return eventForm;
        }

        public async Task EditEventById(int id, EditEventFormModel eventFormModel, DateTime startDate, DateTime endDate)
        {
            // Find the event by id
            // If the event is not found, throw an exception
            // Update the event's properties with the values from the form model
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
            eventToEdit.Description = eventFormModel.Description;

            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteEventById(int id)
        {
            Event? eventToDelete = await dbContext.Events
                .FirstOrDefaultAsync(e => e.Id == id);

            if (eventToDelete is null)
            {
                throw new InvalidOperationException("Event not found");
            }

            if (!eventToDelete!.IsActive!.Value)
            {
                throw new InvalidOperationException();
            }

            dbContext.Remove(eventToDelete);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllEventsForm>> GetAllEvents()
        {
            return await dbContext.Events
                .Where(e => e.IsActive == true)
                .OrderBy(e=> e.StartDate)
                .Select(e => new AllEventsForm
                {
                    Id = e.Id,
                    Name = e.Name,
                    StartDate = e.StartDate.ToString("G", CultureInfo.InvariantCulture),
                    EndDate = e.EndDate.ToString("G", CultureInfo.InvariantCulture),
                    Place = e.Place,
                    Description = e.Description
                })
                .ToListAsync();
        }
    }
}
