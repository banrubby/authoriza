using System;
using Authorizeniki.Datalayer.Repositories;
using Authorizeniki.Datalayer.Tables;
using Microsoft.AspNetCore.Mvc;

namespace Authorizeniki.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventRepository eventRepository;

        public EventController(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        [HttpPut]
        public IActionResult AddEvent([FromBody] Event eEvent)
        {
            eventRepository.Add(eEvent);
            return Ok();
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Guid userId, [FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            return Ok(eventRepository.GetEventsByUserId(userId, from, to));
        }

        [HttpGet]
        public IActionResult CountSalary([FromQuery] Guid userId)
        {
            DateTime date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var events = eventRepository.GetEventsByUserId(userId, firstDayOfMonth, lastDayOfMonth);

            if (events.Count == 0)
                throw new Exception($"А чего считать то? юзер {userId} на работе не появлялся в этом месяце!");
            if (events.Count % 2 != 0)
                throw new Exception($"Видимо юзер {userId} еще не ушел с работы, не могу посчитать");

            var salary = 0.0m;
            var hoursInMonth = 160;
            var salaryForHour = events[0]!.User!.UserRole!.Salary / hoursInMonth;
            for (int i = 0; i < events.Count - 1; i += 2)
            {
                var timediff = events[i + 1].EventTime - events[i].EventTime;
                salary += (salaryForHour * timediff.Hours);
            }

            return Ok(salary);
        }
    }
}