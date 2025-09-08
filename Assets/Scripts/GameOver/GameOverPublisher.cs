using System.Collections.Generic;

public class GameOverPublisher
{
    private static GameOverPublisher _instance;

    private readonly HashSet<IGameOverSubscriber> _subscribers = new();
    
    // Private constructor to prevent instantiation
    private GameOverPublisher(){}
    
    public static GameOverPublisher GetInstance()
    {
        if (_instance is null)
        {
            _instance = new GameOverPublisher();
        }
        
        return _instance;
    }
    
    public void Subscribe(IGameOverSubscriber subscriber)
    {
        if (subscriber == null) return;
        _subscribers.Add(subscriber);
    }
    
    public void GameOver()
    {
        Notify();
    }

    private void Notify()
    {
        foreach (var subscriber in _subscribers)
        {
            subscriber.OnGameOver();
        }
    }
}