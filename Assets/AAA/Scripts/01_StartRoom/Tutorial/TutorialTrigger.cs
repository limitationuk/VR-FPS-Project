public class TutorialTrigger : TutorialBase
{
    public override void Enter(TutorialController controller)
    {
        // �޼��� ����
        base.Enter(controller);

        blinkIndex = 3;
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
