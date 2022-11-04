using UnityEngine;
using Cinemachine;

    public class FindPlayer : MonoBehaviour
    {
    void Start()
        {
            this.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = FindObjectOfType<PlayerMovement>().gameObject.transform;
            this.GetComponent<Cinemachine.CinemachineVirtualCamera>().LookAt = FindObjectOfType<PlayerMovement>().gameObject.transform;
        
        }
	private void Update()
	{
        LookAtPlayer();
	}
    public void LookAtPlayer()
	{
        if (!PlayerMovement.isRunning)
        {
            CinemachineTransposer transposer = gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>();
            transposer.m_BindingMode = CinemachineTransposer.BindingMode.WorldSpace;
        }
    }

}

