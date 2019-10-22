using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        other.gameObject.tag = "Hidden";
        other.gameObject.layer = 10;

    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.tag = "Player";
        other.gameObject.layer = 8;
    }

}
