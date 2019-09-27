using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventHandler : MonoBehaviour {

    public void OpenUrl (string Url){
        Application.OpenURL(Url);
    }

    public void OpenScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}

