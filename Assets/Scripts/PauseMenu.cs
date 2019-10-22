using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject gm;
    public GameObject QuitCheck;
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
                Resume();
            }
            else
            {
                Cursor.visible = true;
                Pause();
            }
        }
    }

    public void Resume()
    {
        
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadControl()
    {
        Debug.Log("Controls Loading...");
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

    public IEnumerator FadeMenu()
    {
        Time.timeScale = 1f;
        float fadeTime = gm.GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(0);
    }
}
