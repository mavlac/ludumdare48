using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDebug : MonoBehaviour
{
#if UNITY_EDITOR
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftShift))
		{
			RestartCurrentScene();
		}
		if (Input.GetKeyDown(KeyCode.F) && Input.GetKey(KeyCode.LeftShift))
		{
			var game = FindObjectOfType<Game>();
			if (game) game.Victory();
		}
		if (Input.GetKeyDown(KeyCode.K) && Input.GetKey(KeyCode.LeftShift))
		{
			var game = FindObjectOfType<Game>();
			if (game) game.GameOver();
		}
	}
#endif



	public void RestartCurrentScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}