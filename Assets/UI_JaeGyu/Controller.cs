using UnityEngine;

public class ModelBlinking : MonoBehaviour
{
    public GameObject modelToBlink;
    public float blinkSpeed = 5.0f; // �����̴� �ӵ��� �����մϴ�.

    private Material[] modelMaterials; // ���� ���͸����� ������ ����
    private Color originalColor; // ���� ���� ������ ������ ����

    void Start()
    {
        // ���� ��� ���͸����� �����ɴϴ�.
        Renderer[] renderers = modelToBlink.GetComponentsInChildren<Renderer>();
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