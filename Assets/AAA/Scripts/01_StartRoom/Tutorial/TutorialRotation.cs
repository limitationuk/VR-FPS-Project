using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialRotation : TutorialBase
{
    [SerializeField] InputActionAsset inputActionAsset;
    [SerializeField] bool isRotate;
    [SerializeField] int rotationCount = 0;

    public override void Enter(TutorialController controller)
    {
        // �޼��� ����
        base.Enter(controller);

        blinkIndex = 1;
        // ControllerModelBlink Ȱ��ȭ
        controller.ControllerModelBlink.SetModelToBlink(blinkIndex);
        controller.ControllerModelBlink.enabled = true;
    }

    public override void Excute(TutorialController controller)
    {
        isRotate = inputActionAsset.actionMaps[6].actions[7].triggered;
        if (isRotate == true)
        {
            rotationCount++;
        }
        if (rotationCount >= 2)
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
