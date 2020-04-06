using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    private bool transitionEnded = false;
    public Animator transition;
    public float transitionTime = 1f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            LoadTown();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadTown()
    {
        StartCoroutine(StartTransition());
        StartCoroutine(LoadLevel(0));
    }
    IEnumerator StartTransition()
    {
        transition.SetTrigger("Start");
        transitionEnded = true;
        yield return new WaitForSeconds(transitionTime);
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        if (transitionEnded)
        {
            AsyncOperation scene = SceneManager.LoadSceneAsync(levelIndex);

            while (scene.progress < 1)
            {
                //Fill loading bar
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            yield return new WaitForEndOfFrame();
        }
    }
}
