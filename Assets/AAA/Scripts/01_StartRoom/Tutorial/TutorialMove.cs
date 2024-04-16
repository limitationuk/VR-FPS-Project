using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMove : TutorialBase
{
    [SerializeField] OnTriggerCheck onTriggerCheck;

    public override void Enter(TutorialController controller)
    {
        // �޼��� ����
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

        // ControllerModelBlink Ȱ��ȭ
        controller.ControllerModelBlink.SetModelToBlink(blinkIndex);
        controller.ControllerModelBlink.enabled = true;

        // Mark Ȱ��ȭ
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

        // Mark ��Ȱ��ȭ
        controller.Marks[marksIndex].gameObject.SetActive(false);

        // ControllerModelBlink ��Ȱ��ȭ, modelToBlink ����
        controller.ControllerModelBlink.enabled = false;
        controller.ControllerModelBlink.NullModelToBlink();
    }
}
