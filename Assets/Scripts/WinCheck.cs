using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCheck : MonoBehaviour
{

    public GameObject winText;

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("WEEENER");
            StartCoroutine(delayReturn());
        }
    }

    IEnumerator delayReturn()
    {

        winText.SetActive(true);

        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(0);
    }
}
