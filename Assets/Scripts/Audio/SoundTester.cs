using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
/// <summary>
/// Sound Testing Script for player
/// </summary>
public class SoundTester : MonoBehaviour
{

    /////////////////Does not replace SoundTesting Script///////////////////
    //  AUDIO SOURCES
    public AudioSource grassAudioSource;         //Audio source ref/var
    public AudioSource gravelAudioSource;         //Audio source ref/var
    public AudioSource walkAudioSource;         //Audio source ref/var
    public AudioSource bgSound, bgSound2, bgSound3; //BG Audi
    public AudioSource breatheAudioSource;
    public AudioSource eatAudioSource;
    public AudioSource hideAudioS;              //When player hides in bush audio
    //AUDIO CLIPS
    public AudioClip grassSFX;             //Actual Sound ref/var
    public AudioClip gravelSFX;            //Actual Sound ref/var
    public AudioClip walkSFX;             //Actual Sound ref/var
    public AudioClip breatheSFX;
    public AudioClip bgSFX1, bgSFX2, bgSFX3;
    public AudioClip eatSFX;
    public AudioClip hideClip;
    //TEXTURE BOOLEANS
    private bool onGrass;            //is the player on grass?
    private bool onGravel;
    //SPRINT TOGGLE
    private bool isRunning = false;
    /// <summary>
    /// Moved some of Sounds scripts code to here
    /// </summary>
    FPCharacterController controller;
    GM health;
    //road to beta soundtesting
    bool isMoving = false;                  //is is not moving when game starts



    // Start is called before the first frame update
    void Start()
    {
        //Play different sounds in different locations to make up the environment sounds

        bgSound.Play();                 //play background sound
        bgSound2.Play();                 //play background sound
        bgSound3.Play();                 //play background sound

        controller = gameObject.GetComponent<FPCharacterController>();
        health = gameObject.GetComponent<GM>(); //check monitor player's health for pick up banana & death
    }

    // Update is called once per frame

    void Update()
    {
        if (controller.isSprint == true || controller.cooldown == true)
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
            breatheAudioSource.pitch = 1.15f;
            breatheAudioSource.volume = 0.5f;
        }

        //Road to beta SoundTesting

        //CHECK IF PLAYER IS MOVING: Stmp sound
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            isMoving = true;
        }
        else
            isMoving = false;
        //if player is moving play audio else stop audio
        if (isMoving)
        {
            if (!walkAudioSource.isPlaying)
                walkAudioSource.Play();

        }
        else
            walkAudioSource.Stop();

        ////////////////////////////////////////////////////////////////////////////////////
        ///
        //Chck if player is on Grass
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            if (onGrass == true)
            {
                onGravel = false;                   //player is onGrass not Gravel


                //if player is moving play audio else stop audio
                if (onGrass)
                {
                    if (!grassAudioSource.isPlaying)
                        grassAudioSource.Play();
                    print("Playing on grass sound");
                }
                else

                    grassAudioSource.Stop();
            }

            //chek if player is walking on gravel
            if (onGravel == true)
            {
                onGrass = false;                   //player is onGrass not Gravel

                //if player is moving play audio else stop audio
                if (onGravel)
                {
                    if (!gravelAudioSource.isPlaying)
                        gravelAudioSource.Play();
                    print("Played gravel sound");
                }
                else

                    gravelAudioSource.Stop();
            }
        }
        //Check if player has consumed a banana/health
        if (Input.GetKeyDown(KeyCode.E) && health.GetComponent<GM>().health < 3f)
        {
            //Player gained health
            if (!eatAudioSource.isPlaying)
                eatAudioSource.Play();
        }


    }

    private void OnTriggerStay(Collider other)
    {
        //check when to play grass audio
        if (other.gameObject.tag == "Grass")
        {
            onGrass = true;                         //Check if player has collided with grass

        }
        else
            onGrass = false;


        //check when to play gravel audio
        if (other.gameObject.tag == "Gravel")
        {
            onGravel = true;                        //If player is OnGravel, it is not on grass

        }
        else
            onGravel = false;

    }
    //Hiding audioFX stuff
    private void OnTriggerEnter(Collider other)
    {
        //Hiding SFX
        //Check if player is hiding the bush tagged ("Hidden")
        if (other.gameObject.tag == "Hidden")
        {
            // if (!hideAudioS.isPlaying)
            //hideAudioS.PlayOneShot(hideClip, 1f);
            if (!hideAudioS.isPlaying)
                hideAudioS.Play();
                print("Played Hidding SFX");
        }

    }

}