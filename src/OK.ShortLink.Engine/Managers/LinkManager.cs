using OK.ShortLink.Common.Entities;
using OK.ShortLink.Common.Models;
using OK.ShortLink.Core.Logging;
using OK.ShortLink.Core.Managers;
using OK.ShortLink.Core.Mapping;
using OK.ShortLink.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OK.ShortLink.Engine.Managers
{
    public class LinkManager : ILinkManager
    {
        private readonly ILinkRepository _linkRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public LinkManager(ILinkRepository linkRepository, ILogger logger, IMapper mapper)
        {
            _linkRepository = linkRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public List<LinkModel> GetLinks(int pageSize = 15, int pageNumber = 1)
        {
            int skip = pageSize * (pageNumber - 1);
            int take = pageSize;

            List<LinkEntity> links = _linkRepository.FindAll()
                                                    .Skip(skip)
                                                    .Take(take)
                                                    .ToList();

            return _mapper.MapList<LinkEntity, LinkModel>(links);
        }

        public LinkModel GetLinkById(int id)
        {
            LinkEntity link = _linkRepository.FindOne(x => x.Id == id);

            return _mapper.Map<LinkEntity, LinkModel>(link);
        }

        public LinkModel GetLinkByShortUrl(string shortUrl)
        {
            LinkEntity link = _linkRepository.FindOne(x => x.ShortUrl == shortUrl && x.IsActive == true);

            return _mapper.Map<LinkEntity, LinkModel>(link);
        }

        public LinkModel CreateLink(int userId, string name, string description, string shortUrl, string originalUrl, bool isActive)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name));
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException(nameof(description));
            }

            if (string.IsNullOrEmpty(shortUrl))
            {
                throw new ArgumentException(nameof(shortUrl));
            }

            if (string.IsNullOrEmpty(originalUrl))
            {
                throw new ArgumentException(nameof(originalUrl));
            }

            LinkEntity link = new LinkEntity();

            link.UserId = userId;
            link.Name = name;
            link.Description = description;
            link.ShortUrl = shortUrl;
            link.OriginalUrl = originalUrl;
            link.IsActive = isActive;

            link = _linkRepository.Insert(link);

            if (link.Id > 0)
            {
                _logger.DebugData(string.Join("/", nameof(LinkManager), nameof(CreateLink)), "A link is created.", new { CreatedId = link.Id, CreatedBy = userId });
            }

            return _mapper.Map<LinkEntity, LinkModel>(link);
        }

        public bool EditLink(int userId, int id, string name, string description, string shortUrl, string originalUrl, bool isActive)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name));
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException(nameof(description));
            }

            if (string.IsNullOrEmpty(shortUrl))
            {
                throw new ArgumentException(nameof(shortUrl));
            }

            if (string.IsNullOrEmpty(originalUrl))
            {
                throw new ArgumentException(nameof(originalUrl));
            }

            LinkEntity link = _linkRepository.FindOne(x => x.Id == id);

            if (link == null)
            {
                return false;
            }

            link.Name = name;
            link.Description = description;
            link.ShortUrl = shortUrl;
            link.OriginalUrl = originalUrl;
            link.IsActive = isActive;

            bool isEdited = _linkRepository.Update(link);

            if (isEdited)
            {
                _logger.DebugData(string.Join("/", nameof(LinkManager), nameof(EditLink)), "A link is edited.", new { EditedId = id, EditedBy = userId });
            }

            return isEdited;
        }

        public bool DeleteLink(int userId, int id)
        {
            LinkEntity link = _linkRepository.FindOne(x => x.Id == id);

            if (link == null)
            {
                return false;
            }

            bool isDeleted = _linkRepository.Remove(id);

            if (isDeleted)
            {
                _logger.DebugData(string.Join("/", nameof(LinkManager), nameof(DeleteLink)), "A link is deleted.", new { DeletedId = id, DeletedBy = userId });
            }

            return isDeleted;
        }
    }
}