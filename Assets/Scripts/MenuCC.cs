using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCC : MonoBehaviour
{

    private float speedNorm = 15f;
    private float sprint = 30f;
    private float speed = 15f;
    private float sneak = 8f;
    public Animator walkCycle;
    public bool cooldown = false;
    public float sprintTime;
    private float sprintDuration = 5f;
    public bool isSprint = false;
    public bool isSneak = false;
    public float stableVol;
    public AudioSource audioSource;
    public bool isGrounded = false;
    public GameObject controlsPanel;
    public bool controlsEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        walkCycle.SetBool("isMove", false);
        walkCycle.SetBool("isMoveBack", false);
        walkCycle.SetBool("isStrafeLeft", false);
        walkCycle.SetBool("isStrafeRight", false);
        walkCycle.SetBool("walkToStrafeL", false);
        walkCycle.SetBool("walkToStrafeR", false);

        stableVol = audioSource.volume;

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

        if (Input.GetKey(KeyCode.LeftShift) && cooldown == false)
        {
            sprintTime += Time.deltaTime;
        }
        else if (cooldown == true)
        {
            sprintTime -= Time.deltaTime;
        }

        if (sprintTime <= 0)
        {
            cooldown = false;
        }

        //Adds sprint functionality. Speeds up animation accordingly.
        if (!isSprint && translation > 0 && Input.GetKeyDown(KeyCode.LeftShift) && cooldown == false)
        {
            speed = sprint;
            isSprint = true;
            audioSource.volume = 0.9f;
            stableVol = audioSource.volume;
            walkCycle.speed = 2f;
        }
        else if (isSprint && (Input.GetKeyUp(KeyCode.LeftShift) || (sprintTime > sprintDuration)))
        {
            speed = speedNorm;
            isSprint = false;
            audioSource.volume = stableVol;
            walkCycle.speed = 1f;
            cooldown = true;
        }

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

        if (isSprint == true || cooldown == true)
        {
            audioSource.pitch = 1.8f;
            audioSource.volume = 0.7f;
        }
        else if (isSneak == true)
        {
            audioSource.pitch = 0.8f;
            audioSource.volume = 0.3f;
        }
        else
        {
            audioSource.pitch = 1.15f;
            audioSource.volume = 0.5f;
        }

        sprintTime = Mathf.Clamp(sprintTime, 0, sprintDuration);

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(controlsEnabled == true)
            {
                controlsEnabled = false;
                controlsPanel.SetActive(false);
            }
            else if (controlsEnabled == false)
            {
                controlsEnabled = true;
                controlsPanel.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine("FadeMenu");
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
    }

    public IEnumerator FadeMenu()
    {
        float fadeTime = gameObject.GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(0);
    }

    /*private void OnTriggerEnter(Collider other)
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
    }*/
}
