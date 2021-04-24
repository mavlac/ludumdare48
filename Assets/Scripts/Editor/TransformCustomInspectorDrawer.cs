using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Extending default Transform Inspector,
/// now with uniform scaling option and all Vector3 values reset functionality.
/// </summary>
/// <remarks>
/// Credit for hooking up to default Inspector goes to Cobo3 from Unity Forums
/// https://forum.unity.com/threads/extending-instead-of-replacing-built-in-inspectors.407612/
/// </remarks>
[CustomEditor(typeof(Transform), true), CanEditMultipleObjects]
public class TransformCustomInspectorDrawer : Editor
{
	Editor defaultEditor; // Unity's built-in editor
	Transform transform;
	
	
	private bool uniformScaling = false;
	
	
	
	void OnEnable()
	{
		// When this inspector is created, also create the built-in inspector
		defaultEditor = Editor.CreateEditor(targets, Type.GetType("UnityEditor.TransformInspector, UnityEditor"));
		transform = target as Transform;
		
		// Enable uniform scaling by default, if the scale is non 1,1,1 and all the values are identical
		uniformScaling =
			transform.localScale != Vector3.one &&
			(transform.localScale.x == transform.localScale.y && transform.localScale.y == transform.localScale.z);
	}
	void OnDisable()
	{
		if (!defaultEditor) return;
		
		// When OnDisable is called, the default editor we created should be destroyed to avoid memory leakage.
		// Also, make sure to call any required methods like OnDisable
		MethodInfo disableMethod = defaultEditor.GetType().GetMethod("OnDisable", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
		if (disableMethod != null)
			disableMethod.Invoke(defaultEditor, null);
		
		DestroyImmediate(defaultEditor);
	}
	
	public override void OnInspectorGUI()
	{
		defaultEditor.OnInspectorGUI();
		
		
		// Optionable uniform scaling
		if (transform.localScale.x != 1f ||
			(transform.localScale.x != transform.localScale.y) ||
			(transform.localScale.x != transform.localScale.z))
		{
			EditorGUI.indentLevel++;
			uniformScaling = EditorGUILayout.Toggle("Uniform Scaling", uniformScaling);
			EditorGUI.indentLevel--;
			
			if (uniformScaling && Application.isEditor && !Application.isPlaying)
			{
				transform.localScale = Vector3.one * transform.localScale.x;
			}
		}
		
		
		// Reset
		GUILayout.Space(EditorGUIUtility.standardVerticalSpacing);
		GUILayout.BeginHorizontal();
		GUILayout.Space(EditorGUIUtility.labelWidth);
		ButtonGroup buttonGroup = new ButtonGroup(this.transform);
		if (transform.localPosition != Vector3.zero)
		{
			if (GUILayout.Button("Reset Position", buttonGroup.ThisButtonStyle, GUILayout.Height(22)))
			{
				Undo.RegisterCompleteObjectUndo(target, $"Reset {transform.gameObject.name} Transform");
				transform.localPosition = Vector3.zero;
			}
		}
		if (transform.localRotation != Quaternion.identity)
		{
			if (GUILayout.Button("Reset Rotation", buttonGroup.ThisButtonStyle, GUILayout.Height(22)))
			{
				Undo.RegisterCompleteObjectUndo(target, $"Reset {transform.gameObject.name} Transform");
				transform.localRotation = Quaternion.identity;
			}
		}
		if (transform.localScale != Vector3.one)
		{
			if (GUILayout.Button("Reset Scale", buttonGroup.ThisButtonStyle, GUILayout.Height(22)))
			{
				Undo.RegisterCompleteObjectUndo(target, $"Reset {transform.gameObject.name} Transform");
				transform.localScale = Vector3.one;
			}
		}
		GUILayout.EndHorizontal();
	}






	public class ButtonGroup
	{
		readonly Transform transform;
		
		int currentButtonOfGroup = 0;
		
		public ButtonGroup(Transform transform)
		{
			this.transform = transform;
		}
		
		
		
		/// <summary>
		/// How many reset Buttons to show in horizontal group of this Transform
		/// </summary>
		public int ButtonCount
		{
			get
			{
				int i = 0;
				if (transform.localPosition != Vector3.zero) i++;
				if (transform.localRotation != Quaternion.identity) i++;
				if (transform.localScale != Vector3.one) i++;
				return i;
			}
		}
		
		/// <summary>
		/// Returns style of a Button, increments iterator itself
		/// </summary>
		public GUIStyle ThisButtonStyle
		{
			get
			{
				if (ButtonCount == 1) return GUI.skin.FindStyle("button");
				
				if (currentButtonOfGroup == 0)
				{
					currentButtonOfGroup++;
					//return EditorStyles.miniButtonLeft;
					return GUI.skin.FindStyle("buttonleft");
				}
				else if (currentButtonOfGroup < ButtonCount - 1)
				{
					currentButtonOfGroup++;
					//return EditorStyles.miniButtonMid;
					return GUI.skin.FindStyle("buttonmid");
				}
				else
				{
					currentButtonOfGroup++;
					//return EditorStyles.miniButtonRight;
					return GUI.skin.FindStyle("buttonright");
				}
			}
		}
	}
}