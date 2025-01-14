using EventMi.Web.ViewModels.Event;
using Microsoft.AspNetCore.Mvc;

namespace EventMiMVC.Web.Controllers
{
    public class EventController : Controller
    {
        // This method will be called when the user navigates to the /event/add URL
        // It will return the Add view to the user
        // The Add view will contain a form that the user can fill out to add a new event
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // This method will be called when the user submits the form
        // It will receive the data from the form in the AddEventFormModel model
        [HttpPost]
        public IActionResult Add(AddEventFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Return the view with the model if the model state is not valid
                                    // This will display the validation errors to the user
            }

            return View(model); // Return the view with the model if the model state is valid
                                // This will display the data that the user entered in the form
        }

    }
}
