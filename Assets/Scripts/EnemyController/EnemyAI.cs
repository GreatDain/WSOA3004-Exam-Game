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
    LayerMask targetMask;

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

        // initialises starting position and nav mesh navigator for enemy
        anchorPos = gameObject.transform.position;

        walkCycle.SetBool("isEnemyWalk", false);

        navigator = this.GetComponent<NavMeshAgent>();

        // checks if the enemy has a patrol path or not and then decides its behavior according to state or function

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

        // states for ai behaviours

        switch (currentState)
        {

            case (AISTATE.IDLE): // does nothing in this state
                if (playerSeen == false && playerInShootingRange == false)// if any player exits range
                {
                    StopAllCoroutines(); // cancels shooting function
                }
                break;

            case (AISTATE.CHANGE): // is called when enemy is deciding on next destination

                if (navigator.remainingDistance == 0) {

                    setEndRotation(patrolPath[currentPath].transform); // sets rotation of the enemy to the way points rotation

                }

                if (changing == false)// if statement prevents co routine from being called multiple times
                {
                    StartCoroutine("changePatrolPath"); // calls routine to change the enemy navigators destination
                }

                break;
            
            case (AISTATE.PATROL): // behavior for patrolling preset paths according to waypoints

                navigator.speed = moveSpeed; // sets standard move speed

                anchorPos = gameObject.transform.position; // updates origin position while patrolling for when enemy needs to return to patrol

                if (navigator.remainingDistance == 0) { // checks if destination has been reached

                    currentState = AISTATE.CHANGE; // switchs state to change for next destination

                    walkCycle.SetBool("isEnemyWalk", false);
                }


                break;

            case (AISTATE.SEARCH): // state for searching for player when a sound is detected

                navigator.speed = moveSpeed;

                if (changing == false) // to prevent multiple calls
                {
                    StartCoroutine("searchSound"); // interupts patrol to search in the area of the sound
                }

                if (searching == true && changing == true) {

                    trackSound(); // updates sound position due to new sounds

                    if (navigator.remainingDistance == 0) // if reaches sound destination and has not detected the player, enemy returns to patrol
                    {

                        currentState = AISTATE.RETURN;
                        searching = false;
                        changing = false; // resets for future use
                        walkCycle.SetBool("isEnemyWalk", false);
                    }
                }

                
                break;

            case (AISTATE.DETECT): // for rotating the enemy that is stationary or changing their patrol route

                if (count == 0)
                {
                    StartCoroutine("setRotation");// sets rotation for initial look
                }


                transform.rotation = Quaternion.Slerp(transform.rotation,rotation,Time.deltaTime * rotationSpeed);

                if (transform.rotation == rotation && count < 2) { // if statement to make the enemy rotate twice before continueing

                    if (rotating == false)// [prevents multiple calls and allows enemy to rotate before rotation is updated
                    {
                        StartCoroutine("changeRotation");
                    }
                }

                if (patrolPath.Count > 0 && rotated == true) { // continues with patrol path if it exists

                    patrol();
                    count = 0;
                    rotated = false;
                    currentState = AISTATE.PATROL;
                }

                else if (patrolPath.Count == 0 && rotated == true)
                { // continues with patrol path if it exists

                    count = 0;
                    rotated = false;
                   
                }



                break;

            case (AISTATE.PURSUE): // pursueing a detected player

                navigator.speed = pursueSpeed; // changes movement speed

                if (playerSeen == true && playerInShootingRange == false)
                {
                    trackPlayer();// tracks player position and follows when in line of sight
                }

                else if (playerSeen == true && playerInShootingRange == true) { // if player is in shooting range

                    navigator.SetDestination(gameObject.transform.position); // enemy stops moving

                    if (shooting == false) {

                        StartCoroutine("enemyShoot"); // calls shoot function

                    }


                }

                if (playerSeen == false && playerInShootingRange == false)// if any player exits range
                {
                    StopAllCoroutines(); // cancels shooting function
                    currentState = AISTATE.SEARCH; // sets player to search for sound
                }

                break;

            case (AISTATE.RETURN): // returns to original position or last patrol position

                navigator.speed = moveSpeed;

                returnToPath();

                if (navigator.remainingDistance == 0 && patrolPath.Count > 0) // returns to patrol after reaching orignal position
                {

                    navigator.SetDestination(patrolPath[currentPath].transform.position);
                    //walkCycle.SetBool("isEnemyWalk", false);

                    currentState = AISTATE.PATROL;
                }

                else {

                    currentState = AISTATE.DETECT;// if not patrolling returns to origin for rotating patrol

                }

                break;
        }

    }

    void OnTriggerStay(Collider Player) // trigger for detecting and tracking player information
    {

        if (Player.gameObject.tag.Equals("Player")) {


            playerTracker = Player.gameObject; // sets player tracker when player is in range


            if (playerSeen == true && shooting == false)
            {
                walkCycle.SetBool("isEnemyWalk", true);

                currentState = AISTATE.PURSUE; // calles pursue if player is seen
            }

            if (Player.gameObject.GetComponent<FPCharacterController>().stableVol >= 0.4f && playerSeen == false)
            {

                soundPos = Player.gameObject.transform.position;
                //walkCycle.SetBool("isEnemyWalk", true);

                currentState = AISTATE.SEARCH; // calls search if player makes sound near enemy
            }
        }

        

        

    }


    void OnTriggerExit(Collider Player)
    {

        playerTracker = null; // resets tracker if enemy leaves enemy range

    }

    public void trackPlayer()//function that makes enemy follow player position
    {

        playerPos = playerTracker.gameObject.transform.position;

        navigator.SetDestination(playerPos);
        walkCycle.SetBool("isEnemyWalk", true); 

    }

    public void trackSound() { // function for enemy to follow sound position

        navigator.SetDestination(soundPos);
        walkCycle.SetBool("isEnemyWalk", true);

    }

    public void patrol() { // function for enemy to follow waypoint in patrol

        navigator.SetDestination(patrolPath[currentPath].transform.position);
        walkCycle.SetBool("isEnemyWalk", true);

        currentState = AISTATE.PATROL;
    }

   

    public void returnToPath() // function for enemy to go to last saved position
    {

        navigator.SetDestination(anchorPos);
        walkCycle.SetBool("isEnemyWalk", true);

    }

    public void setSeen(bool seen) // sets whether the player has been seen or not
    {
       
        if (seen == true)
        {

            playerSeen = true;
            //gm.GetComponent<GM>().StartCoroutine("Fade");

        }

        else if (seen == false) {

            playerSeen = false;

        }
    }

    public void setInRange(bool inRange) { // sets whether or not the player is in shooting range or not


        if (inRange == true)
        {

            playerInShootingRange= true;
        }

        else if (inRange == false)
        {

            playerInShootingRange = false;

        }



    }

    public void shootAtPlayer() { // function to shoot at enemy

        Vector3 dirToTarget = (playerTracker.transform.position - transform.position).normalized;
        float targetDistance = Vector3.Distance(transform.position, playerTracker.transform.position);

        if (Physics.Raycast(transform.position, dirToTarget, targetDistance,targetMask)) {

            Debug.Log("hit player with raycast");

        }
       
        //gm.GetComponent<GM>().StartCoroutine("Fade");
        shooting = false;
        currentState = AISTATE.PURSUE;

    }


    private void setEndRotation(Transform target) { // sets enemy rotation to waypoint rotation

        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * rotationSpeed);


    }

    IEnumerator enemyShoot() { // coroutine for shooting with delay

        shooting = true;

        currentState = AISTATE.IDLE;

        yield return new WaitForSeconds(shootDelayTime);

        shootAtPlayer();

    }

    IEnumerator changePatrolPath()// coroutine for change the next waypoiint in patrol
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

    IEnumerator setRotation() { // function for rotating enemy field of view

        yield return new WaitForSeconds(rotationDelay);

        rotation = Quaternion.Euler(0, transform.rotation.y + Mathf.Pow(-1, count) * patrolRotationAngle, 0);

    }

    IEnumerator changeRotation()
    {// function for rotating enemy field of view

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

    IEnumerator searchSound() {// coroutine for delay before enemy seeks sound position

        changing = true;

        if (changing == true)
        {
            
            yield return new WaitForSeconds(waitTime/3);

            searching = true;

        }

    }
}

