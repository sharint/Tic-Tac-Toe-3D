using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    private const string loadingScreenPrefabPath = "Prefabs/Loading Screen";

    private static string loadSceneName;
    private static GameObject loadingScreenPrefab;
    private static GameObject loadingScreen;
    
    private AsyncOperation asyncLoad;

    public Image progressBar;
    public Text currentProgressText;
    public Text currentStateText;

    private void Start()
    {
        Load();
    }

    public static void LoadNew(string SceneName)
    {
        loadingScreenPrefab = Resources.Load<GameObject>(loadingScreenPrefabPath);
        loadingScreen = Instantiate(loadingScreenPrefab);
        loadSceneName = SceneName;
    }

    private void SetupUI(GameObject _gameObject)
    {
        progressBar.fillAmount = 0f;
        currentProgressText.text = "0%";
        currentStateText.text = LoadingStatesText.loading;
    }

    private void Load()
    {
        //GameObject loadingScreen = Instantiate(loadingScreenPrefab);
        SetupUI(loadingScreen);

        StartCoroutine(LoadScene());
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
            //currentProgressText.text = (int)loadingProgress + "%";

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