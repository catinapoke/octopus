using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsChecker : MonoBehaviour {
    public GameObject Player;
    PlayerController pc;
    Text Points;
	// Use this for initialization
	void Start () {
        pc = Player.GetComponent<PlayerController>();
        Points = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        Points.text = pc.points.ToString();
	}
}
