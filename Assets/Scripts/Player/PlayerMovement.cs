using UnityEngine;
using DG.Tweening;
using PathCreation;

public class PlayerMovement : MonoBehaviour
{
	#region Private Variables
	[SerializeField] private GameObject _player;
	[SerializeField] private float _speed;
	[SerializeField] private float _horizontalSpeed;
	[SerializeField] private PathCreator _pathCreator;
	private EndOfPathInstruction _end;
	private float _dstTravelled;
	#endregion

	#region Public Variables

	public static bool isRunning = true;
	#endregion

	#region Properties
	public float horizontalSpeed { get => _horizontalSpeed; set => _horizontalSpeed = value; }
	public GameObject player { get => _player; set => _player = value; }
	public float speed { get => _speed; set => _speed = value; }
	public float dstTravelled { get => _dstTravelled; set => _dstTravelled = value; }
	public EndOfPathInstruction end { get => _end; set => _end = value; }
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
			Vector3 road = _pathCreator.path.GetPointAtDistance(dstTravelled, end = EndOfPathInstruction.Stop);
			Vector3 lastPoint = _pathCreator.path.GetPoint(_pathCreator.path.localPoints.Length - 1);
			if (GetInput.inputHorizontal == 0)
			{
				//If no input, dont Move. Otherwise move according to input.
			}
			else
			{
				if (lastPoint != road) // Stop moving if reached out to end
				{
					GetInput.horizontalOffSet += GetInput.inputHorizontal * Time.deltaTime * horizontalSpeed;
				}
			}
			if (lastPoint == road)
			{
				isRunning = false;
				this.transform.DOMove(lastPoint, 0.7f);
				this.enabled = false;
			}
			transform.position = road;
			GetInput.horizontalOffSet = Mathf.Clamp(GetInput.horizontalOffSet, -2.5f, 3f);
			road = transform.TransformPoint(new Vector3(GetInput.horizontalOffSet, 0, 0));
			transform.position = road;
		}
	}
}
