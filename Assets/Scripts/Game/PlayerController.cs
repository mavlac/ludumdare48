using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, InputControls.IGameplayActions
{
	public void OnMovement(InputAction.CallbackContext context)
	{
		Vector2 controlVector;
		controlVector = context.ReadValue<Vector2>();
	}
}