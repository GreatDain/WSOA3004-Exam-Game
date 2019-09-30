using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Tutorial : MonoBehaviour
{
    public Text objective1;
    public Text objective2;
    GameObject FPS;
    public GameObject prefab;
    public bool sprintAbility = false;
    public bool sneakAbiilty = false;
    // Start is called before the first frame update

    void Start()
    {
        FPS = GameObject.FindGameObjectWithTag("Player");
        objective1.enabled = true;
        objective2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (FPS.GetComponent<FPCharacterController>().cageOpen == true)
        {
            ObjectiveOne();
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
        Instantiate(prefab, new Vector3(-71f, 7.7f, 69f), transform.rotation);
        sprintAbility = true;
    }
}
