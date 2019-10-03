using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class FPCharacterController : MonoBehaviour
{

    private float speedNorm = 15f;
    private float sprint = 30f;
    private float speed = 15f;
    private float sneak = 8f;
    public bool isSprint = false;
    public bool isSneak = false;
    public AudioSource audioSource;
    public float stableVol;
    public bool isClimb = false;
    public bool cageOpen = false;
    public GameObject GM;
    public Animator animator;
    public Animator walkCycle;
    public Text interactCage;

    // Start is called before the first frame update
    void Start()
    {
        stableVol = audioSource.volume;
        animator.SetBool("isOpen", false);
        walkCycle.SetBool("isMove", false);
        interactCage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Basic Player strafing style movement for prototype
        audioSource.volume = stableVol;
        float translation = Input.GetAxis("Vertical") * speed;

        float strafe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        strafe *= Time.deltaTime;

        transform.Translate(strafe, 0, translation);

        if (translation != 0 || strafe != 0)
        {
            walkCycle.SetBool("isMove", true);
        }
        else
        {
            walkCycle.SetBool("isMove", false);
        }
        // transform.Translate(0, 0, translation);

        //Adds sprint functionality. Still needs higher noise levels.
        if (GM.GetComponent<Tutorial>().sprintAbility == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                speed = sprint;
                isSprint = true;
                audioSource.volume = 0.9f;
                stableVol = audioSource.volume;
                walkCycle.speed = 2f;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = speedNorm;
                isSprint = false;
                audioSource.volume = stableVol;
                walkCycle.speed = 1f;
            }
        }

        //Adds enhanced sneak functionality. Still needs lower noise levels.
        if (GM.GetComponent<Tutorial>().sneakAbility == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                audioSource.volume = 0.1f;
                speed = sneak;
                isSneak = true;
                stableVol = audioSource.volume;
                walkCycle.speed = 0.5f;
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                speed = speedNorm;
                isSneak = false;
                audioSource.volume = stableVol;
                walkCycle.speed = 1f;
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Grass" && isSneak == false && isSprint == false)
        {
            audioSource.volume = 0.4f;
            stableVol = audioSource.volume;
        }
        else if (other.gameObject.tag == "Gravel" && isSneak == false && isSprint == false)
        {
            audioSource.volume = 0.6f;
            stableVol = audioSource.volume;
        }

        if (other.gameObject.tag == "Cage" && Input.GetKeyUp(KeyCode.E))
        {
            other.gameObject.GetComponent<Collider>().enabled = false;
            //other.gameObject.transform.rotation = Quaternion.Slerp(other.gameObject.transform.rotation, Quaternion.Euler(-90, 0, 0), 2 * Time.deltaTime);
            animator.SetBool("isOpen", true);
            cageOpen = true;
            interactCage.enabled = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cage")
        {
            interactCage.enabled = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cage")
        {
            interactCage.enabled = false;
        }
    }
}
