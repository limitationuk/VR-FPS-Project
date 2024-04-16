using UnityEngine;

public class OnTriggerCheck : MonoBehaviour
{
    public bool isOnTrigger = false;
    [SerializeField] GameObject[] marks;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == marks[0].gameObject || other.gameObject == marks[1].gameObject)
            isOnTrigger = true;
    }
}
