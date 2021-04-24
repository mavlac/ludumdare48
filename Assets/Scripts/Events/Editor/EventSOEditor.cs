using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EventSO)), CanEditMultipleObjects]
public class EventSOEditor : Editor
{
	public override void OnInspectorGUI()
	{
		EventSO inspectedEvent = (EventSO)target;
		
		DrawDefaultInspector();
		
		
		GUILayout.Space(10);
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Manual trigger");
		if (GUILayout.Button("Raise Event", GUILayout.Height(22)))
		{
			inspectedEvent.Raise();
		}
		EditorGUILayout.EndHorizontal();
	}
}