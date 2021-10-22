using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    private const string loadingScreenPrefabPath = "Prefabs/Loading Screen";

    private static string loadSceneName;
    private static GameObject loadingScreenPrefab;
    
    private AsyncOperation asyncLoad;

    public Image progressBar;
    public Text currentProgressText;
    public Text currentStateText;

    public static void Load(string SceneName)
    {
        loadingScreenPrefab = Resources.Load<GameObject>(loadingScreenPrefabPath);
        Instantiate(loadingScreenPrefab);
        loadSceneName = SceneName;

    }

    private void Start()
    {
        SetupAndLoad();
    }

    private void SetupAndLoad()
    {
        SetupUI();

        StartCoroutine(LoadScene());
    }

    private void SetupUI()
    {
        progressBar.fillAmount = 0f;
        currentProgressText.text = "0%";
        currentStateText.text = LoadingStatesText.loading;
    }

    private IEnumerator LoadScene()
    {
        asyncLoad = CreateOperation();

        // Emulate loading
        yield return new WaitForSeconds(2); 

        while (!asyncLoad.isDone)
        {
            float loadingProgress = asyncLoad.progress / 0.9f;
            progressBar.fillAmount = loadingProgress;
            int intProgress = (int)loadingProgress * 100;
            currentProgressText.text =intProgress.ToString() + "%";

            isLoaded();

            yield return null;
        }
    }

    private AsyncOperation CreateOperation()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(loadSceneName);
        asyncLoad.allowSceneActivation = false;
        return asyncLoad;
    }

    private void isLoaded()
    {
        if (asyncLoad.progress >= .9f)
        {
            currentStateText.text = LoadingStatesText.press_any_key;
            isUserTapOnScreen();
        }
    }

    private void isUserTapOnScreen()
    {
        if (Input.anyKeyDown)
        {
            asyncLoad.allowSceneActivation = true;
            Destroy(gameObject);
        }
    }
}

class LoadingStatesText
{
    public static string loading = "Loading...";
    public static string press_any_key = "Press Any Key";
}