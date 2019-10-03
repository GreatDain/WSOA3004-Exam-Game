﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Tutorial : MonoBehaviour
{
    public Text objective1;
    public Text objective2;
    public Text constantObjText;
    public Text sprintUnlocked;
    public Text howToSprint;
    public Canvas objectivesTab;
    GameObject FPS;
    public GameObject prefab;
    GameObject oldPrefab;
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

        counter = 0;
        constantObjText.enabled = true;

        sprintUnlocked.enabled = false;
        howToSprint.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && objectivesTab.enabled == true)
        {
            objectivesTab.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && objectivesTab.enabled == false)
        {
            objectivesTab.enabled = true;
        }

        if (FPS.GetComponent<FPCharacterController>().cageOpen == true && counter == 0)
        {
            ObjectiveOne();
        }

        if (prefab.GetComponent<PlayerWaypoint>().checkpoint == true && counter == 1f)
        {
            ObjectiveThree();
        }

        if (prefab.GetComponent<PlayerWaypoint>().checkpoint == true && counter == 2f)
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
        StartCoroutine("Sprint");
        sprintAbility = true;
        objective2.enabled = true;
        //sneakAbility = true;
        counter++;
    }

    public void ObjectiveFour()
    {
        //StartCoroutine("Objectives");
        oldPrefab = prefab;
        prefab.GetComponent<PlayerWaypoint>().checkpoint = false;
        prefab = Instantiate(prefab, new Vector3(-54f, 0, -142f), transform.rotation);
        Destroy(GameObject.Find("WaypointMarker(Clone)"));
        Destroy(oldPrefab);
        counter++;
    }

    /*public void ObjectiveFive()
    {
        StartCoroutine("Objectives");
        oldPrefab = prefab;
        prefab.GetComponent<PlayerWaypoint>().checkpoint = false;
        prefab = Instantiate(prefab, new Vector3(-54f, 0, -142f), transform.rotation);
        Destroy(GameObject.Find("WaypointMarker(Clone)"));
        Destroy(oldPrefab);
        counter++;
    }*/

    private IEnumerator Sprint()
    {
        sprintUnlocked.enabled = true;
        yield return new WaitForSeconds(1.5f);
        sprintUnlocked.enabled = false;
        howToSprint.enabled = true;
        yield return new WaitForSeconds(3f);
        howToSprint.enabled = false;
    }

    private IEnumerator Objectives()
    {
        objectivesTab.enabled = true;
        yield return new WaitForSeconds(10f);
        objectivesTab.enabled = false;
    }
}
