using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Event SO", order = 33)]
public class EventSO : ScriptableObject
{
	[SerializeField, Space]
	private bool logWhenRaised = false;
	
	[SerializeField, TextArea, Space]
	private string userComment;
	
	
	private readonly List<EventSOListener> listeners = new List<EventSOListener>();
	private readonly List<System.Action> actions = new List<System.Action>();
	
	
	
	public void Raise()
	{
		if (logWhenRaised) Debug.Log($"{this.name} EventSO raised.");
		
		for (int i = listeners.Count - 1; i >= 0; i--)
		{
			listeners[i].OnEventRaised();
		}
		for (int i = actions.Count - 1; i >= 0; i--)
		{
			actions[i].Invoke();
		}
	}
	
	
	
	public void RegisterListener(EventSOListener listener)
	{
		listeners.Add(listener);
	}
	public void UnregisterListener(EventSOListener listener)
	{
		listeners.Remove(listener);
	}
	public void RegisterAction(System.Action action)
	{
		actions.Add(action);
	}
	public void UnregisterAction(System.Action action)
	{
		actions.Remove(action);
	}
}
