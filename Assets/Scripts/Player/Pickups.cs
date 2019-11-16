using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Pickups : MonoBehaviour
{
    public Text interact;
    public GameObject gm;
    public GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        interact.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Allows interaction with food objects within a certain distance of it.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && gm.GetComponent<NewTut>().counter >= 2)
        {
            interact.enabled = true;
            /*Checks how much health the player. If some is missing, the interaction is allowed, if full, prevents the player
             from interacting with it.*/
            if (Input.GetKeyDown(KeyCode.E) && gm.GetComponent<GM>().health != 3f)
            {
                player.GetComponent<FPCharacterController>().Banana();
                gm.GetComponent<GM>().healthInc();
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
