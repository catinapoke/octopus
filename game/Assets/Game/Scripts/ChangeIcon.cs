using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeIcon : MonoBehaviour {

    public Sprite One;
    public Sprite Two;
    Image img;
    public bool isOne;
    void Start()
    {
        isOne = false;
        img = gameObject.GetComponent<Image>();
    }
	public void Change()
    {
        if (isOne)
            img.sprite = Two;
        else
            img.sprite = One;
        isOne = !isOne;
    }
}
