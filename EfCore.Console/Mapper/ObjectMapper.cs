using AutoMapper;
using EfCore.Console.Dal;
using EfCore.Console.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Console.Mapper
{
    internal class ObjectMapper
    {
        static private readonly Lazy<IMapper> _mapper = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductProfile>();
            });

            return config.CreateMapper();

        });

        public static IMapper Lazy => _mapper.Value;
    }

    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
