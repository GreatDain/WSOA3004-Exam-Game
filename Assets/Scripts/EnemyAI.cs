using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 startingPos;

    float radius;

    public GameObject playerTracker;

    Vector3 playerPos;

    bool playerInRange = false;

    float speed = 5f;

    void Start()
    {
        startingPos = gameObject.transform.position;

        radius = gameObject.GetComponent<SphereCollider>().radius;

    }
        // Update is called once per frame
        void Update()
        {

        if (playerInRange == true) {

            trackPlayer();

        }

        }

        void OnTriggerEnter(Collider Player)
        {

            playerTracker = Player.gameObject;

            playerInRange = true;
            
        }


        void OnTriggerExit(Collider Player)
        {

            playerTracker = null;

        playerInRange = false;


        }

        public void trackPlayer() {

            playerPos = playerTracker.gameObject.transform.position;

        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position,playerPos,step);

        }

    }

