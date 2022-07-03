using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus
{
    private static Dictionary<Type, List<ISubscriber>> _subscribers = new Dictionary<Type, List<ISubscriber>>();

    public static void Subscribe(ISubscriber subscriber)
    {
        List<Type> interfaces = GetSubscriberInterfaces(subscriber.GetType());

        foreach(Type type in interfaces)
        {
            if (!_subscribers.ContainsKey(type))
                _subscribers[type] = new List<ISubscriber>();

            if (_subscribers[type].Contains(subscriber))
                continue;

            _subscribers[type].Add(subscriber);
        }
    }

    public static void Unsubscribe(ISubscriber subscriber)
    {
        List<Type> interfaces = GetSubscriberInterfaces(subscriber.GetType());

        foreach (Type type in interfaces)
        {
            if (!_subscribers.ContainsKey(type))
            {
                Debug.Log(subscriber.GetType() + " not subscriber of " + type);
                continue;
            }

            _subscribers[type].Remove(subscriber);
        }
    }

    private static List<Type> GetSubscriberInterfaces(Type subscriberType)
    {
        List<Type> interfaces = new List<Type>();

        foreach (Type type in subscriberType.GetInterfaces()) 
        {
            if (typeof(ISubscriber).IsAssignableFrom(type) && type != typeof(ISubscriber))
                interfaces.Add(type); // получение интерфейсов, наследуемых от ISubscriber
        }

        return interfaces;
    }

    public static void Publish<TSubscriber>(Action<TSubscriber> action) where TSubscriber : ISubscriber
    {
        Type type = typeof(TSubscriber);

        if (!_subscribers.ContainsKey(type))
        {
            Debug.Log("Missing key! " + type);
            return;
        }
        
        foreach (ISubscriber subscriber in _subscribers[type])
        {
            action.Invoke((TSubscriber)subscriber);
        }
    }
}
