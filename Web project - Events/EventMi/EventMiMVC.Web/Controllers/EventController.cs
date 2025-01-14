using EventMi.Web.Services.Data.Contracts;
using EventMi.Web.ViewModels.Event;
using Microsoft.AspNetCore.Mvc;

namespace EventMiMVC.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(IEventService _eventService)
        {

            eventService = _eventService;
        }

        // This method will be called when the user navigates to the /event/add URL
        // It will return the Add view to the user
        // The Add view will contain a form that the user can fill out to add a new event
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        // This method will be called when the user submits the form
        // It will receive the data from the form in the AddEventFormModel model
        [HttpPost]
        public async Task<IActionResult> Add(AddEventFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Return the view with the model if the model state is not valid
                                    // This will display the validation errors to the user
            }

            bool addSuccess = await eventService.AddEvenet(model); // Call the AddEvenet method of the eventService
            
            if (!addSuccess)
            {
                ModelState.AddModelError(nameof(model.StartDate), "Start and or End date is not in correct format!");
                return View(model); // Return the view with the model if the event could not be added
                                    // This will display an error message to the user
            }

            DateTime startDate = DateTime.Parse(model.StartDate);
            DateTime endDate = DateTime.Parse(model.EndDate);

            if (startDate >= endDate)
            {
                ModelState.AddModelError(nameof(model.StartDate), "The start date must be earlier than the end date!");
                return View(model);
            }
            else
            {
                ModelState.AddModelError(nameof(model.EndDate), "The end date must be later than the start date!");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
            // Redirect the user to the home page if the event was added successfully
        }

    }
}
