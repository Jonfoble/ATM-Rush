using UnityEngine;
using PathCreation;

public class PlayerMovement : MonoBehaviour
{
	#region Private Variables
	[SerializeField] private GameObject _player;
	[SerializeField] private float _speed;
	[SerializeField] private PathCreator _pathCreator;
	private EndOfPathInstruction _end;
	private float _dstTravelled;
	#endregion
	#region Properties
	public GameObject player { get => _player; set => _player = value; }
	public float speed { get => _speed; set => _speed = value; }
	public float dstTravelled { get => _dstTravelled; set => _dstTravelled = value; }
	public EndOfPathInstruction end { get => _end; }
	public float DstTravelled { get => _dstTravelled; set => _dstTravelled = value; }
	#endregion

	private void OnEnable()
	{
		GameManager.OnGameRunning += Move;
	}
	private void OnDisable()
	{
		GameManager.OnGameRunning -= Move;
	}
	public void Move()
	{
		//Moving with Spline Controller

		if (_pathCreator != null)
		{
			dstTravelled += speed * Time.deltaTime;
			Vector3 road = _pathCreator.path.GetPointAtDistance(dstTravelled, end);
			if (GetInput.inputHorizontal == 0)
			{
				GetInput.horizontalOffSet = Mathf.MoveTowards(GetInput.horizontalOffSet, 0, Time.deltaTime * speed);
			}
			else
			{
				GetInput.horizontalOffSet += GetInput.inputHorizontal * Time.deltaTime * speed;
			}
			transform.position = road;
			GetInput.horizontalOffSet = Mathf.Clamp(GetInput.horizontalOffSet, -2.5f, 3f);
			road = transform.TransformPoint(new Vector3(GetInput.horizontalOffSet, 0, 0));
			transform.position = road;
		}
	}
}
