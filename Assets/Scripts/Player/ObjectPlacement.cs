using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ObjectPlacement : MonoBehaviour
{
    public GameObject target;
    private Vector3 triggerPos;
    private float speed = 65f;
    public GameObject bird;
    private Vector3 startPos;
    private bool isFlying;
    private bool bullseye;

    // Start is called before the first frame update
    void Start()
    {
        bullseye = false;
        target.SetActive(false);
        isFlying = false;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        if (Input.GetMouseButtonDown(1) && bullseye == false)
        {
            target.SetActive(true);
            bullseye = true;
        }
        else if (Input.GetMouseButtonDown(1) && bullseye == true)
        {
            target.SetActive(false);
            bullseye = false;
        }
        //startPos = new Vector3(gameObject.transform.position.x, transform.position.y + 30, transform.position.z);
        if (Input.GetMouseButtonDown(0) && bullseye == true)
        {
            isFlying = true;
            triggerPos = target.transform.position;
        }

        if (isFlying == true)
        {
            bird.transform.position = Vector3.MoveTowards(bird.transform.position, triggerPos, step);
            if (bird.transform.position == triggerPos)
            {
                isFlying = false;
            }
        }
    }
}
