// Based on original code by Dylan Engelman http://jupiterlighthousestudio.com/custom-inspectors-unity/
// altered by Brecht Lecluyse http://www.brechtos.com 
// and Sichen Liu https://sichenn.github.io
// and dbrizov https://github.com/dbrizov
using UnityEngine;
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
#endif

/// <summary>
/// Make Tag appear as dropdown (EditorGUI.Popup)
/// </summary>
public class TagAttribute : PropertyAttribute
{
}



#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(TagAttribute))]
public class TagDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		if (property.propertyType == SerializedPropertyType.String)
		{
			// Generate the taglist + custom tags
			List<string> tagList = new List<string>
			{
				"(None)",
				"Untagged"
			};
			tagList.AddRange(UnityEditorInternal.InternalEditorUtility.tags);
			
			string propertyString = property.stringValue;
			int index = 0;
			// Check if there is an entry that matches the entry and get the index
			// We skip index 0 as that is a special custom case
			for (int i = 1; i < tagList.Count; i++)
			{
				if (tagList[i] == propertyString)
				{
					index = i;
					break;
				}
			}

			// Draw the popup box with the current selected index
			EditorGUI.BeginProperty(position, label, property);
			index = EditorGUI.Popup(position, label.text, index, tagList.ToArray());
			EditorGUI.EndProperty();

			// Adjust the actual string value of the property based on the selection
			if (index > 0)
			{
				property.stringValue = tagList[index];
			}
			else
			{
				property.stringValue = string.Empty;
			}
		}
		else
		{
			EditorGUILayout.HelpBox(property.type + " is not supported by TagAttribute\n" + "Use string instead", MessageType.Warning);
		}
	}
}
#endif