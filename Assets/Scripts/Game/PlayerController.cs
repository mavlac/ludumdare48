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
			player.InputControlAcceptedAsMovement();

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
		StartCoroutine(MovementCoroutine(rb.position, destination, MovementFinished));
		
		void MovementFinished()
		{
			player.MovementFinished();
		}
	}
	IEnumerator MovementCoroutine(Vector2 start, Vector2 destination, System.Action movementFinished)
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
		
		movementFinished.Invoke();
	}
}