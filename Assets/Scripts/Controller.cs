using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public void Load(string loadSceneName)
    {
        //LoadingController loadingController = gameObject.AddComponent<LoadingController>();
        //loadingController.LoadScene(loadSceneName);
        LoadingController.LoadNew(loadSceneName);

    }
}
