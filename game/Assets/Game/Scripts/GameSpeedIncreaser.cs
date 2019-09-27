using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedIncreaser : MonoBehaviour {

    public float coef;
    public float SecondToCoef;
    private float speed;
    private float percent;
    GameObject[] SomeObjects;
    List <Stuff> SomeStuff;
    public bool DebugMode;
    private SpawnStuff spawnStuff;

    public hasSpeed[] PlayerNSlider;
    public Settings settings;


	void Start () {
        percent = 0;
        speed = 1 / SecondToCoef;
        if (DebugMode)
            Debug.Log("Speed is " + speed);
        spawnStuff = gameObject.GetComponent<SpawnStuff>();
        SomeStuff = new List<Stuff>();
        int i;
        foreach (hasSpeed temp in PlayerNSlider)
        {
            SomeStuff.Add(new Stuff(temp));
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (settings.GameState == Settings.State.Play)
        {
            percent += speed * Time.deltaTime;
            if (DebugMode)
                Debug.Log(percent);
            if (percent >= 1)
            {
                percent = 1;
                setNewSpeed();
                selfDisable();
                return;
            }
            setNewSpeed();
        }
        
    }

    private void setNewSpeed()
    {
        foreach(Stuff temp in SomeStuff)
        {
            if (DebugMode)
                Debug.Log("start: " + temp.StartSpeed + " now: " + Mathf.Lerp(temp.StartSpeed, temp.StartSpeed * coef, percent));
            temp.SetSpeed(Mathf.Lerp(temp.StartSpeed, temp.StartSpeed * coef, percent));
        }

        spawnStuff.spawnedObj.ForEach(delegate (GameObject tempObj) {
            if (tempObj != null)
            {
                Stuff temp = new Stuff(tempObj.GetComponent<hasSpeed>());
                if (DebugMode)
                    Debug.Log("start: " + temp.StartSpeed + " now: " + Mathf.Lerp(temp.StartSpeed, temp.StartSpeed * coef, percent));
                temp.SetSpeed(Mathf.Lerp(temp.StartSpeed, temp.StartSpeed * coef, percent));
            }
            else
                spawnStuff.spawnedObj.Remove(tempObj);
        });

    }

    public void SetItBack()
    {
        percent = 0;
        foreach (Stuff temp in SomeStuff)
        {
            temp.SetSpeed(temp.StartSpeed);
        }
    }

    private void selfDisable()
    {
        this.enabled = false;
    }
}

class Stuff
{
    public GameObject StuffObj;
    public hasSpeed Controller;
    public float StartSpeed;

    public Stuff(GameObject obj)
    {
        StuffObj = obj;
        Controller = StuffObj.GetComponent<hasSpeed>();
        if (Controller == null)
            throw new System.InvalidOperationException("Object has no 'hasSpeed object'");
        StartSpeed = Controller.GetStartMoveSpeed();
    }

    public Stuff(hasSpeed temp)
    {
        Controller = temp;
        StartSpeed = Controller.GetStartMoveSpeed();
    }

    public void SetSpeed(float speed)
    {
        Controller.SetNewSpeed(speed);
    }
}