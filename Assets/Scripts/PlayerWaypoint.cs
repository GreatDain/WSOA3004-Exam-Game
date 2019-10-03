using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWaypoint : MonoBehaviour
{
    public RectTransform prefab;

    private RectTransform waypoint;

    private Transform player;
    private Text distanceText;

    public bool checkpoint;

    // Start is called before the first frame update
    void Start()
    {
        var canvas = GameObject.Find("WaypointCanvas").transform;

        waypoint = Instantiate(prefab, canvas);
        distanceText = waypoint.GetComponentInChildren<Text>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        checkpoint = false;
    }

    // Update is called once per frame
    void Update()
    {
        var screenPos = Camera.main.WorldToScreenPoint(transform.position);
        waypoint.position = screenPos;

        waypoint.gameObject.SetActive(screenPos.z > 0);

        distanceText.text = Vector3.Distance(player.position, transform.position).ToString("0") + " m";
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            checkpoint = true;
        }
    }
}
