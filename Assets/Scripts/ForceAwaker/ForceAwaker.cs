using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Code from Niklas Borchers

public class ForceAwaker : MonoBehaviour
{
    private void Awake()
    {
        List<IShouldForceAwake> scripts = new List<IShouldForceAwake>();
        Scene scene = SceneManager.GetActiveScene();

        GameObject[] rootObjects = scene.GetRootGameObjects();

        foreach (GameObject go in rootObjects)
            scripts.AddRange(go.GetComponentsInChildren<IShouldForceAwake>(true));
        foreach (IShouldForceAwake script in scripts)
            script.ForceAwake();
    }
}
