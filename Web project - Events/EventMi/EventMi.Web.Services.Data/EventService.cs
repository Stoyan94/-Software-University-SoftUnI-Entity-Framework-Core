using System.Globalization;
using EventMi.Web.Services.Data.Contracts;
using EventMi.Web.ViewModels.Event;
using EventMiMVC.Web.Data;
using EventMiMVC.Web.Data.Models;
using Microsoft.EntityFrameworkCore;

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
                Place = eventFormModel.Place
            };

            await dbContext.Events.AddAsync(newEvent);
            await dbContext.SaveChangesAsync();
        }

        public async Task<EditEventFormModel> GetEventById(int id)
        {
            EditEventFormModel eventForm = await dbContext.Events
                .Where(e => e.Id == id)
                .Select(e => new EditEventFormModel()
                {
                    Name = e.Name,
                    StartDate = e.StartDate.ToString("G"),
                    EndDate = e.EndDate.ToString("G"),
                    Place = e.Place
                }).FirstAsync();

            return eventForm;
        }

        public async Task EditEventById(int id, EditEventFormModel eventFormModel, DateTime startDate, DateTime endDate)
        {
            Event eventToEdit = await dbContext.Events
                .FirstAsync(e => e.Id == id);

            eventToEdit.Name = eventFormModel.Name;
            eventToEdit.StartDate = startDate;
            eventToEdit.EndDate = endDate;
            eventToEdit.Place = eventFormModel.Place;

           await dbContext.SaveChangesAsync();
        }
    }
}
