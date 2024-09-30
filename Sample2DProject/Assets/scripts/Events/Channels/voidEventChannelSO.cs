using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Void Event Channel", menuName = "Events/Void Event Channel")]
public class VoidEventChannelSO : GenericEventChannelSO<object>
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
