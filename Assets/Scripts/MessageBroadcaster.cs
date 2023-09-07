using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBroadcaster
{
    public static MessageBroadcaster Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MessageBroadcaster();
            }

            return instance;
        }
    }

    private static MessageBroadcaster instance;

    public delegate void OnMessageReceived();

    public void Subscribe(string eventName, OnMessageReceived delegateFunction)
    {
        if(messageQueue.ContainsKey(eventName))
        {
            messageQueue[eventName] += delegateFunction;
        }
        else
        {
            messageQueue.Add(eventName, delegateFunction);
        }
    }

    public bool Unsubscribe(string eventName, OnMessageReceived delegateFunction)
    {
        if (messageQueue.ContainsKey(eventName))
        {
            messageQueue[eventName] -= delegateFunction;

            if(messageQueue[eventName] == null || messageQueue[eventName].GetInvocationList().Length == 0)
            {
                messageQueue.Remove(eventName);
            }

            return true;
        }

        return false;
    }

    public bool BroadcastEvent(string eventName)
    {
        if (messageQueue.ContainsKey(eventName))
        {
            messageQueue[eventName]?.Invoke();
            return true;
        }

        return false;
    }

    protected Dictionary<string, OnMessageReceived> messageQueue = new Dictionary<string, OnMessageReceived>();
}
