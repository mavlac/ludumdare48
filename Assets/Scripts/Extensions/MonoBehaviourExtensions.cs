using System.Collections;
using UnityEngine;

public static class MonoBehaviourExtensions
{
	public static void Invoke(this MonoBehaviour monoBehaviour, System.Action methodDelegate, float time)
	{
		monoBehaviour.StartCoroutine(ExecuteAfterDelayCoroutine(methodDelegate, time));
		
		IEnumerator ExecuteAfterDelayCoroutine(System.Action method, float delay)
		{
			yield return new WaitForSeconds(delay);
			
			method.Invoke();
		}
	}
}