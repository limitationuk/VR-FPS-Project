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
        // �޼��� ����
        base.Enter(controller);

        blinkIndex = 0;
        // ControllerModelBlink Ȱ��ȭ
        controller.ControllerModelBlink.SetModelToBlink(blinkIndex);
        controller.ControllerModelBlink.enabled = true;
    }

    public override void Excute(TutorialController controller)
    {
        isDownUp = inputActionAsset.actionMaps[1].actions[2].triggered;
        if (isDownUp == true)
        {
            Debug.Log("�ɱ⴩��");
        }

    }

    public override void Exit(TutorialController controller)
    {

    }
}
