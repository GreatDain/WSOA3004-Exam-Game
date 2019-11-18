using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI, tipsUI;
    public GameObject gm;
    public GameObject QuitCheck;
    public GameObject Controls;
    public GameObject helpfulTips;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (GameIsPaused)
            {
                Cursor.visible = false;
                player.GetComponent<MouseLook>().enabled = true;
                Resume();
            }
            else
            {
                Cursor.visible = true;
                player.GetComponent<MouseLook>().enabled = false;
                Pause();
                
            }
        }
    }

    public void Resume()
    {
        
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        player.GetComponent<MouseLook>().enabled = true;
        GameIsPaused = false;
        tipsUI.SetActive(true);
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        tipsUI.SetActive(false);
    }

    public void LoadControl()
    {
        Debug.Log("Controls Loading...");
        pauseMenuUI.SetActive(false);
        Controls.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game ...");
        pauseMenuUI.SetActive(false);
        QuitCheck.SetActive(true);
    }

    public void NoQuit()
    {
        QuitCheck.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void YesQuit()
    {
        StartCoroutine("FadeMenu");
    }

    public void ReturnControls()
    {
        pauseMenuUI.SetActive(true);
        Controls.SetActive(false);
    }

    public void Tips()
    {
        Controls.SetActive(false);
        helpfulTips.SetActive(true);
    }

    public void ReturnTips()
    {
        Controls.SetActive(true);
        helpfulTips.SetActive(false);
    }

    public IEnumerator FadeMenu()
    {
        Time.timeScale = 1f;
        float fadeTime = gm.GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(0);
    }
}
