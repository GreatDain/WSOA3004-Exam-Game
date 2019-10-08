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
    public GameObject gunBarrelPoint;
    public GameObject enemyBullet;
    public GameObject gm;

    public Animator walkCycle;

    public Vector3 anchorPos;
    Vector3 playerPos;
    Vector3 soundPos;

   
    bool changing = false;
    bool shooting = false;
    bool rotating = false;
    bool rotated = false;
    public bool searching = false;
    public bool playerSeen = false;
    public bool playerInShootingRange = false;

    [SerializeField]
    List<Waypoint> patrolPath;

    [SerializeField]
    float moveSpeed = 15f;
    [SerializeField]
    float pursueSpeed = 20f;
    [SerializeField]
    float rotationSpeed = 10f;

    [SerializeField]
    float waitTime;

    [SerializeField]
    float shootDelayTime;

    [SerializeField]
    float rotationDelay;

    [SerializeField]
    [Range(0,360)]
    float patrolRotationAngle;

    [SerializeField]
    Quaternion rotation;

    [SerializeField]
    int count;

    public int currentPath;

    void Start()
    {
        anchorPos = gameObject.transform.position;

        walkCycle.SetBool("isEnemyWalk", false);

        navigator = this.GetComponent<NavMeshAgent>();

        if (patrolPath != null && patrolPath.Count >= 2)
        {
            currentPath = 0;
            patrol();
        }

        else {

            currentState = AISTATE.DETECT;

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

                if (navigator.remainingDistance == 0) {

                    setEndRotation(patrolPath[currentPath].transform);

                }

                if (changing == false)
                {
                    StartCoroutine("changePatrolPath"); // stops state changing multiple times
                }

                break;
            
            case (AISTATE.PATROL):

                navigator.speed = moveSpeed;

                anchorPos = gameObject.transform.position;

                if (navigator.remainingDistance == 0) {

                    currentState = AISTATE.CHANGE;

                    walkCycle.SetBool("isEnemyWalk", false);
                }


                break;

            case (AISTATE.SEARCH):

                navigator.speed = moveSpeed;

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
                        walkCycle.SetBool("isEnemyWalk", false);
                    }
                }

                
                break;

            case (AISTATE.DETECT):

                if (count == 0)
                {
                    StartCoroutine("setRotation");
                }


                transform.rotation = Quaternion.Slerp(transform.rotation,rotation,Time.deltaTime * rotationSpeed);

                if (transform.rotation == rotation && count < 2) {

                    if (rotating == false)
                    {

                        StartCoroutine("changeRotation");
                    }
                }

                if (patrolPath.Count > 0 && rotated == true) {

                    patrol();
                    currentState = AISTATE.PATROL;
                }

                

                break;

            case (AISTATE.PURSUE):

                navigator.speed = pursueSpeed;

                if (playerSeen == true && playerInShootingRange == false)
                {

                    trackPlayer();

                }

                else if (playerSeen == true && playerInShootingRange == true) {

                    navigator.SetDestination(gameObject.transform.position);

                    if (shooting == false) {

                        StartCoroutine("enemyShoot");

                    }


                }

                else if (playerSeen == false && playerInShootingRange == false)
                {

                    StopAllCoroutines();
                    currentState = AISTATE.SEARCH;

                }

                break;

            case (AISTATE.RETURN):

                navigator.speed = moveSpeed;

                returnToPath();

                if (navigator.remainingDistance == 0 && patrolPath.Count > 0)
                {

                    navigator.SetDestination(patrolPath[currentPath].transform.position);
                    //walkCycle.SetBool("isEnemyWalk", false);

                    currentState = AISTATE.PATROL;
                }

                else {

                    currentState = AISTATE.DETECT;

                }

                break;
        }

    }

    void OnTriggerStay(Collider Player)
    {

        if (Player.gameObject.tag.Equals("Player")) {


            playerTracker = Player.gameObject;


            if (playerSeen == true && shooting == false)
            {
                walkCycle.SetBool("isEnemyWalk", true);

                currentState = AISTATE.PURSUE;
            }

            if (Player.gameObject.GetComponent<FPCharacterController>().stableVol >= 0.4f && playerSeen == false)
            {

                soundPos = Player.gameObject.transform.position;
                //walkCycle.SetBool("isEnemyWalk", true);

                currentState = AISTATE.SEARCH;
            }
        }

        

        

    }


    void OnTriggerExit(Collider Player)
    {

        playerTracker = null; 

    }

    public void trackPlayer()
    {

        playerPos = playerTracker.gameObject.transform.position;

        navigator.SetDestination(playerPos);
        walkCycle.SetBool("isEnemyWalk", true);

    }

    public void trackSound() {

        navigator.SetDestination(soundPos);
        walkCycle.SetBool("isEnemyWalk", true);

    }

    public void patrol() {

        navigator.SetDestination(patrolPath[currentPath].transform.position);
        walkCycle.SetBool("isEnemyWalk", true);

        currentState = AISTATE.PATROL;
    }

   

    public void returnToPath()
    {

        navigator.SetDestination(anchorPos);
        walkCycle.SetBool("isEnemyWalk", true);

    }

    public void setSeen(bool seen)
    {

        if (seen == true)
        {

            playerSeen = true;
            gm.GetComponent<GM>().StartCoroutine("Fade");

        }

        else if (seen == false) {

            playerSeen = false;

        }
    }

    public void setInRange(bool inRange) {


        if (inRange == true)
        {

            playerInShootingRange= true;
        }

        else if (inRange == false)
        {

            playerInShootingRange = false;

        }



    }

    public void shootAtPlayer() {


        GameObject firedBullet = Instantiate(enemyBullet, gunBarrelPoint.transform.position,gunBarrelPoint.transform.rotation);
        firedBullet.GetComponent<Rigidbody>().AddForce(gunBarrelPoint.transform.forward * 500);
        //gm.GetComponent<GM>().StartCoroutine("Fade");
        shooting = false;
        currentState = AISTATE.PURSUE;

    }


    private void setEndRotation(Transform target) {

        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * rotationSpeed);


    }

    IEnumerator enemyShoot() {

        shooting = true;

        currentState = AISTATE.IDLE;

        yield return new WaitForSeconds(shootDelayTime);

        shootAtPlayer();

    }

    IEnumerator changePatrolPath()
    {

        changing = true;

        if (changing == true)
        {
            yield return new WaitForSeconds(waitTime);

            currentPath = (currentPath + 1) % patrolPath.Count;

            currentState = AISTATE.DETECT;

        }

        changing = false;
    }

    IEnumerator setRotation() {

        yield return new WaitForSeconds(rotationDelay);

        rotation = Quaternion.Euler(0, transform.rotation.y + Mathf.Pow(-1, count) * patrolRotationAngle, 0);

    }

    IEnumerator changeRotation() {

        rotating = true;

        if(rotating == true) {

            yield return new WaitForSeconds(rotationDelay);

            count++;

            rotation = Quaternion.Euler(0, transform.rotation.y + Mathf.Pow(-1, count) * patrolRotationAngle, 0);

            if (count == 2)
            {

                rotated = true;
            }

           

        }

        rotating = false;

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

