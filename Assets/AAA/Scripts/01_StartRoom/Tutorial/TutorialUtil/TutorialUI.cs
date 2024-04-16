using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [TextArea(3, 5)]
    [SerializeField] string[] message;

    [SerializeField][ReadOnly] TextMeshProUGUI textUI;

    private void Start()
    {
        textUI = transform.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetTutorialMessage(int currentIndex)
    {
        textUI = transform.GetComponentInChildren<TextMeshProUGUI>();
        textUI.text = message[currentIndex];
    }
}
