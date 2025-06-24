using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtenClick : MonoBehaviour
{
    public  GameObject gameOverPannel;

    [SerializeField] GameObject pausePannel;
    [SerializeField] GameObject gameFinishPannel;
    [SerializeField] GameObject aboutPannel;
    [SerializeField] GameObject enemyTv;
    [SerializeField] GameObject enemyCactus;
    [SerializeField] GameObject enemyDino;
    [SerializeField] AudioListener audiListener;
    [SerializeField] AudioSource audiSource;
    [SerializeField] AudioClip statSfx;
    [SerializeField] AudioClip aboutSfx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void Pause()
    {
        pausePannel.SetActive(true);
        Time.timeScale = 0;
        enemyTv.SetActive(false);
        enemyCactus.SetActive(false);
        enemyDino.SetActive(false);
    }
    public void Countinue()
    {
        pausePannel.SetActive(false);
        Time.timeScale = 1;
        enemyTv.SetActive(true);
        enemyCactus.SetActive(true);
        enemyDino.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
        pausePannel.SetActive(false);
        gameOverPannel.SetActive(false);
        Time.timeScale = 1;

    }
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);

    }
    public void play()
    {
        audiSource.playOnAwake = true;
        SceneManager.LoadSceneAsync(1);
        Time.timeScale = 1;
    }
    public void Quit()
    {
        Application.Quit();
    }
   
    public void About()
    {
        aboutPannel.SetActive(true);
        audiSource.clip = aboutSfx;
        audiSource.Play();
    }
    public void ClossAbout()
    {
        aboutPannel.SetActive(false);
        audiSource.clip = statSfx;
        audiSource.Play();
    }
    public void Mute()
    {
        audiListener.enabled = false;
    }
    public void UnMute()
    {
        audiListener.enabled = true;
    }

}
