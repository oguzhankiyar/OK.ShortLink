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
    public class VisitorManager : IVisitorManager
    {
        private readonly IVisitorRepository _visitorRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public VisitorManager(IVisitorRepository visitorRepository, ILogger logger, IMapper mapper)
        {
            _visitorRepository = visitorRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public List<VisitorModel> GetVisitors(int pageSize = 15, int pageNumber = 1)
        {
            int skip = pageSize * (pageNumber - 1);
            int take = pageSize;

            List<VisitorEntity> visitors = _visitorRepository.FindAll()
                                                             .Skip(skip)
                                                             .Take(take)
                                                             .ToList();

            return _mapper.MapList<VisitorEntity, VisitorModel>(visitors);
        }

        public List<VisitorModel> GetVisitorsByLink(int linkId, int pageSize = 15, int pageNumber = 1)
        {
            int skip = pageSize * (pageNumber - 1);
            int take = pageSize;

            List<VisitorEntity> visitors = _visitorRepository.FindMany(x => x.LinkId == linkId)
                                                             .Skip(skip)
                                                             .Take(take)
                                                             .ToList();

            return _mapper.MapList<VisitorEntity, VisitorModel>(visitors);
        }

        public VisitorModel GetVisitorById(int id)
        {
            VisitorEntity visitor = _visitorRepository.FindOne(x => x.Id == id);

            return _mapper.Map<VisitorEntity, VisitorModel>(visitor);
        }

        public VisitorModel CreateVisitor(int linkId, string ipAddress, string userAgent, string osInfo, string deviceInfo, string browserInfo)
        {
            if (string.IsNullOrEmpty(ipAddress))
            {
                throw new ArgumentException(nameof(ipAddress));
            }

            if (string.IsNullOrEmpty(userAgent))
            {
                throw new ArgumentException(nameof(userAgent));
            }

            if (string.IsNullOrEmpty(osInfo))
            {
                throw new ArgumentException(nameof(osInfo));
            }

            if (string.IsNullOrEmpty(deviceInfo))
            {
                throw new ArgumentException(nameof(deviceInfo));
            }

            if (string.IsNullOrEmpty(browserInfo))
            {
                throw new ArgumentException(nameof(browserInfo));
            }

            VisitorEntity visitor = new VisitorEntity();

            visitor.LinkId = linkId;
            visitor.IPAddress = ipAddress;
            visitor.UserAgent = userAgent;
            visitor.OSInfo = osInfo;
            visitor.DeviceInfo = deviceInfo;
            visitor.BrowserInfo = browserInfo;

            visitor = _visitorRepository.Insert(visitor);

            if (visitor.Id > 0)
            {
                _logger.DebugData(string.Join("/", nameof(VisitorManager), nameof(CreateVisitor)), "A visitor is created.", new { VisitorId = visitor.Id });
            }

            return _mapper.Map<VisitorEntity, VisitorModel>(visitor);
        }
    }
}