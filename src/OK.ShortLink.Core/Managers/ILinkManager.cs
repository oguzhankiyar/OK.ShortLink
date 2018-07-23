using OK.ShortLink.Common.Models;
using System.Collections.Generic;

namespace OK.ShortLink.Core.Managers
{
    public interface ILinkManager
    {
        List<LinkModel> GetLinks(int pageSize = 15, int pageNumber = 1);
        
        LinkModel GetLinkById(int id);

        LinkModel GetLinkByShortUrl(string shortUrl);

        LinkModel CreateLink(int userId, string name, string description, string shortUrl, string originalUrl, bool isActive);

        bool EditLink(int userId, int id, string name, string description, string shortUrl, string originalUrl, bool isActive);

        bool DeleteLink(int userId, int id);
    }
}