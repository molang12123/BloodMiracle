using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventChoice
{
    public string description;
    public int hpChange;
    public int goldChange;
    public bool removeRandomCard;
    public string resultMessage;
}

[CreateAssetMenu(fileName = "NewEvent", menuName = "Game/Event")]
public class EventData : ScriptableObject
{
    public string eventName;
    public string description;
    public List<EventChoice> choices;
}
