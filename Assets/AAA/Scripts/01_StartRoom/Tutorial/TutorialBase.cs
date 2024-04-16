using UnityEngine;

public abstract class TutorialBase : MonoBehaviour
{
    public int blinkIndex;
    public int marksIndex;

    // 해당 튜토리얼 과정을 시작할 때 1회 호출
    public virtual void Enter(TutorialController controller)
    {
        Debug.Log($"Base - Enter {controller.Index} 실행");

        // 어떤 메세지 띄울지 결정
        controller.TutorialUI.SetTutorialMessage(controller.Index);
    }

    // 해당 튜토리얼 과정을 진행하는 동안 매 프레임 호출
    public virtual void Excute(TutorialController controller)
    {
    }

    // 해당 튜토리얼 과정을 종료할 때 1회 호출
    public virtual void Exit(TutorialController controller)
    {

    }
}
