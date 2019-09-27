using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleButt : MonoBehaviour {
    public static GameObject Bubble;
    public static Sprite[] BubblesSprites;
    public static float[] Speed;
    public float coefToPercent = 0.05f;
    public float time = 1.5f;
    public float SpriteWidth;
    public Mode SpawnMode;
    public Size BubbleSize;
    public Settings settings;

    public enum Mode
    {
        onCall,
        Always  
    }

    public enum Size
    {
        Small = 1,
        Medium = 2,
        Big = 3
    }

	void Start () {
        settings = GameObject.FindGameObjectWithTag("Settings").GetComponent<Settings>();
	}
	
	void Update () {
        if (SpawnMode == Mode.Always && settings.GameState == Settings.State.Play)
            TryToSpawn();
        
	}

    public void TryToSpawn()
    {
        if (Random.Range(0,1.0f) <= coefToPercent)
        {
            GameObject temp = spawnBubble();
            Destroy(temp, time);
        }
    }

    GameObject spawnBubble()
    {
        Vector2 tempVector = gameObject.transform.position;
        tempVector.x += (Random.value - 0.5f) * SpriteWidth;
        GameObject temp = Instantiate(Bubble, tempVector, Quaternion.identity);
        int tempNum = Random.Range(0,(int)BubbleSize);
        temp.GetComponent<SpriteRenderer>().sprite = BubblesSprites[tempNum];
        temp.GetComponent<bubbleController>().SetStartSpeed(Speed[tempNum]);
        return temp;
    }
}
