using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGrip : TutorialBase
{
    [SerializeField] RightGraber rightGraber;

    public override void Enter(TutorialController controller)
    {
        // �޼��� ����
        base.Enter(controller);

        blinkIndex = 2;
        // ControllerModelBlink Ȱ��ȭ
        controller.ControllerModelBlink.SetModelToBlink(blinkIndex);
        controller.ControllerModelBlink.enabled = true;
    }

    public override void Excute(TutorialController controller)
    {
        if (rightGraber.GunGrip == true)
        {
            controller.SetNextTutorial();
        }
    }

    public override void Exit(TutorialController controller)
    {
        // ControllerModelBlink ��Ȱ��ȭ, modelToBlink ����
        controller.ControllerModelBlink.enabled = false;
        controller.ControllerModelBlink.NullModelToBlink();
    }
}
