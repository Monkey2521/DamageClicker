using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus
{
    static Dictionary<Type, List<ISubscriber>> _subscribers = new Dictionary<Type, List<ISubscriber>>();

    public static void Subscribe(ISubscriber subscriber)
    {
        List<Type> interfaces = GetSubscriberInterfaces(subscriber.GetType());

        foreach(Type type in interfaces)
        {
            if (!_subscribers.ContainsKey(type))
                _subscribers[type] = new List<ISubscriber>();

            _subscribers[type].Add(subscriber);
        }

        /*foreach(Type type in _subscribers.Keys)
        {
            Debug.Log(type + ":");
            foreach (ISubscriber sub in _subscribers[type])
                Debug.Log("-> " + sub.GetType());
        }*/
    }

    static List<Type> GetSubscriberInterfaces(Type subscriberType)
    {
        List<Type> interfaces = new List<Type>();

        foreach (Type type in subscriberType.GetInterfaces())
        {
            if (typeof(ISubscriber).IsAssignableFrom(type) && type != typeof(ISubscriber))
                interfaces.Add(type);
        }

        //foreach (Type type in interfaces) Debug.Log(type);

        return interfaces;
    }

    static void Invoke<TSubscriber>(Action<TSubscriber> action) where TSubscriber : ISubscriber
    {

    }
}
