using UnityEngine;
using PathCreation;

public class PlayerController : MonoBehaviour
{
	#region Private Variables
	[SerializeField]private GameObject player;
	[SerializeField]private float _speed;
	[SerializeField]private float _speedHorizontal;
	[SerializeField]private PathCreator pathCreator;

	private bool isGameOver;
	private Vector2 mouseRootPos;
	private float inputHorizontal;
	private float horizontalOffSet;

	private EndOfPathInstruction _end;
	private float _dstTravelled;


	private Vector3 screenPosition;
	private Vector3 worldPosition;
	#endregion
	#region Properties
	public float speed { get => _speed; set => _speed = value; }
	public float speedHorizontal { get => _speedHorizontal; set => _speedHorizontal = value; }
	public float dstTravelled { get => _dstTravelled; set => _dstTravelled = value; }
	public EndOfPathInstruction end {get => _end;}

	#endregion
	private void OnEnable()
	{
		GameManager.OnGameRunning += MoveToEnd;
		GameManager.OnGameRunning += GetInput;
	}
	private void OnDisable()
	{
		GameManager.OnGameRunning -= MoveToEnd;
		GameManager.OnGameRunning -= GetInput;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "FinishLine")
		{
			GameManager.OnGameEnd?.Invoke();
			isGameOver = true;
		}
	}
	public void GetInput()
	{
		//Mouse position input
		screenPosition = Input.mousePosition;
		screenPosition.z = Camera.main.nearClipPlane + 1;
		worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

		if (worldPosition.x > transform.position.x)
		{
			inputHorizontal = 1f;
		}
		else if (worldPosition.x < transform.position.x)
		{
			inputHorizontal = -1f;
		}
		else
		{
			inputHorizontal = 0f;
		}
		#region Click and Drag Input Deactivated
		//Click and Drag Input
		/*if (Input.GetMouseButtonDown(0))
		{
			mouseRootPos = Input.mousePosition;
		}
		else if (Input.(0))
		{
			var dragVec = (Vector2)Input.mousePosition - mouseRootPos;
			dragVec.Normalize();
			inputHorizontal = dragVec.x;
			mouseRootPos = Input.mousePosition;
		}
		else
		{
			inputHorizontal = 0;
		}
	Debug.Log(inputHorizontal);*/
		#endregion
	}
	public void MoveToEnd()
	{
		//Moving with Spline Controller

		if (!isGameOver && pathCreator != null)
		{
			dstTravelled += speed * Time.deltaTime;
			Vector3 road = pathCreator.path.GetPointAtDistance(dstTravelled, end);
			if (inputHorizontal == 0)
			{
				horizontalOffSet = Mathf.MoveTowards(horizontalOffSet, 0, Time.deltaTime * speed);
			}
			else
			{
				horizontalOffSet += inputHorizontal * Time.deltaTime * speed;
			}
			transform.position = road;
			horizontalOffSet = Mathf.Clamp(horizontalOffSet, -2.5f, 3f);
			road = transform.TransformPoint(new Vector3(horizontalOffSet, 0, 0));
			transform.position = road;

			#region Moving with Transform Translate Deactivated
			/*
			transform.Translate(Vector3.forward * speed * Time.deltaTime);
			var playerBasePos = player.transform.localPosition;
			playerBasePos += Vector3.right * inputHorizontal * speedHorizontal * Time.deltaTime;
			playerBasePos.x = Mathf.Clamp(playerBasePos.x, -3f, 3f);
			player.transform.localPosition = playerBasePos;
			*/
			#endregion
		}
	}
}
