using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundTester : MonoBehaviour
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
    //TEXTURE BOOLEANS
    public bool onGrass = false;            //is the player on grass?
    public bool onGravel = false;
    //SPRINT TOGGLE
    private bool isRunning = false;
    //CONSTANT AUDIO LEVELING
    /*
    [Range (1f, 3f)]
    public float runPitch, sneakPitch, normPitch;           //
    [Range(0.1f, 10f)]
    public float runVol, sneakVol, normVol;                 //
   */
    //road to beta soundtesting
    bool isMoving = false;                  //is is not moving when game starts

    

    // Start is called before the first frame update
    void Start()
    {
        bgSound.Play();                 //play background sound
    }

    // Update is called once per frame
   
    void Update()
    {
        
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && onGrass == false)      //(Movement inputs) checked if player is moving on the grass
        {   
           // Moving();                           //play step movement sound (stomp)
            //OnGrass();                          //Play grass sfx
            print("Now playing Grass SFX");
            //Change breathing sound Pitch to make it faster when player is running
            grassAudioSource.loop = true;       //loop the grass sound while player is moving
            walkAudioSource.loop = true;        //loop moving sound
        }
        else if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical")!= 0) && onGravel == false)         ////checked if player is not moving on grass but gravel
        {
            //On gravel Sound FX
            //Moving();                           //play step movement sound (stomp)
            //OnGravel();                         //play gravel sfx
            walkAudioSource.loop = true;        //loop moving sound
            gravelAudioSource.loop = true;
            print("Now playing Gravel SFX");
        }
      
        //Check if player is sprinting - increase pitch and volume
        if (gameObject.GetComponent<FPCharacterController>().isSprint == true)
        {
            Running();
        }
       
        //Road to beta SoundTesting
        //CHECK IF PLAYER IS MOVING
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            print("Player is moving left");
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
                print("Player is moving on Grass");
                onGravel = false;                   //player is onGrass not Gravel
            }
            else
                onGravel = true;                    //if player is not on Grass s/he's on Gravel

            //if player is moving play audio else stop audio
            if (onGrass)
            {
                if (!grassAudioSource.isPlaying)
                    grassAudioSource.Play();

            }
            else

                grassAudioSource.Stop();
        }

    }

    private void OnTriggerStay(Collider other)
    {
        //check when to play grass audio
        if (other.gameObject.tag == "Grass")
        {
            //print("Now player is in contact with Grass");
            onGrass = true;                 //Check if player has collided with grass

        }
        

        //check when to play gravel audio
        if (other.gameObject.tag == "Gravel")
        {
            //print("Now player is in contact with Gravel");
            onGravel = true;                //If player is OnGravel, it is not on grass
            onGrass = false;                
        }


    }

    //Sound Functions
    //When player is moving
    void Moving()
    {
        //play/loop sound whenever the player is moving
        walkAudioSource.volume = .5f;
        walkAudioSource.Play();
    }

    //when player is sprinting (pressed shift)
    void Running()
    {
        //IdleBreathing();
        breatheAudioSource.pitch = 1.8f;
        breatheAudioSource.volume = 1f;
        breatheAudioSource.PlayOneShot(breatheSFX, .7f);
        breatheAudioSource.loop = true;
    }

    //when player is stationary
    void IdleBreathing()
    {
        //breatheAudioSource.volume = Random.Range(.3f, .7f);
        breatheAudioSource.pitch = 1.3f;           //randomise pitch
        breatheAudioSource.PlayOneShot(breatheSFX,.7f);                                      //randomise volume
        breatheAudioSource.loop = true;

    }

    void RunningBreathe()
    {
        //Player is moving
        breatheAudioSource.volume = .5f;              //randomise volume
        breatheAudioSource.pitch = 1.7f;           //randomise pitch
        breatheAudioSource.Play();
    }

    void OnGrass()
    {
        //play/loop sound whenever the player is moving  
        //grassAudioSource.volume = .7f;           //randomise volume
        grassAudioSource.pitch = 1.3f;            //randomise pitch
        grassAudioSource.PlayOneShot(grassSFX, .7f);                //Play Audio  
    }

    void OnGravel()
    {
        //play/loop sound whenever the player is moving
        gravelAudioSource.PlayOneShot(gravelSFX, 1f);            //randomise volume); 
        gravelAudioSource.pitch = 1.3f;            //randomise pitch
    }

    
    IEnumerator Delay()
    {
        //Delay
        
        yield return new WaitForSeconds(1f);
        
    }
    
    void RunningOnGravel()
    {
        //Increase sound level

    }

}
