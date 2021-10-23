using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLoad : MonoBehaviour
{
    private void Start()
    {
        LoadingController.Load(1);
    }
}
