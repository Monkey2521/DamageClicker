using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus
{
    static Dictionary<Type, List<ISubscriber>> _subscribers;

    public static void Subscribe(ISubscriber subscriber)
    {

    }

    static List<Type> GetSubscriberInterfaces()
    {
        List<Type> types = new List<Type>();

        return types;
    }

    static void Invoke<TSubscriber>(Action<TSubscriber> action) where TSubscriber : ISubscriber
    {

    }
}
