using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public class GM : MonoBehaviour
{
    public int health;
    public int numberOfGorillas;

    public Image[] gorillas;
    public Sprite fullHP;
    public Sprite emptyHP;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        health = 1;
        
        //pp.vignette.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        var pp = Camera.main.GetComponent<PostProcessingBehaviour>().profile;

        if (Input.GetKeyDown(KeyCode.L) && health < 3)
        {
            health++;
        }


        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine("FadeMenu");
        }*/

        if (health > numberOfGorillas)
        {
            health = numberOfGorillas;
        }

        if (health == 0 || Input.GetKeyDown(KeyCode.R))
        {

            player.GetComponent<FPCharacterController>().setShot();

            StartCoroutine("Fade");

        }

        for (int i = 0; i < gorillas.Length; i++)
        {
            if (i < health)
            {
                gorillas[i].sprite = fullHP;
            }
            else
            {
                gorillas[i].sprite = emptyHP;
            }

            if (i < numberOfGorillas)
            {
                //print(i);
                gorillas[i].enabled = true;
            }
            else
            {
                gorillas[i].enabled = false;
            }
        }
    }

    public void healthInc()
    {
        health++;
    }

    private void Quit()
    {
        Application.Quit();
    }

    public IEnumerator Fade()
    {
        yield return new WaitForSeconds(2f);
        float fadeTime = gameObject.GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(1);
    }

    /*public IEnumerator FadeMenu()
    {
        float fadeTime = gameObject.GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(0);
    }*/
}
