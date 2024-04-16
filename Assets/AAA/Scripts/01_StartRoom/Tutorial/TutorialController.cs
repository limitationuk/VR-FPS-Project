using System.Collections.Generic;
using UnityEngine;


public class TutorialController : MonoBehaviour
{
    [SerializeField] List<TutorialBase> tutorials;
    [SerializeField][ReadOnly] TutorialBase currentTutorial;
    [SerializeField][ReadOnly] int currentIndex;

    [SerializeField] ControllerModelBlink controllerModelBlink;
    [SerializeField] TutorialUI tutorialUI;
    [SerializeField] StartRoomGun startRoomGun;
    [SerializeField] GameObject mark;
    [SerializeField][ReadOnly] GameObject[] marks;

    public int GetIndex() { return currentIndex; }
    public int Index => GetIndex();

    public ControllerModelBlink GetControllerModelBlink() { return controllerModelBlink; }
    public ControllerModelBlink ControllerModelBlink => GetControllerModelBlink();

    public TutorialUI GetTutorialUI() { return tutorialUI; }
    public TutorialUI TutorialUI => GetTutorialUI();

    public StartRoomGun GetStartRoomGun() { return startRoomGun; }
    public StartRoomGun StartRoomGun => GetStartRoomGun();
    
    public GameObject[] GetMarks() { return marks; }
    public GameObject[] Marks => GetMarks();


    private void Start()
    {
        // TutorialController ������Ʈ�� XR Origin - Main Camera �ڽ����� �ֱ�
        GameObject originCamera = GameObject.FindWithTag("MainCamera");
        transform.SetParent(originCamera.transform);

        // ControllerModelBlink ��Ȱ��ȭ
        //controllerModelBlink = gameObject.GetComponentInChildren<ControllerModelBlink>();
        controllerModelBlink.enabled = false;

        // StartRoomGun ��Ȱ��ȭ
        startRoomGun.gameObject.SetActive(false);

        // Mark ǥ�� ��Ȱ��ȭ
        marks = mark.GetComponentsInChildren<GameObject>();
        for (int i = 0; i < marks.Length; i++)
        {
            marks[i].SetActive(false);
        }

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
        currentTutorial = null;

        // �ൿ ����� ���� ������ �Ǿ��� �� �ڵ� �߰� �ۼ�

        Debug.Log("Complete All");
    }
}
