using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Pickups : MonoBehaviour
{
    public Text interact;
    public GameObject gm;
    // Start is called before the first frame update
    void Start()
    {
        interact.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interact.enabled = true;

            if (Input.GetKeyDown(KeyCode.E) && gm.GetComponent<GM>().health < 3f)
            {
                gm.GetComponent<GM>().health += 1;
                Destroy(gameObject);
                interact.enabled = false;
            }
            else if (Input.GetKeyDown(KeyCode.E) && gm.GetComponent<GM>().health == 3f)
            {
                StartCoroutine("FullHealth");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && interact.enabled == true)
        {
            interact.enabled = false;
        }
    }

    IEnumerator FullHealth()
    {
        interact.text = "Health Full";
        yield return new WaitForSeconds(2f);
        interact.text = "Press E to eat banana";
    }
}
