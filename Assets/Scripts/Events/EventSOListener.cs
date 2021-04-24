using UnityEngine;
using UnityEngine.Events;

public class EventSOListener : MonoBehaviour
{
	public EventSO eventDef;
	public UnityEvent response;
	
	
	
	private void OnEnable()
	{
		eventDef.RegisterListener(this);
	}
	private void OnDisable()
	{
		eventDef.UnregisterListener(this);
	}
	
	
	
	public void OnEventRaised()
	{
		response.Invoke();
	}
}