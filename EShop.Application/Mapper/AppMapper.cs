using AutoMapper;

namespace EShop.Application.Mapper;

public class AppMapper
{
    private static Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapProperty = o => o.GetMethod!.IsPublic || o.GetMethod.IsAssembly;
            cfg.AddProfile<AppMapperProfile>();
        });

        var mapper = config.CreateMapper();
        return mapper;
    });

    public static IMapper Mapper = Lazy.Value;
}
