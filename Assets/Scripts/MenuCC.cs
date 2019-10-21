using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCC : MonoBehaviour
{

    private float speed = 15f;

    public Animator walkCycle;
    // Start is called before the first frame update
    void Start()
    {
        walkCycle.SetBool("isMove", false);
        walkCycle.SetBool("isMoveBack", false);
        walkCycle.SetBool("isStrafeLeft", false);
        walkCycle.SetBool("isStrafeRight", false);
        walkCycle.SetBool("walkToStrafeL", false);
        walkCycle.SetBool("walkToStrafeR", false);
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;

        float strafe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        strafe *= Time.deltaTime;

        transform.Translate(strafe, 0, translation);

        if (translation > 0)
        {
            if (strafe < 0)
            {
                //walkCycle.SetBool("isMove", false);
                walkCycle.SetBool("walkToStrafeL", true);
            }
            else if (strafe > 0)
            {
                walkCycle.SetBool("walkToStrafeR", true);
            }
            else
            {
                walkCycle.SetBool("walkToStrafeL", false);
                walkCycle.SetBool("walkToStrafeR", false);
                walkCycle.SetBool("isMove", true);
            }
        }
        else if (translation < 0)
        {
            if (strafe < 0)
            {
                //walkCycle.SetBool("isMove", false);
                walkCycle.SetBool("walkToStrafeL", true);
            }
            else if (strafe > 0)
            {
                walkCycle.SetBool("walkToStrafeR", true);
            }
            else
            {
                walkCycle.SetBool("walkToStrafeL", false);
                walkCycle.SetBool("walkToStrafeR", false);
                walkCycle.SetBool("isMoveBack", true);
            }
        }
        else if (strafe < 0)
        {
            walkCycle.SetBool("isStrafeLeft", true);
        }
        else if (strafe > 0)
        {
            walkCycle.SetBool("isStrafeRight", true);
        }
        else
        {
            walkCycle.SetBool("isMove", false);
            walkCycle.SetBool("isMoveBack", false);
            walkCycle.SetBool("isStrafeLeft", false);
            walkCycle.SetBool("isStrafeRight", false);
            walkCycle.SetBool("walkToStrafeL", false);
            walkCycle.SetBool("walkToStrafeR", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Play")
        {
            SceneManager.LoadScene(1);
        }
        else if (other.gameObject.name == "Controls" && Input.GetKeyDown(KeyCode.E))
        {
            print("Do something");
        }
        else if (other.gameObject.name == "Quit" && Input.GetKeyDown(KeyCode.E))
        {
            Application.Quit();
        }
    }
}
