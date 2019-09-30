using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
     NavMeshAgent navigator;

    public enum AISTATE
    {

        IDLE,CHANGE, PATROL, DETECT, SEARCH, PURSUE, RETURN

    };

    public AISTATE currentState;

    public GameObject playerTracker;

    public Vector3 anchorPos;
    Vector3 playerPos;
    Vector3 soundPos;

   
    bool changing = false;
    public bool searching = false;
    public bool playerSeen = false;

    [SerializeField]
    List<Waypoint> patrolPath;

    float speed = 15f;

    [SerializeField]
    float waitTime = 5f;

    public int currentPath;

    void Start()
    {
        anchorPos = gameObject.transform.position;

        navigator = this.GetComponent<NavMeshAgent>();

        if(patrolPath !=null && patrolPath.Count > 2)
        {
            currentPath = 0;
            patrol();
        }

    }
    // Update is called once per frame
    void Update()
    {

        switch (currentState)
        {

            case (AISTATE.IDLE):

                break;

            case (AISTATE.CHANGE):

                if (changing == false)
                {
                    StartCoroutine("changePatrolPath"); // stops state changing multiple times
                }

                break;
            
            case (AISTATE.PATROL):

                anchorPos = gameObject.transform.position;

                if (navigator.remainingDistance == 0) {


                    currentState = AISTATE.CHANGE;

                }


                break;

            case (AISTATE.SEARCH):

                if (changing == false)
                {
                    StartCoroutine("searchSound"); // state changing multiple times
                }

                if (searching == true && changing == true) {

                    trackSound();

                    if (navigator.remainingDistance == 0)
                    {

                        currentState = AISTATE.RETURN;
                        searching = false;
                        changing = false;
                    }
                }

                
                break;

            case (AISTATE.DETECT):

                break;

            case (AISTATE.PURSUE):

                if (playerSeen == true)
                {

                    trackPlayer();

                }

                else if (playerSeen == false) {

                    currentState = AISTATE.SEARCH;

                }

                break;

            case (AISTATE.RETURN):

                returnToPath();

                if (navigator.remainingDistance == 0 && patrolPath.Count>0) {

                    navigator.SetDestination(patrolPath[currentPath].transform.position);

                    currentState = AISTATE.PATROL;
                }

                break;
        }

    }

    void OnTriggerStay(Collider Player)
    {

        playerTracker = Player.gameObject;


        if (playerSeen == true)
        {

            currentState = AISTATE.PURSUE;
        }

        if (Player.gameObject.GetComponent<FPCharacterController>().stableVol >= 0.4f && playerSeen == false)
        {
            
            soundPos = Player.gameObject.transform.position;

            currentState = AISTATE.SEARCH;
        }

        

    }


    void OnTriggerExit(Collider Player)
    {

        playerTracker = null; 

    }

    public void trackPlayer()
    {

        playerPos = playerTracker.gameObject.transform.position;

        float step = speed * Time.deltaTime;

        navigator.SetDestination(playerPos);

    }

    public void trackSound() {

        navigator.SetDestination(soundPos);


    }

    public void patrol() {

        navigator.SetDestination(patrolPath[currentPath].transform.position);

        currentState = AISTATE.PATROL;
    }

   

    public void returnToPath()
    {

        float step = speed * Time.deltaTime;

        navigator.SetDestination(anchorPos);

    }

    public void setSeen(bool seen)
    {

        if (seen == true)
        {

            playerSeen = true;
        }

        else if (seen == false) {

            playerSeen = false;

        }
    }

    IEnumerator changePatrolPath()
    {

        changing = true;

        if (changing == true)
        {
            yield return new WaitForSeconds(waitTime);

            currentPath = (currentPath + 1) % patrolPath.Count;

            patrol();

        }

        changing = false;
    }

    IEnumerator searchSound() {

        changing = true;

        if (changing == true)
        {
            
            yield return new WaitForSeconds(waitTime/3);

            searching = true;

        }

    }
}

