public class TutorialTrigger : TutorialBase
{
    public override void Enter(TutorialController controller)
    {
        // �޼��� ����
        base.Enter(controller);

        blinkIndex = 3;
        // ControllerModelBlink Ȱ��ȭ
        controller.ControllerModelBlink.SetModelToBlink(blinkIndex);
        controller.ControllerModelBlink.enabled = true;

        // target ȭ��ǥ Ȱ��ȭ
        controller.TargetArrow.SetActive(true);
    }

    public override void Excute(TutorialController controller)
    {


    }

    public override void Exit(TutorialController controller)
    {
    }
}
