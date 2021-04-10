using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEvent", menuName = "Game Event", order = 52)]
public class Event : ScriptableObject
{
    private List<EventListener> elisteners = new List<EventListener>();

    public void Register(EventListener listener)
    {
        elisteners.Add(listener);
    }

    public void Unregister(EventListener listener)
    {
        elisteners.Remove(listener);
    }

    public void Occured(GameObject go)
    {
        for (int i = 0; i < elisteners.Count; i++)
        {
            elisteners[i].OnEventOccurs(go);
        }
    }
}
