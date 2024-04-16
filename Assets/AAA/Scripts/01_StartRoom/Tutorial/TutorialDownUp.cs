using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialDownUp : TutorialBase
{
    [SerializeField] InputActionAsset inputActionAsset;
    [SerializeField] bool isDownUp;

    public override void Enter(TutorialController controller)
    {
        // 메세지 띄우기
        base.Enter(controller);

        blinkIndex = 0;
        // ControllerModelBlink 활성화
        controller.ControllerModelBlink.SetModelToBlink(blinkIndex);
        controller.ControllerModelBlink.enabled = true;
    }

    public override void Excute(TutorialController controller)
    {
        isDownUp = inputActionAsset.actionMaps[1].actions[2].triggered;
        if (isDownUp == true)
        {
            Debug.Log("앉기누름");
        }

    }

    public override void Exit(TutorialController controller)
    {

    }
}
