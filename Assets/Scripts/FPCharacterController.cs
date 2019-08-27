using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FPCharacterController : MonoBehaviour
{

    private float speedNorm = 10f;
    private float sprint = 20f;
    private float speed = 10f;
    private float sneak = 5f;
    public bool isSprint = false;
    public bool isSneak = false;
    public AudioSource audioSource;
    public float stableVol;

    // Start is called before the first frame update
    void Start()
    {
        stableVol = audioSource.volume;
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
       // transform.Translate(0, 0, translation);

        //Adds sprint functionality. Still needs higher noise levels.
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = sprint;
            isSprint = true;
            audioSource.volume = 1f;
            stableVol = audioSource.volume;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            speed = speedNorm;
            isSprint = false;
            audioSource.volume = stableVol;
        }

        //Adds enhanced sneak functionality. Still needs lower noise levels.
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            audioSource.volume = 0.1f;
            speed = sneak;
            isSneak = true;
            stableVol = audioSource.volume;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            speed = speedNorm;
            isSneak = false;
            audioSource.volume = stableVol;
        }
    }

    private void OnTriggerStay(Collider other)
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
    }
}
