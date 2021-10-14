using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public void Load()
    {
        LoadingController loadingController = gameObject.AddComponent<LoadingController>();
        loadingController.LoadScene("UI Scene");

    }
}
