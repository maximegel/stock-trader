using System;
using AutoMapper;
using SimpleCqrs.Portfolios.Domain;

namespace SimpleCqrs.Portfolios.Persistence.DataModels
{
    internal class PortfolioDataMappings : Profile
    {
        public PortfolioDataMappings()
        {
            CreateMap<Portfolio, PortfolioData>().ReverseMap();
            
            CreateMap<PortfolioId, Guid>().ConvertUsing(id => Guid.Parse(id.ToString()));
            CreateMap<Guid, PortfolioId>().ConvertUsing(guid => PortfolioId.Parse(guid.ToString()));
        }
    }
}