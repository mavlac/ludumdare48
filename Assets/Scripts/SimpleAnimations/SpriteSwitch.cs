using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Simple sprite switching animation
/// supports applying onto SpriteRendere or UI.Image
/// </summary>
//[RequireComponent(typeof(SpriteRenderer))] // now looking for SpriteRenderer OR UI.Image and such multicondition not definable
public class SpriteSwitch : MonoBehaviour
{
	[System.Serializable]
	public class FrameTimeOverrides
	{
		[System.Serializable]
		public struct FrameTime
		{
			public int index;
			public float time;
		}

		[SerializeField] List<FrameTime> frameTimes = default;
		
		public float GetFrameTime(int frameIndex, float defaultValue)
		{
			if (frameTimes.Count == 0) return defaultValue;
			
			foreach(var ft in frameTimes)
				if (ft.index == frameIndex) return ft.time;
			
			return defaultValue;
		}
	}

	[Space]
	public bool playOnAwake = true;
	public float frameTime = 0.25f;
	public bool loop = true;
	public bool destroyAtLoopEnd = false;

	[Space]
	public List<Sprite> spriteSet;
	[Tooltip("Specified frames can override default frameTime value.")]
	public FrameTimeOverrides frameTimeOverrides;

	[Space]
	public UnityEvent onLoopEnd;
	
	
	bool running = false;
	int frameIndex;
	float frameRemainingTime = 0f;
	
	SpriteRenderer sr;
	Image uiImg;
	
	
	
	void Awake()
	{
		sr = gameObject.GetComponent<SpriteRenderer>();
		uiImg = gameObject.GetComponent<Image>();

		Assert.IsTrue(spriteSet.Count > 0, $"SpriteSwitch on {gameObject.name} with undefined frames.");

		if (playOnAwake) TriggerAnimation();
	}
	void Update()
	{
		if (!running) return;
		
		frameRemainingTime -= Time.deltaTime;
		
		if (frameRemainingTime <= 0f)
		{
			frameIndex++;
			
			if (frameIndex >= spriteSet.Count)
			{
				frameIndex = 0;
				
				onLoopEnd.Invoke();
				if (destroyAtLoopEnd)
				{
					Destroy(gameObject);
					return;
				}
				if (!loop)
				{
					running = false;
					return; // do not even switch sprite back to [0]
				}
			}
			
			UpdateSprite(spriteSet[frameIndex]);
			
			frameRemainingTime = frameTimeOverrides.GetFrameTime(frameIndex, frameTime);
		}
	}



	void UpdateSprite(Sprite s)
	{
		if (sr) sr.sprite = s;
		if (uiImg) uiImg.sprite = s;
	}



	public void TriggerAnimation()
	{
		frameIndex = 0;
		UpdateSprite(spriteSet[frameIndex]);

		frameRemainingTime = frameTimeOverrides.GetFrameTime(frameIndex, frameTime);
		running = true;
	}
	public void SetFrame(int frame = 0)
	{
		frameIndex = Mathf.Clamp(frame, 0, spriteSet.Count);
		UpdateSprite(spriteSet[frameIndex]);
	}
}
