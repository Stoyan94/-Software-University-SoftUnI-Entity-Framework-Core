﻿using EventMi.Web.Services.Data.Contracts;
using EventMi.Web.ViewModels.Event;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using EventMiMVC.Web.Data.Models;

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
            await eventService.AddEvent(model, startDate, endDate ); // Call the AddEvent method of the eventService
            

            return RedirectToAction("Index", "Home");
            // Redirect the user to the home page if the event was added successfully
        }

        
        [HttpGet] // This method will be called when the user navigates to the /event/edit/{id} URL
                  // It will return the Edit view to the user
                  // The Edit view will contain a form that the user can fill out to edit an existing event
        public async Task<IActionResult> Edit(int? id)
        {

            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                EditEventFormModel eventModel = await eventService.GetEventById(id.Value);

                return View(eventModel); 

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int? id, EditEventFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
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
                await eventService.EditEventById(id.Value, model, startDate, endDate);
                return RedirectToAction("All", "Event");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpGet]

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                EditEventFormModel eventModel = await eventService.GetEventById(id.Value);

                return View(eventModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]

        public async Task<IActionResult> Delete(int? id, EditEventFormModel model)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }


            try
            {
                await eventService.DeleteEventById(id.Value);
                return RedirectToAction("All", "Event");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var allEvents = await eventService.GetAllEvents();

            return View(allEvents);
        }
    }
}
