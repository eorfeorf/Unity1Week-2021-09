using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneSequencerBase : MonoBehaviour, ISceneSequencer
{
    [SerializeField] protected string nextScene;

    protected ScreenFader fader;

    private void Awake()
    {
        //fader = gameObject.AddComponent<ScreenFader>();
        fader = (ScreenFader)FindObjectOfType(typeof(ScreenFader));   
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneAsync(string sceneName)
    {
        //SceneManager.LoadSceneAsync(sceneName);
    }
}
