using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Example Button Event Channel", menuName = "Events/Example Button Event Channel")]
public class ExampleButtonEventChannelSO : GenericEventChannelSO<object>
{
    /// <summary>
    /// Rather than make a separate system for void events, just make the event take an object
    /// and pass through null
    /// </summary>
    public void RaiseEvent()
    {
        base.RaiseEvent(null);
    }
}
