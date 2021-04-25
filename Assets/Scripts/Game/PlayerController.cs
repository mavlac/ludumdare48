using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, InputControls.IGameplayActions
{
	public Player player;

	[Space]
	public LayerMask obstacleLayers;
	public float obstacleCheckRadius = 0.01f;

	[Space]
	public float movementSpeed = 1f;
	public AnimationCurve movementProgression = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	[Space]
	public ParticleSystem moveParticles;

	[Header("Events")]
	public EventSO playerKilled;
	public EventSO treasureReached;


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
		playerKilled.RegisterAction(OnEventThatStopsMovement);
		treasureReached.RegisterAction(OnEventThatStopsMovement);
	}
	private void OnDisable()
	{
		inputControls.Disable();
		playerKilled.UnregisterAction(OnEventThatStopsMovement);
		treasureReached.UnregisterAction(OnEventThatStopsMovement);
	}
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, obstacleCheckRadius);
	}



	public void OnMovement(InputAction.CallbackContext context)
	{
		if (player.game.Phase != Game.GamePhase.Idle) return;
		
		player.InputControlTriggered();
		
		Vector2 controlVector;
		controlVector = context.ReadValue<Vector2>();
		
		var destinationPosition =
			Level.DetermineMovementDestinationPosition(rb.position, controlVector, obstacleCheckRadius, obstacleLayers);

		if (destinationPosition.HasValue)
		{
			player.MovementInitiated();
			
			if (firstMovement)
			{
				player.JumpedFromBoat();
				firstMovement = false;
			}
			
			MoveTo(destinationPosition.Value);
		}
	}
	void OnEventThatStopsMovement()
	{
		StopAllCoroutines();
	}







	public void MoveTo(Vector2 destination)
	{
		StartCoroutine(MoveToCoroutine(destination));
	}
	IEnumerator MoveToCoroutine(Vector2 destination)
	{
		// Move or Shoot and move
		if (player.launcher.Armed)
		{
			// Shoot the harpoon
			// Wait until it hits the obstacle
			yield return StartCoroutine(player.launcher.ShotCoroutine(destination));
		}
		else
		{
			// Arm the launcher
			player.launcher.Arm();
		}
		
		// Move the Player
		yield return StartCoroutine(PlayerMovementCoroutine(rb.position, destination));
		
		player.MovementFinished();
	}
	IEnumerator PlayerMovementCoroutine(Vector2 start, Vector2 destination)
	{
		var duration = Vector2.Distance(start, destination) * movementSpeed;
		
		// Spawn particles if level is underwater
		if (player.CurrentLevel.isUnderwater)
		{
			moveParticles.Play();
		}
		
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			float p = Mathf.InverseLerp(0f, duration, t);
			
			rb.MovePosition(Vector2.Lerp(start, destination, movementProgression.Evaluate(p)));
			
			yield return null;
		}

		// Stop particles
		moveParticles.Stop();

		rb.MovePosition(destination);
	}
}