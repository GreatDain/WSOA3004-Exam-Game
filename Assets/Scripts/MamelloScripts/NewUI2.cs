using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewUI2 : MonoBehaviour
{
    public GameObject ticketBooth;
    public GameObject healthTicket;
    public GameObject objectiveTicketContainer;
    public GameObject notificationTicket;
    public GameObject blueGlass;
    public GameObject redGlass;
    public GameObject greenGlass;
    public GameObject greenGlass2;
    public bool paused = false;

    private Animator ticketBoothAnimator;
    private Animator healthTicketAnimator;
    private Animator objectiveTicketContainerAnimator;
    private Animator objectiveTicketAnimator;
    private Animator notificationTicketAnimator;
    private Animator blueGlassAnimator;
    private Animator redGlassAnimator;
    private Animator greenGlassAnimator;
    private Animator greenGlass2Animator;

    // Start is called before the first frame update
    void Start()
    {
        ticketBoothAnimator = ticketBooth.GetComponent<Animator>();
        healthTicketAnimator = healthTicket.GetComponent<Animator>();
        objectiveTicketContainerAnimator = objectiveTicketContainer.GetComponent<Animator>();
        notificationTicketAnimator = notificationTicket.GetComponent<Animator>();
        blueGlassAnimator = blueGlass.GetComponent<Animator>();
        redGlassAnimator = redGlass.GetComponent<Animator>();
        greenGlassAnimator = greenGlass.GetComponent<Animator>();
        greenGlass2Animator = greenGlass2.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowNotification();
        PauseGame();
        HealthGain();
        ToggleObjective();
        ObjectiveFalsifyer();
        NotificationFalsifier();
    }

    void HealthGain()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))//place holder for gaining 1st life
        {
            GameObject newHTicket1 = Instantiate(healthTicket, transform.position, Quaternion.identity) as GameObject;
            newHTicket1.transform.SetParent(GameObject.FindGameObjectWithTag("SpawnPoint1").transform, false);
            Invoke("FalsifySpin1", 1f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))//place holder for gaining 2nd life
        {
            GameObject newHTicket1 = Instantiate(healthTicket, transform.position, Quaternion.identity) as GameObject;
            newHTicket1.transform.SetParent(GameObject.FindGameObjectWithTag("SpawnPoint2").transform, false);
            Invoke("FalsifySpin1", 1f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))//place holder for gaining 3rd life
        {
            GameObject newHealthTicket = Instantiate(healthTicket, transform.position, Quaternion.identity) as GameObject;
            newHealthTicket.transform.SetParent(GameObject.FindGameObjectWithTag("SpawnPoint3").transform, false);
        }
    }

    //making animatior variable false 
    void FalsifySpin1()
    {
        healthTicketAnimator.SetBool("Ate", false);
    }

    void ToggleObjective()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))//place holder for toggling Escape from the cage.
        {
            objectiveTicketContainerAnimator.SetBool("Show", true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8)) //place holder for Use your new found abilities to aid in your escape. 
        {
            objectiveTicketContainerAnimator.SetBool("Show2", true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9)) //place holder for Find and eat the bananas to recover health.
        {
            objectiveTicketContainerAnimator.SetBool("Show3", true);
        }

        if (Input.GetKeyDown(KeyCode.N)) //place holder for Enter the Circus.
        {
            objectiveTicketContainerAnimator.SetBool("Show4", true);
        }

        if (Input.GetKeyDown(KeyCode.M)) //place holder for Find the main road and make your escape on a passing vehicle.
        {
            objectiveTicketContainerAnimator.SetBool("Show5", true);
        }
    }

    //invoking functions that make objective animations false
    void ObjectiveFalsifyer() 
    {
        if (objectiveTicketContainerAnimator.GetBool("Show"))
        {
            Invoke("FalsifyObjective1", 6f);
        }

        if (objectiveTicketContainerAnimator.GetBool("Show2"))
        {
            Invoke("FalsifyObjective2", 6f);
        }

        if (objectiveTicketContainerAnimator.GetBool("Show3"))
        {
            Invoke("FalsifyObjective3", 6f);
        }

        if (objectiveTicketContainerAnimator.GetBool("Show4"))
        {
            Invoke("FalsifyObjective4", 6f);
        }

        if (objectiveTicketContainerAnimator.GetBool("Show5"))
        {
            Invoke("FalsifyObjective5", 6f);
        }
    }

    void FalsifyObjective1 ()
    {
        objectiveTicketContainerAnimator.SetBool("Show", false);
    }

    void FalsifyObjective2()
    {
        objectiveTicketContainerAnimator.SetBool("Show2", false);
    }

    void FalsifyObjective3()
    {
        objectiveTicketContainerAnimator.SetBool("Show3", false);
    }

    void FalsifyObjective4()
    {
        objectiveTicketContainerAnimator.SetBool("Show4", false);
    }

    void FalsifyObjective5()
    {
        objectiveTicketContainerAnimator.SetBool("Show5", false);
    }

    void ShowNotification ()
    {
        if (Input.GetKeyDown(KeyCode.Y))// place holder for sprint gaining ability
        {
            notificationTicketAnimator.SetBool("ShowNotification1", true);
        }

        if (Input.GetKeyDown(KeyCode.U))// place holder for how to Sprint.
        {
            notificationTicketAnimator.SetBool("ShowNotification2", true);
        }

        if (Input.GetKeyDown(KeyCode.I))// place holder for gaining sneak ability
        {
            notificationTicketAnimator.SetBool("ShowNotification3", true);
        }

        if (Input.GetKeyDown(KeyCode.O))// place holder for how to sneak
        {
            notificationTicketAnimator.SetBool("ShowNotification4", true);
        }
    }

    //invoking functions that make objective animations false
    void NotificationFalsifier ()
    {
        if (notificationTicketAnimator.GetBool("ShowNotification1"))
        {
            Invoke("FalsifyNotification1", 10f);
        }

        if (notificationTicketAnimator.GetBool("ShowNotification2"))
        {
            Invoke("FalsifyNotification2", 10f);
        }

        if (notificationTicketAnimator.GetBool("ShowNotification3"))
        {
            Invoke("FalsifyNotification3", 10f);
        }

        if (notificationTicketAnimator.GetBool("ShowNotification4"))
        {
            Invoke("FalsifyNotification4", 10f);
        }
    }

    void FalsifyNotification1()//making animatior variable false (animation already hides notification)
    {
        notificationTicketAnimator.SetBool("ShowNotification1", false);
    }

    void FalsifyNotification2()
    {
        notificationTicketAnimator.SetBool("ShowNotification2", false);
    }

    void FalsifyNotification3()
    {
        notificationTicketAnimator.SetBool("ShowNotification3", false);
    }

    void FalsifyNotification4()
    {
        notificationTicketAnimator.SetBool("ShowNotification4", false);
    }


    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P))//place holder for pause
        {
            paused = true;
        }

        //showing ticket booth
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
        //falsifying animations
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

