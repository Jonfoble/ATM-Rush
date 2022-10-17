using UnityEngine;

public class FindPlayer : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = FindObjectOfType<PlayerController>().gameObject.transform;
        this.GetComponent<Cinemachine.CinemachineVirtualCamera>().LookAt = FindObjectOfType<PlayerController>().gameObject.transform;
    }

}
