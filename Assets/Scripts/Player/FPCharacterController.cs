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
    private float jump = 20f;
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
    public bool isGrounded;

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
        //Detecting axes and initiating movement
        audioSource.volume = stableVol;
        float translation = Input.GetAxis("Vertical") * speed;

        float strafe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        strafe *= Time.deltaTime;

        transform.Translate(strafe, 0, translation);

        //Checks that the player is/isnt moving and either starts or stops the walk cycle animation
        if (translation != 0 || strafe != 0)
        {
            walkCycle.SetBool("isMove", true);
        }
        else
        {
            walkCycle.SetBool("isMove", false);
        }
        // transform.Translate(0, 0, translation);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //transform.Translate(0, jump * speed, 0);
            gameObject.GetComponent<Rigidbody>().velocity += jump * Vector3.up;
            isGrounded = false;
        }

        //Adds sprint functionality. Speeds up animation accordingly.
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

        //Adds enhanced sneak functionality. Slows down animation accordingly.
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
        //Detects if the player is on the ground.
        if (other.gameObject.tag == "Grass" || other.gameObject.tag == "Gravel")
        {
            isGrounded = true;
        }

        //Detects what terrain the player is walking on (either gravel or grass and sets sounds conditions accordingly.
        if (other.gameObject.tag == "Grass" && isSneak == false && isSprint == false)
        {
            audioSource.volume = 0.4f;
            stableVol = audioSource.volume;
            //isGrounded = true;
        }
        else if (other.gameObject.tag == "Gravel" && isSneak == false && isSprint == false)
        {
            audioSource.volume = 0.6f;
            stableVol = audioSource.volume;
            //isGrounded = true;
        }

        //Player interaction with the cage
        if (other.gameObject.tag == "Cage" && Input.GetKeyUp(KeyCode.E))
        {
            other.gameObject.GetComponent<Collider>().enabled = false;
            //other.gameObject.transform.rotation = Quaternion.Slerp(other.gameObject.transform.rotation, Quaternion.Euler(-90, 0, 0), 2 * Time.deltaTime);
            animator.SetBool("isOpen", true);
            cageOpen = true;
            interactCage.enabled = false;
        }
    }

    //Trigger to allow for cage interaction within a certain distance.
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

        if (other.gameObject.tag == "Gravel" || other.gameObject.tag == "Grass")
        {
            isGrounded = false;
        }
    }
}
