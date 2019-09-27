using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePlayer : MonoBehaviour {

    public Vector2 startPos;
    public Vector2 direction;
    public Text m_Text;
    string message;
    public float accur;

    public GameObject[] LineButtons;
    public GameObject SettingsObj;
    Settings settings;
    PlayerController playerController;
    [SerializeField]
    private int offset;
    public float TapCooldown;
    public GameObject Area;
    private ClickAreaController AreaController;
    private bool canTap;

    // Use this for initialization
    void Start () {
        
        settings = SettingsObj.GetComponent<Settings>();
        playerController = settings.Player.GetComponent<PlayerController>();
        AreaController = Area.GetComponent<ClickAreaController>();
        offset = LineButtons.Length / 2;
        canTap = true;
        m_Text.text = "Debuuuuuug";
        direction = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on TouchPhase
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    m_Text.text = "Touch : Begun"  + "in direction" + direction;
                    startPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    // Determine direction by comparing the current touch position with the initial one
                    direction = touch.position - startPos;
                    m_Text.text = "Touch : Moved" + "in direction" + direction;
                    break;

                case TouchPhase.Ended:
                    // Report that the touch has ended when it ends
                    m_Text.text = "Touch : Ended" + "in direction" + direction;
                    if (direction.magnitude <= accur)
                        HandleHitInPoint(startPos);
                    direction = Vector2.zero;
                    break;
            }
        }
    }


    void HandleHitInPoint(Vector3 Hit)
    {
        m_Text.text = "Tap in " + Hit;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Hit);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        //RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        RaycastHit2D[] allHits = Physics2D.RaycastAll(mousePos2D, Vector2.zero); /// CHEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEECK
        int temp = ButtonNum(FindButtonInHits(allHits)[0]);
        if (temp > -1)
            ButtonPress(temp - offset);
    }

    GameObject[] FindButtonInHits(RaycastHit2D[] allHits)//Впринципи можна сейчас не хранить, но мб потом нужно будет
    {
        List<GameObject> Buttons = new List<GameObject>();
        foreach(RaycastHit2D hit in allHits)
        {
            Debug.Log("Hits with "+hit.transform.name);
            if (hit.transform.tag == "LineButton")
            {
                Buttons.Add(hit.transform.gameObject);
                Debug.Log("added to list");
            }
                
        }
        return Buttons.ToArray();
    }

    int WhatHitTouches(RaycastHit2D hit)
    {
        for (int i = 0; i < LineButtons.Length; i++)
        {
            if (hit.collider.IsTouching(LineButtons[i].GetComponent<BoxCollider2D>()))
                return i;
        }
        return -1;
    }

    //check equals with buttons
    int ButtonNum(GameObject obj)
    {
        if (obj == null)
            return - 1;
        Debug.Log("Check button num with name: "+obj.name);
        for (int i = 0; i < LineButtons.Length; i++)
        {
            if (obj == LineButtons[i])
                return i;
        }
        return -1;
    }

    //Handle button press
    void ButtonPress(int num)
    {
        Debug.Log("Button pressed with num "+num);
        canTap = false;
        StartCoroutine(AllowTap(TapCooldown));
        AreaController.TurnOffOutline();
        if (AreaController.wasOnOkayArea())
        {
            if (!playerController.isMoving)
            {
                if (num == playerController.CurrentLine)
                    playerController.SetGoingUp();
                else
                    playerController.GoToStartOfLine(num);
            }
        }
    }

    //Allow tap after time
    IEnumerator AllowTap(float time)
    {
        yield return new WaitForSeconds(time);
        canTap = true;
        AreaController.TurnOnOutline();
    }
}
