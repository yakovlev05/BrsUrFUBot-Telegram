using BrsTgBot.Services.Interfaces;

namespace BrsTgBot.Services.Factory;

public class UpdateHandlerFactory : IUpdateHandlerFactory
{
    private readonly IEnumerable<IUpdateHandler> _serviceCollection;

    public UpdateHandlerFactory(IEnumerable<IUpdateHandler> serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }

    public T Create<T>() where T : IUpdateHandler
    {
        var handler = (T)_serviceCollection.FirstOrDefault(x => x.GetType() == typeof(T));
        if (handler is null)
            throw new InvalidOperationException(
                $"Handler of type {typeof(T).Name} is not registered in the DI.");
        return handler;
    }
}