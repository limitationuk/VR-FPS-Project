using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGrip : TutorialBase
{
    RightGraber rightGraber;

    public override void Enter(TutorialController controller)
    {
        // 메세지 띄우기
        base.Enter(controller);

        blinkIndex = 2;
        // modelToBlink 할당, ControllerModelBlink 활성화
        controller.ControllerModelBlink.SetModelToBlink(blinkIndex);
        controller.ControllerModelBlink.enabled = true;

        // 무기 오브젝트 활성화
        controller.StartRoomGun.gameObject.SetActive(true);
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
        // ControllerModelBlink 비활성화, modelToBlink 제거
        controller.ControllerModelBlink.enabled = false;
        controller.ControllerModelBlink.NullModelToBlink();
    }
}
