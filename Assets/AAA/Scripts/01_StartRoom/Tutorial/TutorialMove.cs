using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMove : TutorialBase
{
    [SerializeField] OnTriggerCheck onTriggerCheck;

    public override void Enter(TutorialController controller)
    {
        // 메세지 띄우기
        base.Enter(controller);

        int index = controller.Index;

        switch (index)
        {
            case 1:
                blinkIndex = 0;
                marksIndex = 0;
                break;
            case 4:
                blinkIndex = 0;
                marksIndex = 1;
                break;
            default:
                break;
        }

        // ControllerModelBlink 활성화
        controller.ControllerModelBlink.SetModelToBlink(blinkIndex);
        controller.ControllerModelBlink.enabled = true;

        // Mark 활성화
        controller.Marks[marksIndex].gameObject.SetActive(true);
    }

    public override void Excute(TutorialController controller)
    {
        if (onTriggerCheck.isOnTrigger == true)
        {
            controller.SetNextTutorial();
        }
    }

    public override void Exit(TutorialController controller)
    {
        onTriggerCheck.isOnTrigger = false;

        // Mark 비활성화
        controller.Marks[marksIndex].gameObject.SetActive(false);

        // ControllerModelBlink 비활성화, modelToBlink 제거
        controller.ControllerModelBlink.enabled = false;
        controller.ControllerModelBlink.NullModelToBlink();
    }
}
