using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class Level : MonoBehaviour
{
	public Game game;
	[Tag] public string playerTag = "Player";

	[Header("Level Config")]
	[FormerlySerializedAs("underwaterPlayerVisual")] public bool isUnderwater = true;
	public bool anchorCameraOnEnter = true;
	public bool splashWaterOnEnter = false;

	[Space]
	public Collider2D entranceCollider;



	private void OnValidate()
	{
		if (!game) game = FindObjectOfType<Game>();
	}
	private void Awake()
	{
		Assert.IsNotNull(game);

		if (entranceCollider)
		{
			entranceCollider.enabled = false;
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(playerTag))
		{
			Debug.Log($"{playerTag} has entered {this.gameObject.name}");
			
			game.LevelEntered(this);
			
			if (entranceCollider)
			{
				Debug.Log("Closing the entrance");
				entranceCollider.enabled = true;
			}
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag(playerTag))
		{
			Debug.Log($"{playerTag} has left {this.gameObject.name}");
		}
	}







	public static Vector2? DetermineMovementDestinationPosition(
		Vector2 from,
		Vector2 controlVector,
		float obstacleCheckRadius,
		LayerMask obstacleLayers)
	{
		var direction = GetNormalizedDirectionFromControlVector(controlVector);
		if (direction == null) return null;

		//Debug.Log(direction);
		Vector2? previousPoint = null;
		for (int i = 1; i < 20; i++)
		{
			Vector2 point = from + (Vector2)(direction.Value) * Game.GridSize * i;
			var blocked = CheckPointForObstacle(point, obstacleCheckRadius, obstacleLayers);

			if (blocked)
			{
				// Path blocked - return previous step (null when first step is this and already blocked)
				return previousPoint;
			}

			previousPoint = point;
		}

		Debug.LogWarning("Movement into the void.");
		return null;
	}
	static Vector2Int? GetNormalizedDirectionFromControlVector(Vector2 controlVector)
	{
		if (controlVector == Vector2.zero) return null;

		var direction = controlVector.normalized;
		if (direction != Vector2.up &&
			direction != Vector2.down &&
			direction != Vector2.left &&
			direction != Vector2.right)
		{
			// Non exclusive axis vector
			return null;
		}

		return new Vector2Int((int)direction.x, (int)direction.y);
	}
	static bool CheckPointForObstacle(Vector2 point, float obstacleCheckRadius, LayerMask obstacleLayers)
	{
		var hit = Physics2D.OverlapCircle(
			point,
			obstacleCheckRadius,
			obstacleLayers);

		return (hit);
	}
}