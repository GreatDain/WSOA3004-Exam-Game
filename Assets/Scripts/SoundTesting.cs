using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// 'Sound Testing' for the enemies because the script is attached to all enemies (object)
/// </summary>
public class SoundTesting : MonoBehaviour
{
    public AudioSource chaseAudio, grassWalkAudio, gravelWalkAudio, searchAudio;
    public AudioClip chaseClip, grassWalk, gravelWalk, searchClip;

    //Must be serialized or private
    public bool idling = true;          //enemy's normal state is idle
    public bool chasing = false;        //Check if the enemy has spotted player
    public bool searching = false;
    // Start is called before the first frame update
    void Start()
    {
        //source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Play sound to be identified by the player where the enemy is
        if (Input.GetKeyDown(KeyCode.O))
        {
            searching = true;
            //if enemy is chasing (chase= true) player check if sound not playing > if true then play sound else stop sound
            if (searching)
            {
                if (!searchAudio.isPlaying)
                    searchAudio.Play();

            }
            else
                searchAudio.Stop();
        }
        else
            searching = false;

        //play sound to alert the player is beinng chased
        if (Input.GetKeyDown(KeyCode.P))
        {
            chasing = true;
            //if enemy is chasing (chase= true) player check if sound not playing > if true then play sound else stop sound
            if (chasing)
            {
                if (!chaseAudio.isPlaying)
                    chaseAudio.Play();

            }
            else

                chaseAudio.Stop();
        }
        else
            chasing = false;

        //play sound when the player is being caught (Lose)
        if (Input.GetKeyDown(KeyCode.I))
        {

            //Idling sound is jjust a chat (whispers or threats)
            //Replace with States (idle; search etc.)

            idling = true;                          //Enemy is now idling
            //NO sound yet || Temporary Sound
            if (idling)
            {
                // idling = false;                //Enemy is not chasing but just idling

                if (!grassWalkAudio.isPlaying)
                    grassWalkAudio.Play();

            }
            else

                grassWalkAudio.Stop();
                
        }
        else
            idling = false;
    }

    /*
    void Idle()
    {
        //Replace with States (idle; search etc.)

        idling = true;
        //NO sound yet || Temporary Sound
        if (idling)
        {
           // idling = false;                //Enemy is not chasing but just idling

            if (!grassWalkAudio.isPlaying)
                grassWalkAudio.Play();

        }
        else

            grassWalkAudio.Stop();
        chasing = false;
        searching = false;

    }*/
    
        /*
    void Search()
    {
        searching = true;
        //if enemy is chasing (chase= true) player check if sound not playing > if true then play sound else stop sound
        if (searching)
        {
            if (!searchAudio.isPlaying)
                searchAudio.Play();

        }
        else

            searchAudio.Stop();
        searching = false;
    }
    */

        /*
    void Chasing()
    {
        chasing = true;
        //if enemy is chasing (chase= true) player check if sound not playing > if true then play sound else stop sound
        if (chasing)
        {
            if (!chaseAudio.isPlaying)
                chaseAudio.Play();

        }
        else

            chaseAudio.Stop();
        chasing = false;
    } */
}
