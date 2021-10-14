using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    private const string loadingScreenPrefabPath = "Prefabs/Loading Screen";

    private string loadSceneName;
    private GameObject loadingScreenPrefab;
    private Slider progressBar;
    private Text currentStateText;
    private AsyncOperation asyncLoad;

    private void LoadingControllerConstructor(string loadSceneName)
    {
        this.loadSceneName = loadSceneName;
        loadingScreenPrefab = FindLoadingScreenPrefab();
    }

    private void LoadingControllerConstructor(string loadSceneName, GameObject loadingScreenPrefab)
    {
        this.loadSceneName = loadSceneName;
        this.loadingScreenPrefab = loadingScreenPrefab;
    }

    private GameObject FindLoadingScreenPrefab()
    {
        return Resources.Load<GameObject>(loadingScreenPrefabPath);
    }

    private void SetupUI(GameObject _gameObject)
    {
        progressBar = _gameObject.GetComponentInChildren<Slider>();
        currentStateText = _gameObject.GetComponentInChildren<Text>();
        progressBar.value = 0f;
        currentStateText.text = LoadingStatesText.loading;
    }

    public void LoadScene(string loadSceneName)
    {
        LoadingControllerConstructor(loadSceneName);
        Load();
    }

    public void LoadScene(string loadSceneName, GameObject loadingScreenPrefab)
    {
        LoadingControllerConstructor(loadSceneName, loadingScreenPrefab);
        Load();
    }

    private void Load()
    {
        GameObject loadingScreen = Instantiate(loadingScreenPrefab);
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
            progressBar.value = asyncLoad.progress;

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