using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    public int health;
    public int numberOfGorillas;

    public Image[] gorillas;
    public Sprite fullHP;
    public Sprite emptyHP;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        health = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && health < 3)
        {
            health++;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine("FadeMenu");
        }

        if (health > numberOfGorillas)
        {
            health = numberOfGorillas;
        }

        if (health == 0 || Input.GetKeyDown(KeyCode.R))
        {
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
        float fadeTime = gameObject.GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(1);
    }

    public IEnumerator FadeMenu()
    {
        float fadeTime = gameObject.GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(0);
    }
}
