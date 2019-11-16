using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Tutorial : MonoBehaviour
{
    public Text objective1;
    public Text objective2;
    public Text foodObjective;
    public Text constantObjText;
    public Text sprintUnlocked;
    public Text sneakUnlocked;
    public Text howToSprint;
    public Text howToSneak;
    public Text gateObjective;
    public Text escapeObjective;
    public Canvas objectivesTab;
    public GameObject ticket;
    GameObject FPS;
    public GameObject prefab;
    GameObject oldPrefab;
    //public GameObject banana;
    public bool sprintAbility = false;
    public bool sneakAbility = false;
    public int counter;
    // Start is called before the first frame update

    void Start()
    {
        FPS = GameObject.FindGameObjectWithTag("Player");
        //objectivesTab.enabled = true;
        StartCoroutine("Objectives");
        objective1.enabled = true;
        objective2.enabled = false;
        foodObjective.enabled = false;
        gateObjective.enabled = false;
        escapeObjective.enabled = false;


        counter = 0;
        constantObjText.enabled = true;

        sprintUnlocked.enabled = false;
        howToSprint.enabled = false;
        sneakUnlocked.enabled = false;
        howToSneak.enabled = false;

        //banana.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (objectivesTab.enabled == true)
        {
            ticket.SetActive(true);
        }
        else if (objectivesTab.enabled == false)
        {
            ticket.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            sprintAbility = true;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            sneakAbility = true;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            counter++;
        }

        if (Input.GetKeyDown(KeyCode.Tab) && objectivesTab.enabled == true)
        {
            objectivesTab.enabled = false;
            StopCoroutine("Objectives");
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && objectivesTab.enabled == false)
        {
            StartCoroutine("Objectives");
        }

        if (FPS.GetComponent<FPCharacterController>().cageOpen == true && counter == 0)
        {
            ObjectiveOne();
        }

        else if (prefab.GetComponent<PlayerWaypoint>().checkpoint == true && counter == 1f)
        {
            ObjectiveThree();
        }

        else if (prefab.GetComponent<PlayerWaypoint>().checkpoint == true && counter == 2f)
        {
            ObjectiveFour();
        }

        else if (prefab.GetComponent<PlayerWaypoint>().checkpoint == true && counter == 3f)
        {
            ObjectiveFive();
        }

        else if (prefab.GetComponent<PlayerWaypoint>().checkpoint == true && counter == 4f)
        {
            ObjectiveSix();
        }

        else if (prefab.GetComponent<PlayerWaypoint>().checkpoint == true && counter == 5f)
        {
            ObjectiveSeven();
        }

        /*else if (prefab.GetComponent<PlayerWaypoint>().checkpoint == true && counter == 6f)
        {
            ObjectiveEight();
        }*/
    }

    public void ObjectiveOne()
    {
        //Bring up text for objective one

        //check failure conditions
        objective1.enabled = false;
        FPS.GetComponent<FPCharacterController>().cageOpen = false;
        Debug.Log("Obj 2");
        ObjectiveTwo();
    }

    public void ObjectiveTwo()
    {
        prefab = Instantiate(prefab, new Vector3(-353f, 0, 132f), transform.rotation);
        //sprintAbility = true;
        counter++;
    }

    public void ObjectiveThree()
    {
        StartCoroutine("Objectives");
        Debug.Log("Obj 3");
        oldPrefab = prefab;
        prefab.GetComponent<PlayerWaypoint>().checkpoint = false;
        prefab = Instantiate(prefab, new Vector3(-315.2f, 0, -197.7f), transform.rotation);
        Destroy(GameObject.Find("WaypointMarker(Clone)"));
        Destroy(oldPrefab);
        //StartCoroutine("Sprint");
        //sprintAbility = true;
        StartCoroutine("Sneak");
        sneakAbility = true;
        objective2.enabled = true;
        counter++;
    }

    /*public void ObjectiveFour()
    {
        //StartCoroutine("Objectives");
        oldPrefab = prefab;
        prefab.GetComponent<PlayerWaypoint>().checkpoint = false;
        prefab = Instantiate(prefab, new Vector3(-301.6f, 0, -302.4f), transform.rotation);
        Destroy(GameObject.Find("WaypointMarker(Clone)"));
        Destroy(oldPrefab);
        counter++;
    }*/

    public void ObjectiveFour()
    {
        StartCoroutine("Objectives");
        oldPrefab = prefab;
        prefab.GetComponent<PlayerWaypoint>().checkpoint = false;
        prefab = Instantiate(prefab, new Vector3(-132f, 0, -298f), transform.rotation);
        Destroy(GameObject.Find("WaypointMarker(Clone)"));
        Destroy(oldPrefab);
        //StartCoroutine("Sneak");
        //sneakAbility = true;
        StartCoroutine("Sprint");
        sprintAbility = true;
        counter++;
    }

    public void ObjectiveFive()
    {
        //banana.SetActive(true);
        StartCoroutine("Objectives");
        objective2.enabled = false;
        foodObjective.enabled = true;
        oldPrefab = prefab;
        prefab.GetComponent<PlayerWaypoint>().checkpoint = false;
        prefab = Instantiate(prefab, new Vector3(-59.8f, 3.9f, -155.6f), transform.rotation);
        Destroy(GameObject.Find("WaypointMarker(Clone)"));
        Destroy(oldPrefab);
        counter++;
    }

    public void ObjectiveSix()
    {
        StartCoroutine("Objectives");
        foodObjective.enabled = false;
        gateObjective.enabled = true;
        oldPrefab = prefab;
        prefab.GetComponent<PlayerWaypoint>().checkpoint = false;
        prefab = Instantiate(prefab, new Vector3(20.8f, 3.9f, -155.6f), transform.rotation);
        Destroy(GameObject.Find("WaypointMarker(Clone)"));
        Destroy(oldPrefab);
        counter++;
    }

    public void ObjectiveSeven()
    {
        StartCoroutine("objectives");
        gateObjective.enabled = false;
        escapeObjective.enabled = true;
        oldPrefab = prefab;
        prefab.GetComponent<PlayerWaypoint>().checkpoint = false;
        Destroy(GameObject.Find("WaypointMarker(Clone)"));
        Destroy(oldPrefab);
        counter++;
    }

    private IEnumerator Objectives()
    {
        objectivesTab.enabled = true;
        yield return new WaitForSeconds(10f);
        objectivesTab.enabled = false;
    }

    private IEnumerator Sprint()
    {
        sprintUnlocked.enabled = true;
        yield return new WaitForSeconds(1.5f);
        sprintUnlocked.enabled = false;
        howToSprint.enabled = true;
        yield return new WaitForSeconds(3f);
        howToSprint.enabled = false;
    }

    private IEnumerator Sneak()
    {
        sneakUnlocked.enabled = true;
        yield return new WaitForSeconds(1.5f);
        sneakUnlocked.enabled = false;
        howToSneak.enabled = true;
        yield return new WaitForSeconds(3f);
        howToSneak.enabled = false;
    }
}
