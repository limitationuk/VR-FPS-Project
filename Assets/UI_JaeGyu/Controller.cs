using UnityEngine;

public class ModelBlinking : MonoBehaviour
{
    public GameObject modelToBlink;
    public float blinkSpeed = 5.0f; // 깜빡이는 속도를 조절합니다.

    private Material[] modelMaterials; // 모델의 머터리얼을 저장할 변수
    private Color originalColor; // 모델의 원래 색상을 저장할 변수

    void Start()
    {
        // 모델의 모든 머터리얼을 가져옵니다.
        Renderer[] renderers = modelToBlink.GetComponentsInChildren<Renderer>();
        modelMaterials = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            modelMaterials[i] = renderers[i].material;
        }

        // 모델의 원래 색상을 저장합니다.
        originalColor = modelMaterials[0].color;
    }

    void Update()
    {
        // 빨간색으로 깜빡이는 효과를 구현합니다.
        float blinkAlpha = Mathf.Abs(Mathf.Sin(Time.time * blinkSpeed));
        Color blinkColor = Color.red * blinkAlpha;

        // 모델의 모든 머터리얼에 색상을 적용하여 깜빡이는 효과를 구현합니다.
        foreach (Material mat in modelMaterials)
        {
            mat.color = originalColor + blinkColor;
        }
    }
}