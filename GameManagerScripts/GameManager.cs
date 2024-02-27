using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Animator animTheater;
    [SerializeField] GameObject Curtains;
    [SerializeField] ParticleSystem yay1;
    [SerializeField] ParticleSystem yay2;
    float timerCurtain = 1;
    float counterCurtain;
    bool start;
    Scenes currentScene;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(currentScene.ToString(), LoadSceneMode.Additive);
    }

    private void Update()
    {
        if(counterCurtain <= timerCurtain && start == true)
        {
            counterCurtain += Time.deltaTime;
        }
        if (counterCurtain > timerCurtain)
        {
            Curtains.SetActive(false);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (mode == LoadSceneMode.Additive)
        {
            animTheater.SetTrigger("Open");
            start = true;
        }

    }

    public void PlayerDie()
    {
        Curtains.SetActive(true);
        animTheater.SetTrigger("Close");
        StartCoroutine(RetryCorroutine());
    }

    public void NextLevel()
    {
        Curtains.SetActive(true);
        animTheater.SetTrigger("Close");
        StartCoroutine(NextLevelCorroutine());
    }

    IEnumerator RetryCorroutine()
    {
        Curtains.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.UnloadSceneAsync(currentScene.ToString());
        yield return new WaitForSeconds(2);
        animTheater.ResetTrigger("Close");
        SceneManager.LoadScene(currentScene.ToString(),LoadSceneMode.Additive);
        yield return new WaitForSeconds(1);
    }

    IEnumerator NextLevelCorroutine()
    {
        Curtains.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.UnloadSceneAsync(currentScene.ToString());
        switch (currentScene)
        {
            case Scenes.Level1:
                currentScene = Scenes.Level2;
                break;
            case Scenes.Level2:
                currentScene = Scenes.Level3;
                break;
            case Scenes.Level3:
                currentScene = Scenes.EndCredits;
                break;
        }
        animTheater.ResetTrigger("Close");
        SceneManager.LoadScene(currentScene.ToString(), LoadSceneMode.Additive);
        yield return new WaitForSeconds(1);
    }

    public void Yay()
    {
        yay1.Play();
        yay2.Play();
    }
}
