using System.Collections.Generic;
using Tcc.Api.Hypermedia.Abstract;

namespace Tcc.Api.Hypermedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
