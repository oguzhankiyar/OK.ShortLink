using System.Collections.Generic;

namespace OK.ShortLink.Core.Mapping
{
    public interface IMapper
    {
        TTo Map<TFrom, TTo>(TFrom from);

        List<TTo> MapList<TFrom, TTo>(List<TFrom> from);
    }
}