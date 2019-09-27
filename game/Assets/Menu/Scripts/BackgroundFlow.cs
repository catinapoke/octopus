using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFlow : MonoBehaviour {

    [SerializeField]
    private GameObject LineCheck;

    [Header("Spawn config")]
    [SerializeField]
    private float startPercentToSpawn;
    public float PercentToSpawn;
    public float PercentToBonus;
    public float HeightToSpawn;

    [Header("Stuff generate")]
    public GameObject BaseStuffObject;
    public GameObject Bubble;
    public float[] Speed;
    public Sprite[] BubblesSprites;
    public Sprite[] BadSpritesSmall;
    public Sprite[] BadSpritesMedium;
    public Sprite[] BadSpritesBig;
    public Sprite[] BadSpritesBoss;
    public LineController lineController;
    //Private
    private float lineWidth;
    private float lineOffset;
    [SerializeField]
    private Queue<GameObject[]> stuffQueue;
    private GameObject[] EmptyLine = new GameObject[5];

    void Start () {
        bubbleButtMenu.Bubble = this.Bubble;
        bubbleButtMenu.BubblesSprites = this.BubblesSprites;
        bubbleButtMenu.Speed = this.Speed;
        PercentToSpawn = startPercentToSpawn;
        stuffQueue = new Queue<GameObject[]>();
        lineOffset = 0.1f;
        lineWidth = 1;
        lineController.EndLineEvent.AddListener(onEndingLine);
        Spawn();
    }

    void onEndingLine()
    {
        Spawn();
    }

    void Spawn()
    {
        
        GameObject[] list = GenerateStuff();

        for (int line=0;line<5;line++)
        {
            if (list[line] != null)
            {
                list[line].SetActive(true);
                Destroy(list[line],7);
            }
        }
        Instantiate(LineCheck, new Vector2(0, HeightToSpawn), Quaternion.identity);
    }

    GameObject GenerateRandomBadStuff(int line)
    {
        BadStuff.BadType Type = (BadStuff.BadType)Random.Range(0, 3);
        return GenerateRandomBadStuff(line, Type);
    }
/*
    public void Restart()
    {
        spawnedStuff = new List<GameObject>();
        stuffQueue = new Queue<GameObject[]>();
        Spawn();
    }
    */
    int Max(int a, int b)
    {
        return a > b ? a : b;
    }

    int Min(int a, int b)
    {
        return a < b ? a : b;
    }

    //inclusive low and up
    int ToLimit(int number,int low, int high)
    {
        return Max(low, Min(high, number));
    }

    GameObject GenerateRandomBadStuff(int line, BadStuff.BadType Type)
    {
        Sprite testSprite = RandomSpriteByType(Type);
        Vector2 Line = new Vector2((lineWidth + lineOffset) * line, HeightToSpawn);
        Vector2 SpriteSize = new Vector2(testSprite.rect.width / testSprite.pixelsPerUnit, testSprite.rect.height / testSprite.pixelsPerUnit);
        GameObject test = Instantiate(BaseStuffObject, Line - RandomOffset(SpriteSize, new Vector2(1, 1)), Quaternion.identity);
        test.GetComponent<SpriteRenderer>().sprite = testSprite;
        goDown sc = test.AddComponent<goDown>();
        sc.moveSpeed = 2;
        bubbleButtMenu bb = test.AddComponent<bubbleButtMenu>();
        bb.BubbleSize = (bubbleButtMenu.Size)ToLimit(((int)Type) + 1, 1, 3);
        bb.SpriteWidth = SpriteSize.x;
        test.SetActive(false);
        return test;
    }

    Sprite RandomSpriteByType(BadStuff.BadType Type)
    {
        switch (Type)
        {
            case global::BadStuff.BadType.Small:
                return BadSpritesSmall[Random.Range(0, BadSpritesSmall.Length)];
                break;
            case global::BadStuff.BadType.Medium:
                return BadSpritesMedium[Random.Range(0, BadSpritesMedium.Length)];
                break;
            case global::BadStuff.BadType.Big:
                return BadSpritesBig[Random.Range(0, BadSpritesBig.Length)];
                break;
            case global::BadStuff.BadType.Boss:
                return BadSpritesBoss[Random.Range(0, BadSpritesBoss.Length)];
                break;
        }
        return null;
    }

    Vector2 RandomOffset(Vector2 size, Vector2 border)
    {
        Vector2 test;
        float XMaxOffset = border.x - size.x;
        float YMaxOffset = border.y - size.y;
        test.x = size.x >= border.x ? 0 : Random.RandomRange(0, XMaxOffset);
        test.x = test.x - XMaxOffset / 2;
        test.y = size.y >= border.y ? 0 : Random.RandomRange(0, YMaxOffset);
        test.y = test.y - YMaxOffset / 2;
        return test;
    }

    private GameObject[] GenerateStuff()
    {
        GameObject[] temp = new GameObject[5];
        for (int line = 0; line < 5; line++)
        {
            if (Random.value <= (PercentToSpawn / 100))
                    temp[line] = GenerateRandomBadStuff(line - 2);
            else
                temp[line] = null;
        }
        return temp;
    }
}
