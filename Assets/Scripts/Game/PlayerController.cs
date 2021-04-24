using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, InputControls.IGameplayActions
{
	public Player player;

	[Header("Events")]
	public EventSO jumpFromBoat;


	private InputControls inputControls;

	private Rigidbody2D rb;


	bool firstMovement = true;



	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		Assert.IsNotNull(rb);

		inputControls = new InputControls();
		inputControls.Gameplay.SetCallbacks(this);
	}
	private void OnEnable()
	{
		inputControls.Enable();
	}
	private void OnDisable()
	{
		inputControls.Disable();
	}



	public void OnMovement(InputAction.CallbackContext context)
	{
		if (player.game.Phase != Game.GamePhase.Idle) return;
		
		if (firstMovement)
		{
			jumpFromBoat.Raise();
			firstMovement = false;
		}

		
		Vector2 controlVector;
		controlVector = context.ReadValue<Vector2>();
	}
}