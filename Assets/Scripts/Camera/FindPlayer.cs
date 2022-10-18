using UnityEngine;

    public class FindPlayer : MonoBehaviour
    {
        void Start()
        {
            this.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = FindObjectOfType<PlayerMovement>().gameObject.transform;
            this.GetComponent<Cinemachine.CinemachineVirtualCamera>().LookAt = FindObjectOfType<PlayerMovement>().gameObject.transform;
        }

    }

