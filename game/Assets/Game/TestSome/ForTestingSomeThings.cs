using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class ForTestingSomeThings : MonoBehaviour {

    [Header("Sprite size")]
    public bool SizeWork = false;
    private SpriteRenderer SR;
    public Vector2 Size = Vector2.zero;
    public Vector2 Borders = Vector4.zero;
    public Vector2 coefToOne = Vector2.zero;

    [Header("Clone test")]
    public GameObject ObjectForTest;
    [SerializeField]
    private GameObject test;

    private void Awake()
    {
        Debug.Log("Editor causes this Awake");
    }
    // Use this for initialization
    private void Start () {
        SR = gameObject.GetComponent<SpriteRenderer>();
        SetAboutSize();
            
	}

    private void SetAboutSize()
    {
        if (SR && SizeWork)
        {
            Size = SR.size;
            Borders = new Vector2(SR.sprite.rect.width / SR.sprite.pixelsPerUnit, SR.sprite.rect.height / SR.sprite.pixelsPerUnit);
            coefToOne.x = 1 / Size.x;
            coefToOne.y = 1 / Size.y;
        }
    }

    public void InstantiateTest()
    {
        test = Instantiate(ObjectForTest);
    }

    public void InActiveBase()
    {
        ObjectForTest.SetActive(!ObjectForTest.activeSelf);
    }

    public void InActiveTest()
    {
        test.SetActive(!test.activeSelf);
    }
    public void Changebase()
    {
        ObjectForTest = test;
        test = null;
    }





    // Update is called once per frame
    private void Update () {
        Debug.Log("Editor causes this Update");
        SetAboutSize();
    }
}
