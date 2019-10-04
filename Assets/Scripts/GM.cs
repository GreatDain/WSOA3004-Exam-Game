using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }

        if (health > numberOfGorillas)
        {
            health = numberOfGorillas;
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

    private void Quit()
    {
        Application.Quit();
    }
}
