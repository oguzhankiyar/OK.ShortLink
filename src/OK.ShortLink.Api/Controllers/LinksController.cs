using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OK.ShortLink.Api.Requests;
using OK.ShortLink.Common.Models;
using OK.ShortLink.Core.Managers;
using System.Collections.Generic;

namespace OK.ShortLink.Api.Controllers
{
    [Route("api/[controller]")]
    public class LinksController : BaseController
    {
        private readonly ILinkManager _linkManager;

        public LinksController(ILinkManager linkManager)
        {
            _linkManager = linkManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get(int page = 1)
        {
            List<LinkModel> links = _linkManager.GetLinks(15, page);

            return Ok(links);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            LinkModel link = _linkManager.GetLinkById(id);

            if (link == null)
            {
                return NotFound();
            }

            return Ok(link);
        }

        [HttpGet]
        public IActionResult GetByShortUrl([FromQuery] string shortUrl)
        {
            LinkModel link = _linkManager.GetLinkByShortUrl(shortUrl);

            if (link == null)
            {
                return NotFound();
            }

            return Ok(link);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] CreateLinkRequest request)
        {
            LinkModel link = _linkManager.CreateLink(CurrentUserId.Value,
                                                     request.Name,
                                                     request.Description,
                                                     request.ShortUrl,
                                                     request.OriginalUrl,
                                                     request.IsActive);

            if (link == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetById), new { id = link.Id }, null);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Edit([FromRoute] int id, [FromBody] EditLinkRequest request)
        {
            bool isEdited = _linkManager.EditLink(CurrentUserId.Value,
                                                  id,
                                                  request.Name,
                                                  request.Description,
                                                  request.ShortUrl,
                                                  request.OriginalUrl,
                                                  request.IsActive);

            if (!isEdited)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            bool isDeleted = _linkManager.DeleteLink(CurrentUserId.Value, id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}