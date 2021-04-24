// Credit goes to FreCre and following asset:
// https://assetstore.unity.com/packages/tools/easyshortcutlockinspector-23579

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class LockInspectorHotkey
{
	[MenuItem("Tools/Toggle Inspector Lock %l")] // CTRL - L
	private static void ToggleInspectorLock()
	{
		ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
		ActiveEditorTracker.sharedTracker.ForceRebuild();
	}
}
#endif