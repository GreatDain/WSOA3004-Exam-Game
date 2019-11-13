using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewUI : MonoBehaviour
{
    public GameObject ticketBooth;
    public GameObject healthTicket;
    public GameObject blueGlass;
    public GameObject redGlass;
    public GameObject greenGlass;
    public GameObject greenGlass2;
    public bool paused = false;

    private Animator ticketBoothAnimator;
    private Animator healthTicketAnimator;
    private Animator blueGlassAnimator;
    private Animator redGlassAnimator;
    private Animator greenGlassAnimator;
    private Animator greenGlass2Animator;

    // Start is called before the first frame update
    void Start()
    {
        ticketBoothAnimator = ticketBooth.GetComponent<Animator>();
        healthTicketAnimator = healthTicket.GetComponent<Animator>();
        blueGlassAnimator = blueGlass.GetComponent<Animator>();
        redGlassAnimator = redGlass.GetComponent<Animator>();
        greenGlassAnimator = greenGlass.GetComponent<Animator>();
        greenGlass2Animator = greenGlass2.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //HealthLoss();
        PauseGame();
    }

    /*void HealthLoss()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            healthTicketAnimator.SetBool("Shot", true);
   
        }
    }*/

    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P))//place holder for pause
        {
            paused = true;
        }

        if (paused == true)
        {
            ticketBooth.SetActive(true);
            ticketBoothAnimator.SetBool("Up", true);
            ticketBoothAnimator.SetBool("Down", false);
            TicketInteraction();
        }
    }

    void TicketInteraction ()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //out storing info from raycast in hit variable without returning it
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                if (hit.collider.tag == "ResumeTicket")
                {
                    blueGlassAnimator.SetBool("ResumeHover", true); 

                    //resuming
                    if (Input.GetMouseButtonDown(0))
                    {
                        paused = false;
                        ticketBoothAnimator.SetBool("Up", false);
                        ticketBoothAnimator.SetBool("Down", true);
                        Invoke("HideTicketBooth", 1f);
                    }
                }

                //showing controls
                if (hit.collider.tag == "ControlsTicket")
                {
                    blueGlassAnimator.SetBool("ControlsHover", true);

                    if (Input.GetMouseButtonDown(0))
                    {
                        blueGlassAnimator.SetBool("uSure", true);
                        greenGlassAnimator.SetBool("ShowControls", true);
                    }
                }

                //hiding controls
                if (hit.collider.tag == "BackToPauseTicket")
                {
                    greenGlassAnimator.SetBool("BackToPauseHover", true);

                    if (Input.GetMouseButtonDown(0))
                    {
                        greenGlassAnimator.SetBool("ShowControls", false);
                        blueGlassAnimator.SetBool("uSure", false);
                    }
                }

                //showing tips
                if (hit.collider.tag == "TipsTicket")
                {
                    greenGlassAnimator.SetBool("TipsHover", true);

                    if (Input.GetMouseButtonDown(0))
                    {
                       greenGlassAnimator.SetBool("ShowControls", false);
                       greenGlass2Animator.SetBool("ShowTips", true);
                    }
                }

                //hiding tips
                if (hit.collider.tag == "BackToControlsTicket")
                {
                    greenGlass2Animator.SetBool("BackToControlsHover", true);

                    if (Input.GetMouseButtonDown(0))
                    {
                        greenGlassAnimator.SetBool("ShowControls", true);
                        greenGlass2Animator.SetBool("ShowTips", false);
                    }
                }

                //quiting?
                if (hit.collider.tag == "QuitTicket")
                {
                    blueGlassAnimator.SetBool("QuitHover", true);

                    if (Input.GetMouseButtonDown(0))
                    {
                        blueGlassAnimator.SetBool("uSure", true);
                        redGlassAnimator.SetBool("uSure", true);
                    }
                }

                //not quiting
                if (hit.collider.tag == "NoTicket")
                {
                    redGlassAnimator.SetBool("NoHover", true);

                    if (Input.GetMouseButtonDown(0))
                    {
                        blueGlassAnimator.SetBool("uSure", false);
                        redGlassAnimator.SetBool("uSure", false);
                    }
                }

                //yes quiting
                if (hit.collider.tag == "YesTicket")
                {
                    redGlassAnimator.SetBool("YesHover", true);

                    if (Input.GetMouseButtonDown(0))
                    {
                        //change scene
                    }
                }
            }
        }
        else
        {
            blueGlassAnimator.SetBool("ResumeHover", false);
            blueGlassAnimator.SetBool("ControlsHover", false);
            greenGlassAnimator.SetBool("BackToPauseHover", false);
            greenGlassAnimator.SetBool("TipsHover", false);
            greenGlass2Animator.SetBool("BackToControlsHover", false);           
            blueGlassAnimator.SetBool("QuitHover", false);
            redGlassAnimator.SetBool("NoHover", false);
            redGlassAnimator.SetBool("YesHover", false);
        }
    }

    void HideTicketBooth()
    {
        ticketBooth.SetActive(false);
    }
}

