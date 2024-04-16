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
        // TutorialController 오브젝트를 XR Origin - Main Camera 자식으로 넣기
        GameObject originCamera = GameObject.FindWithTag("MainCamera");
        transform.SetParent(originCamera.transform);

        // ControllerModelBlink 비활성화
        controllerModelBlink = gameObject.GetComponentInChildren<ControllerModelBlink>();
        controllerModelBlink.enabled = false;

        // Mark 표시 비활성화
        for (int i = 0; i < marks.Length; i++)
        {
            marks[i].SetActive(false);
        }

        // target 화살표 비활성화
        targetArrow.SetActive(false);

        currentIndex = -1;

        // 튜토리얼 진행
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
        // 현재 튜토리얼의 Exit() 메소드 호출
        if (currentTutorial != null)
        {
            Debug.Log($"currentTutorial.Exit({currentIndex})");
            currentTutorial.Exit(this);
        }

        // 마지막 튜토리얼을 진행했다면 CompletedAllTutorials() 메소드 호출
        if (currentIndex >= tutorials.Count - 1)
        {
            Debug.Log("CompletedAllTutorials()");
            CompletedAllTutorials();
            return;
        }

        // 다음 튜토리얼을 currentTutorial로 등록
        Debug.Log($"currentIndex(old): {currentIndex}");
        currentIndex++;
        currentTutorial = tutorials[currentIndex];
        Debug.Log($"currentIndex(new): {currentIndex}");

        // TutoriaUI 오브젝트를 새로운 튜토리얼의 자식으로 넣기
        tutorialUI.transform.SetParent(currentTutorial.transform);

        // 새로운 튜토리얼의 Enter() 메소드 호출
        currentTutorial.Enter(this);
    }

    private void CompletedAllTutorials()
    {
        //currentTutorial = null;

        // 행동 양식이 여러 종류가 되었을 때 코드 추가 작성
        gameObject.SetActive(false);

        Debug.Log("Complete All");
    }
}
