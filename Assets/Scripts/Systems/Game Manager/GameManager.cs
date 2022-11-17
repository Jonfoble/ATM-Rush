using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
	#region GAME STATE EVENTS
	[Header("GAME START\n")]
    public static UnityAction OnGameStart;

    [Header("GAME RUNNING\n")]
	public static UnityAction OnGameRunning;

    [Header("GAME END\n")]
	public static UnityAction OnGameEnd;

    public static bool isGameRunning;
	#endregion
	private void Update()
	{
		if (isGameRunning)
		{
			OnGameRunning?.Invoke();
		}
	}
	public void LoadNextLevel()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        RestartLevel();
    }
    public void RestartLevel()
    {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
