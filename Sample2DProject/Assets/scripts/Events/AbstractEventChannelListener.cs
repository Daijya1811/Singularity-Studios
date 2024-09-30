using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractEventChannelListener<TEventChannel, TEventType> : 
    MonoBehaviour where TEventChannel : GenericEventChannelSO<TEventType>
{
    [Header("Event Channel to Listen to")] 
    [SerializeField] protected TEventChannel EventChannel;

    [Header("Response to recieving signal from Event Channel")] 
    [SerializeField] protected UnityEvent<TEventType> Response;

    protected virtual void OnEnable()
    {
        if (EventChannel != null) EventChannel.OnEventRaised += OnEventRaised;
    }
    
    protected virtual void OnDisable()
    {
        if (EventChannel != null) EventChannel.OnEventRaised -= OnEventRaised;
    }

    public void OnEventRaised(TEventType evt)
    {
        Response?.Invoke(evt);
    }
}
