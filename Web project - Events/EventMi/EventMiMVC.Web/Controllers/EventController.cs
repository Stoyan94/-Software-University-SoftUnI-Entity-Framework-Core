using System.Globalization;
using EventMi.Web.Services.Data.Contracts;
using EventMi.Web.ViewModels.Event;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.Intrinsics.X86;
using System.Runtime.Serialization;

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

            bool isStartDateValid = DateTime.TryParse(model.StartDate, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime startDate);


            if (!isStartDateValid)
            {
                ModelState.AddModelError(nameof(model.StartDate), "Invalid Start Date Format");
                return View(model);
            }

            bool isEndDateValid = DateTime.TryParse(model.EndDate, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime endDate);
            if (!isEndDateValid)
            {
                ModelState.AddModelError(nameof(model.EndDate), "Invalid End Date Format");
                return View(model);
            }


            // This will display the data that the user entered in the form
            await eventService.AddEvenet(model, startDate, endDate ); // Call the AddEvenet method of the eventService
            

            return RedirectToAction("Index", "Home");
            // Redirect the user to the home page if the event was added successfully
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                //we check for error because the user may pass a non - existent id and we in service use First() and make sure it won't return null
                EditEventFormModel eventModel = await eventService.GetEventById(id.Value);

                return View(eventModel); 

            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id, EditEventFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool isStartDateValid = DateTime.TryParse(model.StartDate, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime startDate);


            if (!isStartDateValid)
            {
                ModelState.AddModelError(nameof(model.StartDate), "Invalid Start Date Format");
                return View(model);
            }

            bool isEndDateValid = DateTime.TryParse(model.EndDate, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime endDate);
            if (!isEndDateValid)
            {
                ModelState.AddModelError(nameof(model.EndDate), "Invalid End Date Format");
                return View(model);
            }

            try
            {
                await eventService.EditEventById(id, model, startDate, endDate);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
            }

        }
        
    }
}
