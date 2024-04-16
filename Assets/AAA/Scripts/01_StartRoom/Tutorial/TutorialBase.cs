using UnityEngine;

public abstract class TutorialBase : MonoBehaviour
{
    public int blinkIndex;
    public int marksIndex;

    // �ش� Ʃ�丮�� ������ ������ �� 1ȸ ȣ��
    public virtual void Enter(TutorialController controller)
    {
        Debug.Log($"Base - Enter {controller.Index} ����");

        // � �޼��� ����� ����
        controller.TutorialUI.SetTutorialMessage(controller.Index);
    }

    // �ش� Ʃ�丮�� ������ �����ϴ� ���� �� ������ ȣ��
    public virtual void Excute(TutorialController controller)
    {
    }

    // �ش� Ʃ�丮�� ������ ������ �� 1ȸ ȣ��
    public virtual void Exit(TutorialController controller)
    {

    }
}
