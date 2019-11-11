using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// 'Sound Testing' for the enemies because the script is attached to all enemies (object)
/// </summary>
public class SoundTesting : MonoBehaviour
{
    public AudioSource grassWalkAudio, gravelWalkAudio;
    public AudioClip grassWalk, gravelWalk;

    //Must be serialized or private
    public bool idling;          //enemy's normal state is idle
    public bool chasing;        //Check if the enemy has spotted player
    public bool searching;
    public bool onGrass = true;      //Where is the enemy located
    public bool onGravel = false;

    private EnemyAI enemyScript;

    //
    public GameObject enemy;           //Referencing the enemy
    public bool isMoving = false;
    //public Transform enemyPos;         //Enemy's postuon
    //private Vector3 lastPos;            //Enemy's previous positio
    //public float hasmoved = 0.01f;      //minimum float to recognise as 'moved'
    // Start is called before the first frame update
    void Start()
    {
        enemyScript = enemy.GetComponent<EnemyAI>();
       
        //lastPos = enemyPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        /////////////////////// THESE IF STATEMENTS BELOW MUST BE CALLED ACCORDING THE ENEMY'S 'CURRENT STATE' TO TEST SOUND//////////////////////////////////////

        //CheckEnemyPos();

        if (enemyScript.currentState == EnemyAI.AISTATE.PATROL || enemyScript.currentState == EnemyAI.AISTATE.PURSUE)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

            //Play audio corresponding to enemy state
        /*if (enemyScript.currentState == EnemyAI.AISTATE.PURSUE)
        {
           
        }

        //Enemy checks if its seaching while on grass or gravel
        if (enemyScript.currentState == EnemyAI.AISTATE.SEARCH)
        {
           
        }*/


    }

    /// <summary>
    /// Enemies do not collide with planes (Grass & gravel)
    /// </summary>

    private void OnTriggerStay(Collider other)
    {
        //check when to play grass audio
        if (other.gameObject.tag == "Grass")
        {
            onGrass = true;                         //Check if player has collided with grass
            onGravel = false;
            //print("OnTrigger is called");
        }
        else if (other.gameObject.tag == "Gravel")
        {
            onGrass = false;
            onGravel = true;                        //If player is OnGravel, it is not on grass

        }

    }

    /*void Patrolx()
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

    }

    void Patrol()
    {
        print("Trying to playe enemy texture sound");

        if (onGrass)
        {
            print("OnGrass sir");
            //if enemy is onGrass and enemy is moving == play walking on grass texture sfx
            if (!grassWalkAudio.isPlaying)
            {
                grassWalkAudio.Play();          //play grass audion if its !playing
                print("enemy is walking on grass");
            }
            else
            {
                grassWalkAudio.Stop();            //stop grass audio
            }
        }


        if (onGravel)
        {
            //if enemy is onGrass and enemy is moving == play walking on grass texture sfx
            if (!gravelWalkAudio.isPlaying)
            {
                gravelWalkAudio.Play();          //play grass audion if its !playing
                print("enemy is walking on gravel");
            }
            else
            {
                gravelWalkAudio.Stop();            //stop grass audio
            }
        }
    }*/

    public void Step()
    {
        if (onGrass && isMoving == true)
        {
            grassWalkAudio.Play();
        }

        else if (onGravel && isMoving == true)
        {
            gravelWalkAudio.Play();
        }
    }
}
