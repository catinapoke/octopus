using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipePlayer : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 direction;

    public Text m_Text;
    string message;
    private float dragDistance;
    public GameObject player;
    public float percent;
    PlayerController playerController;
    public float TapCooldown;
    private bool canTap;
    public float accur;
    //float Timer;
    float previousX;
    public GameObject Area;
    private ClickAreaController AreaController;
    //private bool tap;


    // Use this for initialization
    void Start()
    {
        dragDistance = Screen.width * percent / 100; //dragDistance это 20% высоты экрана
        playerController = player.GetComponent<PlayerController>();
        AreaController = Area.GetComponent<ClickAreaController>();
        canTap = true;
        //Timer = TapCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        m_Text.text = "Touch : " + message + "in direction" + direction;

        // Track a single touch as a direction control.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on TouchPhase
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    startPos = touch.position;
                    message = "Begun ";
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Stationary:
                    message = "Stationary ";
                    //tap = true;
                    break;

                case TouchPhase.Canceled:
                    message = "Canceled ";
                    break;

                case TouchPhase.Moved:
                    // Determine direction by comparing the current touch position with the initial one
                    direction = touch.position - startPos;
                    message = "Moving ";
                    break;

                case TouchPhase.Ended:
                    // Report that the touch has ended when it ends
                    
                    if (direction.magnitude <= accur)
                        Tap();
                    else if (direction.magnitude>dragDistance)
                    {
                        if (Math.Abs(direction.x) > Math.Abs(direction.y)) // то это горизонтальный свап
                        {
                            if (direction.x > 0)
                            {
                                Debug.Log("Swipe Right");
                                SwipeRight();
                            }
                            else
                            {
                                Debug.Log("Swipe Left");
                                SwipeLeft();
                            }
                        }
                        else
                        {
                            if (direction.y > 0)
                            {
                                Debug.Log("Swipe Up");
                                SwipeUp();
                            }
                            else
                            {
                                Debug.Log("Swipe Down");
                            }
                        }
                    }
                    direction = Vector2.zero;
                    message = "Ending ";
                    break;
            }
        }
    }

    private void Tap()
    {
        if (canTap)
        {
            Debug.Log("Tap");
            canTap = false;
            StartCoroutine(AllowTap(TapCooldown));
            AreaController.TurnOffOutline();
            if (AreaController.wasOnOkayArea())
            {
                Debug.Log("Tap on green area");
                playerController.SetGoingUp();
            }
                
        }
    }

    IEnumerator AllowTap(float time)
    {
        yield return new WaitForSeconds(time);
        canTap = true;
        AreaController.TurnOnOutline();
    }

    private void SwipeRight()
    {
        playerController.ChangeLine(true);
    }

    private void SwipeLeft()
    {
        playerController.ChangeLine(false);
    }

    private void SwipeUp()
    {
        
    }

}
