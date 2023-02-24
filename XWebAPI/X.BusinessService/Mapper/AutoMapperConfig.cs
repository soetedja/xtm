using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace X.BusinessService.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }
    }
}
