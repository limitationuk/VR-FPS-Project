using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDownUp : TutorialBase
{
    public override void Enter(TutorialController controller)
    {
        // �޼��� ����
        base.Enter(controller);

        blinkIndex = 0;
        // modelToBlink �Ҵ�, ControllerModelBlink Ȱ��ȭ
        controller.ControllerModelBlink.SetModelToBlink(blinkIndex);
        controller.ControllerModelBlink.enabled = true;
    }

    public override void Excute(TutorialController controller)
    {

    }

    public override void Exit(TutorialController controller)
    {

    }
}
