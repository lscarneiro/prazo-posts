using System;
using AutoMapper;
using PrazoPosts.Service.Core;

namespace PrazoPosts.Service.Tests
{
    public static class TestHelper
    {
        public static IMapper GetMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            return mapperConfig.CreateMapper();
        }
    }
}
