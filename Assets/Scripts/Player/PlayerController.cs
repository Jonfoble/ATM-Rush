using UnityEngine;
using PathCreation;

public class PlayerController : MonoBehaviour
{
	#region Private Variables
	[SerializeField]private GameObject player;
	[SerializeField]private float _speed;
	[SerializeField] private float _speedHorizontal;
	[SerializeField]private PathCreator pathCreator;

	private bool isGameOver;
	private Vector2 mouseRootPos;
	private float inputHorizontal;

	private EndOfPathInstruction _end;
	private float _dstTravelled;
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
	}
	private void OnDisable()
	{
		GameManager.OnGameRunning -= MoveToEnd;
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
			if (Input.GetMouseButtonDown(0))
			{
				mouseRootPos = Input.mousePosition;
			}
			else if (Input.GetMouseButton(0))
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
		
	}
	public void MoveToEnd()
	{
		if (!isGameOver)
		{
			GetInput();
			transform.Translate(Vector3.forward * speed * Time.deltaTime);
			var playerBasePos = player.transform.localPosition;
			playerBasePos += Vector3.right * inputHorizontal * speedHorizontal * Time.deltaTime;
			playerBasePos.x = Mathf.Clamp(playerBasePos.x, -3f, 3f);
			player.transform.localPosition = playerBasePos;
		}
	}
}
