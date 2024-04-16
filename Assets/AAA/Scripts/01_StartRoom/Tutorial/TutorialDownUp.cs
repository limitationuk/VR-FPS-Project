using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDownUp : TutorialBase
{
    public override void Enter(TutorialController controller)
    {
        // 메세지 띄우기
        base.Enter(controller);

        blinkIndex = 0;
        // modelToBlink 할당, ControllerModelBlink 활성화
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
