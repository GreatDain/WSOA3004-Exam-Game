using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class NewTut : MonoBehaviour
{
    public Text objective1;
    public Text objective2;
    public Text foodObjective;
    public Text constantObjText;
    public Text sprintUnlocked;
    public Text sneakUnlocked;
    public Text howToSprint;
    public Text howToSneak;
    public Text escapeObjective;
    public Canvas objectivesTab;
    public GameObject ticket;
    GameObject FPS;
    public GameObject prefabR;
    public GameObject prefabL;
    GameObject oldPrefabR;
    GameObject oldPrefabL;
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
        escapeObjective.enabled = false;


        counter = 0;
        constantObjText.enabled = true;

        sprintUnlocked.enabled = false;
        howToSprint.enabled = false;
        sneakUnlocked.enabled = false;
        howToSneak.enabled = false;
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
        else if ((prefabR.GetComponent<PlayerWaypoint>().checkpoint == true || prefabL.GetComponent<PlayerWaypoint>().checkpoint == true) && counter == 1f)
        {
            ObjectiveTwo();
        }
        else if ((prefabR.GetComponent<PlayerWaypoint>().checkpoint == true || prefabL.GetComponent<PlayerWaypoint>().checkpoint == true) && counter == 2f)
        {
            ObjectiveThree();
        }
        else if (prefabR.GetComponent<PlayerWaypoint>().checkpoint == true && counter == 3f)
        {
            ObjectiveFour();
        }
    }

    public void ObjectiveOne()
    {
        //Bring up text for objective one

        //check failure conditions
        objective1.enabled = false;
        FPS.GetComponent<FPCharacterController>().cageOpen = false;
        prefabR = Instantiate(prefabR, new Vector3(-2451.038f, 0, -300.421f), transform.rotation);
        prefabL = Instantiate(prefabL, new Vector3(-2224.038f, 0, -147.4215f), transform.rotation);
        Debug.Log("Obj 2");
        counter++;
    }

    public void ObjectiveTwo()
    {
        StartCoroutine("Objectives");
        Debug.Log("Obj 3");
        oldPrefabR = prefabR;
        prefabR.GetComponent<PlayerWaypoint>().checkpoint = false;
        prefabR = Instantiate(prefabR, new Vector3(-2452.038f, 0, -549.4215f), transform.rotation);
        Destroy(oldPrefabR);
        oldPrefabL = prefabL;
        prefabL.GetComponent<PlayerWaypoint>().checkpoint = false;
        prefabL = Instantiate(prefabL, new Vector3(-2003.038f, 0, -182.4215f), transform.rotation);
        Destroy(oldPrefabL);
        Destroy(GameObject.Find("WaypointMarker(Clone)"));
        StartCoroutine("Sneak");
        sneakAbility = true;
        objective2.enabled = true;
        counter++;
    }

    public void ObjectiveThree()
    {
        Debug.Log("Obj 3");
        objective2.enabled = false;
        StartCoroutine("Objectives");
        oldPrefabR = prefabR;
        prefabR.GetComponent<PlayerWaypoint>().checkpoint = false;
        prefabR = Instantiate(prefabR, new Vector3(-2030.538f, 18.175f, -580.7215f), transform.rotation);
        Destroy(oldPrefabR);
        prefabL.SetActive(false);
        Destroy(GameObject.Find("WaypointMarker(Clone)"));
        StartCoroutine("Sprint");
        sprintAbility = true;
        foodObjective.enabled = true;
        counter++;
    }

    public void ObjectiveFour()
    {
        Debug.Log("Obj 4");
        foodObjective.enabled = false;
        StartCoroutine("Objectives");
        prefabR.SetActive(false);
        Destroy(GameObject.Find("WaypointMarker(Clone)"));
        escapeObjective.enabled = true;
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
