using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    NavMeshAgent navigator;

    public enum AISTATE
    {

        IDLE, CHANGE, PATROL, DETECT, SEARCH, PURSUE, RETURN

    };

    public AISTATE currentState;

    public GameObject playerTracker;
    public GameObject gunBarrelPoint;
    public GameObject enemyBullet;
    public GameObject gm;

    public Animator walkCycle;

    Vector3 anchorPos;
    Vector3 playerPos;
    public Vector3 soundPos;


    public bool changing = false;
    bool shooting = false;
    bool rotated = false;
    public bool searching = false;
    public bool playerSeen = false;
    public bool playerInShootingRange = false;

    public AudioSource ShootingSource;
    public AudioSource ReloadSource;

    [SerializeField]
    LayerMask targetMask;

    [SerializeField]
    List<Waypoint> patrolPath;

    [SerializeField]
    float moveSpeed = 15f;
    [SerializeField]
    float pursueSpeed = 20f;
    [SerializeField]
    float waitTime;
    [SerializeField]
    float shootDelayTime;

    float soundRange = 25f;

    [SerializeField]
    int count;

    public int currentPath;

    //  rotation

    private int currentCount = 0;
    [SerializeField]
    [Range(0, 360)]
    float detectRotation;
    [SerializeField]
    protected float rotationSpeed = 1f;
    [SerializeField]
    float rotationDelay = 1;
    [SerializeField]
    private float currentTimeCheck = 0;
    private float currentGoal = 0;
    private float initialAngle;

    private bool isClockwise = false;
    bool rotating = true;

    private Quaternion initialRotation;
    private Quaternion currentRotationGoal;
    private Quaternion sourceRotation;

    public Vector3 destination;
    public float remaining;

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

        else
        {
            initialAngle = transform.eulerAngles.y;
            initialRotation = sourceRotation = transform.rotation;
            currentGoal = isClockwise ? detectRotation : -detectRotation;
            currentRotationGoal = transform.rotation * Quaternion.Euler(0, currentGoal, 0);

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
                IdleUpate();
                break;

            case (AISTATE.CHANGE): // is called when enemy is deciding on next destination

                ChangeUpdate();

                break;

            case (AISTATE.PATROL): // behavior for patrolling preset paths according to waypoints

                PatrolUpdate();

                break;

            case (AISTATE.SEARCH): // state for searching for player when a sound is detected

                SearchUpdate();

                break;

            case (AISTATE.DETECT): // for rotating the enemy that is stationary or changing their patrol route

                DetectUpdate();

                break;

            case (AISTATE.PURSUE): // pursueing a detected player

                PursueUpdate();

                break;

            case (AISTATE.RETURN): // returns to original position or last patrol position

                ReturnUpdate();

                break;
        }

    }

    private void ReturnUpdate()
    {
        searching = false;
        changing = false; // resets for future use

        navigator.speed = moveSpeed;

        returnToPath();

        if (navigator.remainingDistance == 0 && patrolPath.Count > 0) // returns to patrol after reaching orignal position
        {

            patrol();
            //walkCycle.SetBool("isEnemyWalk", false);


        }

        else if (navigator.remainingDistance == 0 && patrolPath.Count == 0)
        {

            currentState = AISTATE.DETECT;// if not patrolling returns to origin for rotating patrol

        }
    }

    private void PursueUpdate()
    {
        navigator.speed = pursueSpeed; // changes movement speed

        if (playerSeen == true && playerInShootingRange == false)
        {
            StopCoroutine("enemyShoot");
            shooting = false;
            walkCycle.SetBool("isShooting", false);
            trackPlayer();// tracks player position and follows when in line of sight

        }

        else if (playerSeen == true && playerInShootingRange == true)
        { // if player is in shooting range

            navigator.SetDestination(gameObject.transform.position); // enemy stops moving

            if (shooting == false)
            {

                StartCoroutine("enemyShoot"); // calls shoot function

            }

            
        }

        if (playerSeen == false && playerInShootingRange == false)// if any player exits range
        {
            StopCoroutine("enemyShoot"); // cancels shooting function
            shooting = false;
            walkCycle.SetBool("isShooting", false);
            currentState = AISTATE.SEARCH; // sets player to search for sound
        }
    }

    private void DetectUpdate()
    {
        if (currentCount < 3 && rotating == true && navigator.remainingDistance == 0)
        {

            currentTimeCheck += Time.deltaTime * rotationSpeed;
            var newZ = Mathf.Lerp(initialAngle, currentGoal, currentTimeCheck);
            transform.rotation = Quaternion.Slerp(sourceRotation, currentRotationGoal, currentTimeCheck);
        }

        if (currentTimeCheck >= 1)
        {
            isClockwise = !isClockwise;
            currentGoal = isClockwise ? detectRotation : -detectRotation;
            currentTimeCheck = 0;
            initialAngle = transform.eulerAngles.y;
            sourceRotation = transform.rotation;
            currentRotationGoal = initialRotation * Quaternion.Euler(0, currentGoal, 0);

            currentCount++;

            StartCoroutine("callRotationDelay");
        }

        if (currentCount == 3)
        {
            rotated = true;

        }

        if (patrolPath.Count > 0 && rotated == true)
        { // continues with patrol path if it exists


            currentCount = 0;
            rotated = false;
            patrol();
        }

        else if (patrolPath.Count == 0 && rotated == true)
        { // continues with patrol path if it exists

            currentCount = 0;
            rotated = false;

        }
    }

    private void SearchUpdate()
    {
        navigator.speed = moveSpeed;

        if (changing == false) // to prevent multiple calls
        {
            StartCoroutine("searchSound"); // interupts patrol to search in the area of the sound
        }

        destination = navigator.destination;
        remaining = navigator.remainingDistance;

        if (searching == true && changing == true)
        {

            trackSound(); // updates sound position due to new sounds


            if (navigator.remainingDistance == 0 && playerSeen == false && playerInShootingRange == false) // if reaches sound destination and has not detected the player, enemy returns to patrol
            {

                currentState = AISTATE.RETURN;
                
                walkCycle.SetBool("isEnemyWalk", false);
            }
        }
    }

    private void PatrolUpdate()
    {
        navigator.speed = moveSpeed; // sets standard move speed

        anchorPos = gameObject.transform.position; // updates origin position while patrolling for when enemy needs to return to patrol



        if (navigator.remainingDistance == 0)
        { // checks if destination has been reached



            currentState = AISTATE.CHANGE; // switchs state to change for next destination

            walkCycle.SetBool("isEnemyWalk", false);
        }
    }

    private void ChangeUpdate()
    {
        if (navigator.remainingDistance == 0)
        {

            setEndRotation(patrolPath[currentPath].transform); // sets rotation of the enemy to the way points rotation

        }

        if (changing == false)// if statement prevents co routine from being called multiple times
        {
            StartCoroutine(changePatrolPath()); // calls routine to change the enemy navigators destination
        }
    }

    private void IdleUpate()
    {
        if (playerSeen == false && playerInShootingRange == false)// if any player exits range
        {
            StopCoroutine("enemyShoot"); ; // cancels shooting function
            shooting = false;
            walkCycle.SetBool("isShooting", false);
            walkCycle.SetBool("isEnemyWalk", true);

            currentState = AISTATE.SEARCH;
        }


    }

    void OnTriggerStay(Collider Player) // trigger for detecting and tracking player information
    {

        if (Player.gameObject.tag.Equals("Player"))
        {

            playerTracker = Player.gameObject; // sets player tracker when player is in range

            float distance = Vector3.Distance(gameObject.transform.position, Player.transform.position);

            if (playerSeen == true && shooting == false)
            {
                walkCycle.SetBool("isEnemyWalk", true);

                currentState = AISTATE.PURSUE; // calles pursue if player is seen
            }

            else if (Player.gameObject.GetComponent<FPCharacterController>().stableVol >= 0.4f )
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

        if(shooting == false) {

            navigator.SetDestination(playerPos);
            walkCycle.SetBool("isEnemyWalk", true);

        }

    }

    public void trackSound()
    { // function for enemy to follow sound position

        if (shooting == false)
        {
            navigator.SetDestination(soundPos);
            walkCycle.SetBool("isEnemyWalk", true);
        }
    }

    public void patrol()
    { // function for enemy to follow waypoint in patrol

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

        else if (seen == false)
        {

            playerSeen = false;

        }
    }

    public void setInRange(bool inRange)
    { // sets whether or not the player is in shooting range or not


        if (inRange == true)
        {

            playerInShootingRange = true;
        }

        else if (inRange == false)
        {

            playerInShootingRange = false;
            StopCoroutine("enemyShoot"); ;
            walkCycle.SetBool("isShooting", false);
            shooting = false;

        }



    }

    public void shootAtPlayer()
    { // function to shoot at enemy

        Vector3 dirToTarget = (playerTracker.transform.position - transform.position).normalized;
        float targetDistance = Vector3.Distance(transform.position, playerTracker.transform.position);

        if (Physics.Raycast(transform.position, dirToTarget, targetDistance,targetMask))
        {
            if(playerSeen == true && playerInShootingRange == true)
            {

                Debug.Log("hit player with raycast");

                //gm.GetComponent<GM>().StartCoroutine("Fade");
                //walkCycle.SetBool("isShooting", true);
                gm.GetComponent<GM>().health -= 1;

            }

            ShootingSource.Play();

        }

        //gm.GetComponent<GM>().StartCoroutine("Fade");
        shooting = false;
        //walkCycle.SetBool("isShooting", false);
        currentState = AISTATE.PURSUE;

    }


    private void setEndRotation(Transform target)
    { // sets enemy rotation to waypoint rotation

        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * rotationSpeed);
        initialAngle = transform.rotation.eulerAngles.y;
        initialRotation = sourceRotation = transform.rotation;
    }

    IEnumerator enemyShoot()
    { // coroutine for shooting with delay

        shooting = true;

        currentState = AISTATE.IDLE;

        ReloadSource.PlayOneShot(ReloadSource.clip);

        walkCycle.SetBool("isShooting", true);


        yield return new WaitForSeconds(shootDelayTime);

        shootAtPlayer();

        walkCycle.SetBool("isShooting", false);
        walkCycle.SetBool("isEnemyWalk", true);

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

    IEnumerator searchSound()
    {// coroutine for delay before enemy seeks sound position

        changing = true;

        if (changing == true)
        {

            yield return new WaitForSeconds(waitTime / 3);

            searching = true;

        }

    }

    IEnumerator callRotationDelay()
    {

        rotating = false;

        yield return new WaitForSeconds(rotationDelay);


        rotating = true;
    }

}

