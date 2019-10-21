using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundDetect : MonoBehaviour
{
    public bool onGrass;
    public bool onGravel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Grass")
        {
            onGrass = true;
        }

        if (other.gameObject.tag == "Gravel")
        {
            onGravel = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Grass")
        {
            onGrass = false;
        }

        if (other.gameObject.tag == "Gravel")
        {
            onGravel = false;
        }
    }
}
