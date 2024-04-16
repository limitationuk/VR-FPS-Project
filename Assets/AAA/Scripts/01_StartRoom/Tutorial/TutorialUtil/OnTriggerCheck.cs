using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using UnityEngine;

public class OnTriggerCheck : MonoBehaviour
{
    [ReadOnly] public bool isOnTrigger = false;
    [SerializeField] GameObject mark;
    [SerializeField][ReadOnly] GameObject[] marks;

    private void OnTriggerEnter(Collider other)
    {
        marks = mark.GetComponentsInChildren<GameObject>();

        if (other.gameObject == marks[0].gameObject)
            isOnTrigger = true;
    }
}
