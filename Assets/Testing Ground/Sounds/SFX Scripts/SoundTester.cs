using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundTester : MonoBehaviour
{

    /////////////////Does not replace SoundTesting Script///////////////////
    //public float delaySound;
    public AudioSource grassAudioSource;         //Audio source ref/var
    public AudioSource gravelAudioSource;         //Audio source ref/var
    public AudioSource walkAudioSource;         //Audio source ref/var
    public AudioSource bgSound;
    public AudioSource breatheAudioSource;

    public AudioClip grassSFX;             //Actual Sound ref/var
    public AudioClip gravelSFX;            //Actual Sound ref/var
    public AudioClip walkSFX;             //Actual Sound ref/var
                                          //Testing Sound
    public AudioClip breatheSFX;
    public AudioClip bgSFX;

    public bool onGrass = false;            //is the player on grass?
    public bool onGravel = false;
    //manage stamina
    private bool isRunning = false;
    

    // Start is called before the first frame update
    void Start()
    {
        //The player starts  breathing when the game starts
        //IdleBreathing();                //Breathing Sound when player is Idling
        bgSound.Play();                 //plau background sound
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 && onGrass == true)      //checked if player is moving on the grass
        {   
            Moving();                           //play step movement sound (stomp)
            print("Now playing Grass SFX");
            OnGrass();                          //Play grass sfx
            //RunningBreathe();
            //Change breathing sound Pitch to make it faster
            grassAudioSource.loop = true;       //loop the grass sound while player is moving
            walkAudioSource.loop = true;        //loop moving sound
        }
        else //On gravel Sound FX
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 && onGravel == true)         ////checked if player is not moving on grass but gravel
        {
            Moving();                           //play step movement sound (stomp)
            OnGravel();                         //play gravel sfx
            walkAudioSource.loop = true;        //loop moving sound
            print("Now playing Gravel SFX");
        }
      
        //Check if the player is still pressing the button|| else stop playing the sound
     
        //Check if player is printing - increase pitch and volume
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            print("Player is ready to sprint");
        }

    }

    private void OnTriggerStay(Collider other)
    {
        //check when to play grass audio
        if (other.gameObject.tag == "Grass")
        {
            print("Now player is in contact with Grass");
            onGrass = true;                 //Check if player has collided with grass
            onGravel = false;               //if player is OnGrass, it is not on Gravel
        }
        

        //check when to play gravel audio
        if (other.gameObject.tag == "Gravel")
        {
            print("Now player is in contact with Gravel");
            onGravel = true;                //If player is OnGravel, it is not on grass
            onGrass = false;                
        }

        //other collisions
        //check if player is not on either grass nor gravel
        if (other.gameObject.tag != "Gravel" && other.gameObject.tag != "Grass")
        {
            print("Now player is in contact with the tree");
            //Do not play grass or gravel SFX if player in not in contact
            onGravel = false;
            onGrass = false;
            
            gravelAudioSource.Stop();
            grassAudioSource.Stop();
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
        IdleBreathing();
        breatheAudioSource.pitch = 1.8f;
    }

    //when player is stationary
    void IdleBreathing()
    {
        //breatheAudioSource.volume = Random.Range(.3f, .7f);
        breatheAudioSource.pitch = 1.3f;           //randomise pitch
        breatheAudioSource.PlayOneShot(breatheSFX,.7f);                                      //randomise volume

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
