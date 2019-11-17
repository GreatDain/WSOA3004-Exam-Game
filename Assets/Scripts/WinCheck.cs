using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinCheck : MonoBehaviour
{
    public Text winText;
    public GameObject promptTicket;
    public GameObject winConfetti;
    public Transform confettiSpawnPoint;

    // Start is called before the first frame update

    void Start()
    {
        winText.enabled = false;
        promptTicket.SetActive(false);
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
        promptTicket.SetActive(true);
        Instantiate(winConfetti, confettiSpawnPoint.position, Quaternion.identity);

        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(0);
    }
}
