using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTicketAnimations : MonoBehaviour
{
    private Animator healthTicket1Animator;

    // Start is called before the first frame update
    void Start()
    {
        healthTicket1Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthLoss();
    }

    void HealthLoss ()
    {
        if (transform.parent.tag == "SpawnPoint1")
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))//place holder for getting shot
            {
                healthTicket1Animator.SetBool("Shot", true);
                Invoke("DestroyTicket", 2f);
            }
        }

        if (transform.parent.tag == "SpawnPoint2")//place holder for getting shot
        {
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                healthTicket1Animator.SetBool("Shot", true);
                Invoke("DestroyTicket", 2f);
            }
        }

        if (transform.parent.tag == "SpawnPoint3")//place holder for getting shot
        {
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                healthTicket1Animator.SetBool("Shot", true);
                Invoke("DestroyTicket", 2f);
            }
        }
    }

    void DestroyTicket()
    {
        Destroy(gameObject);
    }
}
