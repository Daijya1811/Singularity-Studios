using UnityEngine;
using UnityEngine.Events;

public abstract class GenericEventChannelSO<T> : ScriptableObject 
{
    [Tooltip("This is the action that happens when the event is raised.")]
    public UnityAction<T> OnEventRaised;

    /// <summary>
    /// Method that raises the event and passes any parameters associated
    /// </summary>
    /// <param name="parameters"></param>
    public void RaiseEvent(T parameters)
    {
        OnEventRaised?.Invoke(parameters);
    }
    
}
