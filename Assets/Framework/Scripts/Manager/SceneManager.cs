using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : Singleton<SceneManager>
{
    [SerializeField] Image fade;
    [SerializeField] Slider loadingBar;
    [SerializeField] float fadeTime;
    [SerializeField] GameObject fadeIn;
    [SerializeField] GameObject fadeOut;
    [SerializeField] Graber graber;
    [SerializeField] RightGraber rightGraber;


    private BaseScene curScene;
    private void Start()
    {
       
    }
    private void Update()
    {
        fadeIn = GameObject.Find("ScreenEffect").transform.Find("FadeIn").gameObject;
        fadeOut = GameObject.Find("ScreenEffect").transform.Find("FadeOut").gameObject;
        graber = GameObject.FindObjectOfType<Graber>();
        rightGraber = GameObject.FindObjectOfType<RightGraber>();
    }

    public BaseScene GetCurScene()
    {
        if (curScene == null)
        {
            curScene = FindObjectOfType<BaseScene>();
        }
        return curScene;
    }

    public T GetCurScene<T>() where T : BaseScene
    {
        if (curScene == null)
        {
            curScene = FindObjectOfType<BaseScene>();
        }
        return curScene as T;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadingRoutine(sceneName));
    }

    IEnumerator LoadingRoutine(string sceneName)
    {
        graber.SceneChange = true;
        rightGraber.SceneChange = true;

        fadeIn.gameObject.SetActive(true);
       fade.gameObject.SetActive(true);
       yield return FadeOut();

        Manager.Pool.ClearPool();
        Manager.Sound.StopSFX();
        Manager.UI.ClearPopUpUI();
        Manager.UI.ClearWindowUI();
        Manager.UI.CloseInGameUI();

        Time.timeScale = 0f;
       // loadingBar.gameObject.SetActive(true);

        AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);
        while (oper.isDone == false)
        {
            loadingBar.value = oper.progress;
            yield return null;
        }

        Manager.UI.EnsureEventSystem();

        BaseScene curScene = GetCurScene();
        yield return curScene.LoadingRoutine();

       // loadingBar.gameObject.SetActive(false);
        Time.timeScale = 1f;

        if (rightGraber.DirectInteractable != null)
        {
            rightGraber.Manager.SelectEnter(rightGraber.DirectInteractor, rightGraber.DirectInteractable);
        }
        if (graber.DirectInteractable != null)
        {
            graber.Manager.SelectEnter(graber.DirectInteractor, graber.DirectInteractable);
        }



        fadeOut.gameObject.SetActive(true);




        yield return FadeIn();
        fade.gameObject.SetActive(false);
        yield return null;

        

       

        graber.SceneChange = false;
        rightGraber.SceneChange = false;
    }

    IEnumerator FadeOut()
    {
        float rate = 0;
        Color fadeOutColor = new Color(fade.color.r, fade.color.g, fade.color.b, 1f);
        Color fadeInColor = new Color(fade.color.r, fade.color.g, fade.color.b, 0f);

        while (rate <= 1)
        {
            rate += Time.deltaTime / fadeTime;
            fade.color = Color.Lerp(fadeInColor, fadeOutColor, rate);
            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        float rate = 0;
        Color fadeOutColor = new Color(fade.color.r, fade.color.g, fade.color.b, 1f);
        Color fadeInColor = new Color(fade.color.r, fade.color.g, fade.color.b, 0f);

        while (rate <= 1)
        {
            rate += Time.deltaTime / fadeTime;
            fade.color = Color.Lerp(fadeOutColor, fadeInColor, rate);
            yield return null;
        }
    }
}
