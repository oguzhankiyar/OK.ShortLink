using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OK.ShortLink.Api.Requests;
using OK.ShortLink.Common.Models;
using OK.ShortLink.Core.Managers;
using System.Collections.Generic;

namespace OK.ShortLink.Api.Controllers
{
    [Route("api/[controller]")]
    public class VisitorsController : BaseController
    {
        private readonly IVisitorManager _visitorManager;

        public VisitorsController(IVisitorManager visitorManager)
        {
            _visitorManager = visitorManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get(int page = 1, int? linkId = null)
        {
            List<VisitorModel> visitors;

            if (linkId.HasValue)
            {
                visitors = _visitorManager.GetVisitorsByLink(linkId.Value, 15, page);
            }
            else
            {
                visitors = _visitorManager.GetVisitors(15, page);
            }

            return Ok(visitors);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            VisitorModel visitor = _visitorManager.GetVisitorById(id);

            if (visitor == null)
            {
                return NotFound();
            }

            return Ok(visitor);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create([FromBody] CreateVisitorRequest request)
        {
            VisitorModel visitor = _visitorManager.CreateVisitor(request.LinkId,
                                                                 request.IPAddress,
                                                                 request.UserAgent,
                                                                 request.OSInfo,
                                                                 request.DeviceInfo,
                                                                 request.BrowserInfo);

            if (visitor == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetById), new { id = visitor.Id }, null);
        }
    }
}