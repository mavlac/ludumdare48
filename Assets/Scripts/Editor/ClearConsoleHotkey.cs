// Credit goes to KirillKuzyk from answers.unity.com
// https://answers.unity.com/questions/707636/clear-console-window.html

#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public static class ClearConsoleHotkey
{
	[MenuItem("Tools/Clear Console %q")] // CTRL + Q
	private static void ClearConsole()
	{
		Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
		Type type = assembly.GetType("UnityEditor.LogEntries");
		MethodInfo method = type.GetMethod("Clear");
		method.Invoke(new object(), null);
	}
}
#endif