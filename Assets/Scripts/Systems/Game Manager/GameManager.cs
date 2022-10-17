using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
	#region GAME STATE EVENTS
	[Header("GAME START\n")]
    public static UnityAction OnGameStart;

    [Header("GAME RUNNING\n")]
	public static UnityAction OnGameRunning;

    [Header("GAME END\n")]
	public static UnityAction OnGameEnd;
	#endregion
	private void Start()
	{
		OnGameStart?.Invoke(); //Invoking The Inception of the Game.
	}
	private void Update()
	{
		OnGameRunning?.Invoke(); // Invoking The GamePlay.
	}
}
