using System.Collections;
using UnityEngine;

public class TutorialBasic : TutorialBase
{
    [SerializeField] float messageCoolTime;
    [SerializeField][ReadOnly] float currentTime;
    [SerializeField][ReadOnly] bool isCompleted = false;

    public override void Enter(TutorialController controller)
    {
        // �޼��� ����
        base.Enter(controller);

        StartCoroutine(MessageRoutine());
    }

    public override void Excute(TutorialController controller)
    {
        if (isCompleted == true)
        {
            controller.SetNextTutorial();
        }
    }

    public override void Exit(TutorialController controller)
    {
    }

    IEnumerator MessageRoutine()
    {
        currentTime = 0;

        while (currentTime < messageCoolTime)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        isCompleted = true;
    }
}
