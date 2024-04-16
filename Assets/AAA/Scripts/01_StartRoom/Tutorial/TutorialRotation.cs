using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialRotation : TutorialBase
{
    [SerializeField] InputActionAsset inputActionAsset;
    [SerializeField][ReadOnly] bool isRotate;
    [SerializeField][ReadOnly] int rotationCount = 0;

    public override void Enter(TutorialController controller)
    {
        // 메세지 띄우기
        base.Enter(controller);

        blinkIndex = 1;
        // modelToBlink 할당, ControllerModelBlink 활성화
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
        if (rotationCount >= 4)
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
