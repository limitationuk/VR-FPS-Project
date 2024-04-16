using System.Collections;
using UnityEngine;

public class TutorialBasic : TutorialBase
{
    [SerializeField] float messageCoolTime;
    [SerializeField] bool isCompleted = false;

    public override void Enter(TutorialController controller)
    {
        // 메세지 띄우기
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
        float currentTime = 0;

        while (currentTime < messageCoolTime)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        isCompleted = true;
    }
}
