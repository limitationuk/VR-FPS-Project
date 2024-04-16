public class TutorialTrigger : TutorialBase
{
    public override void Enter(TutorialController controller)
    {
        // 메세지 띄우기
        base.Enter(controller);

        blinkIndex = 3;
        // ControllerModelBlink 활성화
        controller.ControllerModelBlink.SetModelToBlink(blinkIndex);
        controller.ControllerModelBlink.enabled = true;

        // target 화살표 활성화
        controller.TargetArrow.SetActive(true);
    }

    public override void Excute(TutorialController controller)
    {


    }

    public override void Exit(TutorialController controller)
    {
    }
}
