using UnityEngine;

public class ControllerModelBlink : MonoBehaviour
{
    /*
     modelToBlink[0] : Left ThumbStick
     modelToBlink[1] : Right ThumbStick
     modelToBlink[2] : Right Grip
     modelToBlink[3] : Right Trigger
     modelToBlink[4] : Right A Button
     modelToBlink[5] : Left Grip
     */
    public GameObject[] modelToBlink;
    [SerializeField][ReadOnly] GameObject currentModelToBlink;
    [SerializeField] float blinkSpeed; // �����̴� �ӵ��� �����մϴ�.

    private Material[] modelMaterials; // ���� ���͸����� ������ ����
    private Color originalColor; // ���� ���� ������ ������ ����

    public void SetModelToBlink(int index)
    {
        if (currentModelToBlink != null)
            NullModelToBlink();

        currentModelToBlink = modelToBlink[index];
    }

    public void NullModelToBlink()
    {
        modelMaterials[0].color = originalColor;
        currentModelToBlink = null;
    }

    void OnEnable()
    {
        // ���� ��� ���͸����� �����ɴϴ�.
        Renderer[] renderers = currentModelToBlink.GetComponentsInChildren<Renderer>();
        modelMaterials = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            modelMaterials[i] = renderers[i].material;
        }

        // ���� ���� ������ �����մϴ�.
        originalColor = modelMaterials[0].color;
    }

    void Update()
    {
        // ���������� �����̴� ȿ���� �����մϴ�.
        float blinkAlpha = Mathf.Abs(Mathf.Sin(Time.time * blinkSpeed));
        Color blinkColor = Color.red * blinkAlpha;

        // ���� ��� ���͸��� ������ �����Ͽ� �����̴� ȿ���� �����մϴ�.
        foreach (Material mat in modelMaterials)
        {
            mat.color = originalColor + blinkColor;
        }
    }
}
