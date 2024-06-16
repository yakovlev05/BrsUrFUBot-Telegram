using BrsTgBot.Services.Interfaces;

namespace BrsTgBot.Services.Factory;

public interface IUpdateHandlerFactory
{
    public T Create<T>() where T : IUpdateHandler;
}