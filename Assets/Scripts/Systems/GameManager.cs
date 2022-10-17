using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
	#region GAME STATE EVENTS
	[Header("GAME START")]
    public static UnityAction OnGameStart;

    [Header("GAME RUNNING")]
	public static UnityAction OnGameRunning;

    [Header("GAME END")]
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
