using Mapster;
using MapsterMapper;

namespace CRM.Application.Services.Mappings;

public class CustomMapsterMapper : IMapper
{
    private readonly TypeAdapterConfig _config;

    public CustomMapsterMapper(TypeAdapterConfig config)
    {
        _config = config;
    }

    public TypeAdapterConfig Config => _config;

    public ITypeAdapterBuilder<TSource> From<TSource>(TSource source)
    {
        return source.BuildAdapter(_config);
    }

    public TDestination Map<TDestination>(object source)
    {
        return source.Adapt<TDestination>(_config);
    }

    public TDestination Map<TSource, TDestination>(TSource source)
    {
        return source.Adapt<TDestination>(_config);
    }

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        return source.Adapt(destination, _config);
    }

    public object Map(object source, Type sourceType, Type destinationType)
    {
        return source.Adapt(sourceType, destinationType, _config);
    }

    public object Map(object source, object destination, Type sourceType, Type destinationType)
    {
        return source.Adapt(destination, sourceType, destinationType, _config);
    }
}