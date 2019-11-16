using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinCheck : MonoBehaviour
{

    public Text winText;

    // Start is called before the first frame update

    void Start()
    {
        winText.enabled = false;
    }

    // Update is called once per frame

    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            print("WEEENER");
            StartCoroutine(delayReturn());
        }
    }

    IEnumerator delayReturn()
    {

        winText.enabled = true;

        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(0);
    }
}
