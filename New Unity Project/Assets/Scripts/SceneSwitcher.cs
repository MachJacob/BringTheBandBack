using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    FMOD.Studio.EventInstance Loadsound;
    FMOD.Studio.EventInstance Quitsound;
    
    public void LoadScene(string sceneName)
    {
        Loadsound = FMODUnity.RuntimeManager.CreateInstance("event:/UI/Play");
        Loadsound.start();
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        FMOD.Studio.PLAYBACK_STATE state;
        Quitsound = FMODUnity.RuntimeManager.CreateInstance("event:/Player/Jump");
        Quitsound.start();
        Quitsound.getPlaybackState(out state);
        Application.Quit();
    }
}
