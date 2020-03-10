using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Application.UnitTests.Common.Mappings
{
    public class MappingTestsFixture
    {
        public MappingTestsFixture()
        {
            ConfigurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Mapping.Mappings>();
            });

            Mapper = ConfigurationProvider.CreateMapper();
        }

        public IConfigurationProvider ConfigurationProvider { get; }

        public IMapper Mapper { get; }  
    }
}
