using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Tutorial : MonoBehaviour
{
    public Text objective1;
    public Text objective2;
    public Text objective3;
    public Text objective4;
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
        objective1.enabled = true;
        objective2.enabled = false;
        objective3.enabled = false;
        objective4.enabled = false;
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
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
        objective2.enabled = true;
        prefab = Instantiate(prefab, new Vector3(-71f, 7.7f, 69f), transform.rotation);
        sprintAbility = true;
        counter++;
    }

    public void ObjectiveThree()
    {
        Debug.Log("Obj 3");
        oldPrefab = prefab;
        prefab.GetComponent<PlayerWaypoint>().checkpoint = false;
        prefab = Instantiate(prefab, new Vector3(-54f, 0, -142f), transform.rotation);
        Destroy(GameObject.Find("WaypointMarker(Clone)"));
        Destroy(oldPrefab);
        objective2.enabled = false;
        objective3.enabled = true;
        sneakAbility = true;
        counter++;
    }

    public void ObjectiveFour()
    {
        Debug.Log("Obj 4");
        oldPrefab = prefab;
        objective3.enabled = false;
        objective4.enabled = true;
        prefab.GetComponent<PlayerWaypoint>().checkpoint = false;
        prefab = Instantiate(prefab, new Vector3(-54f, 0, -60f), transform.rotation);
        Destroy(GameObject.Find("WaypointMarker(Clone)"));
        Destroy(oldPrefab);
        counter++;
    }
}
