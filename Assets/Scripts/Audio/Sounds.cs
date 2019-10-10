using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    /////////////////Does not replace SoundTesting Script///////////////////
    //  AUDIO SOURCES
    public AudioSource grassAudioSource;         //Audio source ref/var
    public AudioSource gravelAudioSource;         //Audio source ref/var
    public AudioSource walkAudioSource;         //Audio source ref/var
    public AudioSource bgSound;
    public AudioSource breatheAudioSource;
    //AUDIO CLIPS
    public AudioClip grassSFX;             //Actual Sound ref/var
    public AudioClip gravelSFX;            //Actual Sound ref/var
    public AudioClip walkSFX;             //Actual Sound ref/var
    public AudioClip breatheSFX;
    public AudioClip bgSFX;

    FPCharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        bgSound.Play();                 //play background sound
        controller = gameObject.GetComponent<FPCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetAxis("Vertical") > 0)
        {
            walkAudioSource.Play();
        }

        else if (Input.GetAxis("Vertical") == 0)
        {
            walkAudioSource.Stop();
        }*/
        //controller.walkCycle.GetBool("isMove") == true &&
        if (controller.isSprint == true)
        {
            breatheAudioSource.pitch = 1.8f;
            breatheAudioSource.volume = 0.7f;
        }
        else if (controller.isSneak == true)
        {
            breatheAudioSource.pitch = 0.8f;
            breatheAudioSource.volume = 0.3f;
        }
        else
        {
            breatheAudioSource.pitch = 1.3f;
            breatheAudioSource.volume = 0.5f;
        }
    }
}
