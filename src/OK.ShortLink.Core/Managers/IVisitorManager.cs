using OK.ShortLink.Common.Models;
using System.Collections.Generic;

namespace OK.ShortLink.Core.Managers
{
    public interface IVisitorManager
    {
        List<VisitorModel> GetVisitors(int pageSize = 15, int pageNumber = 1);

        List<VisitorModel> GetVisitorsByLink(int linkId, int pageSize = 15, int pageNumber = 1);

        VisitorModel GetVisitorById(int id);

        VisitorModel CreateVisitor(int linkId, string ipAddress, string userAgent, string osInfo, string deviceInfo, string browserInfo);
    }
}