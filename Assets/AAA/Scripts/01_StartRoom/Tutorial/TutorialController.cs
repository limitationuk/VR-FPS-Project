using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] List<TutorialBase> tutorials;
    [SerializeField] TutorialBase currentTutorial;
    [SerializeField] int currentIndex;

    [SerializeField] ControllerModelBlink controllerModelBlink;
    [SerializeField] TutorialUI tutorialUI;
    [SerializeField] GameObject[] marks;
    [SerializeField] GameObject targetArrow;

    public int GetIndex() { return currentIndex; }
    public int Index => GetIndex();

    public ControllerModelBlink GetControllerModelBlink() { return controllerModelBlink; }
    public ControllerModelBlink ControllerModelBlink => GetControllerModelBlink();

    public TutorialUI GetTutorialUI() { return tutorialUI; }
    public TutorialUI TutorialUI => GetTutorialUI();

    public GameObject[] GetMarks() { return marks; }
    public GameObject[] Marks => GetMarks();

    public GameObject GetTargetArrow() { return targetArrow; }
    public GameObject TargetArrow => GetTargetArrow();


    private void Start()
    {
        // TutorialController ������Ʈ�� XR Origin - Main Camera �ڽ����� �ֱ�
        GameObject originCamera = GameObject.FindWithTag("MainCamera");
        transform.SetParent(originCamera.transform);

        // ControllerModelBlink ��Ȱ��ȭ
        controllerModelBlink = gameObject.GetComponentInChildren<ControllerModelBlink>();
        controllerModelBlink.enabled = false;

        // Mark ǥ�� ��Ȱ��ȭ
        for (int i = 0; i < marks.Length; i++)
        {
            marks[i].SetActive(false);
        }

        // target ȭ��ǥ ��Ȱ��ȭ
        targetArrow.SetActive(false);

        currentIndex = -1;

        // Ʃ�丮�� ����
        SetNextTutorial();
    }

    private void Update()
    {
        if (currentTutorial != null)
        {
            currentTutorial.Excute(this);
        }
    }

    public void SetNextTutorial()
    {
        // ���� Ʃ�丮���� Exit() �޼ҵ� ȣ��
        if (currentTutorial != null)
        {
            Debug.Log($"currentTutorial.Exit({currentIndex})");
            currentTutorial.Exit(this);
        }

        // ������ Ʃ�丮���� �����ߴٸ� CompletedAllTutorials() �޼ҵ� ȣ��
        if (currentIndex >= tutorials.Count - 1)
        {
            Debug.Log("CompletedAllTutorials()");
            CompletedAllTutorials();
            return;
        }

        // ���� Ʃ�丮���� currentTutorial�� ���
        Debug.Log($"currentIndex(old): {currentIndex}");
        currentIndex++;
        currentTutorial = tutorials[currentIndex];
        Debug.Log($"currentIndex(new): {currentIndex}");

        // TutoriaUI ������Ʈ�� ���ο� Ʃ�丮���� �ڽ����� �ֱ�
        tutorialUI.transform.SetParent(currentTutorial.transform);

        // ���ο� Ʃ�丮���� Enter() �޼ҵ� ȣ��
        currentTutorial.Enter(this);
    }

    private void CompletedAllTutorials()
    {
        //currentTutorial = null;

        // �ൿ ����� ���� ������ �Ǿ��� �� �ڵ� �߰� �ۼ�
        gameObject.SetActive(false);

        Debug.Log("Complete All");
    }
}
