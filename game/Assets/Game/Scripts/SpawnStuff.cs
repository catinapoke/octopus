using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStuff : Spawner {

    [Header("Game objects")]
    public GameObject DefaultBonus;
    public GameObject DamageImmunBonus;

    [Header("Spawn config")]
    [SerializeField]
    public float PercentToBonus;
    public float SecondsToBossWave;

    [Header("Stuff generate")]
    //public GameObject BaseStuffObject;
    public Sprite[] BadSpritesSmall;
    public Sprite[] BadSpritesMedium;
    public Sprite[] BadSpritesBig;
    public Sprite[] BadSpritesBoss;
    public Sprite[] BonusSprites;
    public StuffInfo BadInfo;
   

    enum SpawnType
    {
        Random,
        Scenario
    }

    protected override void Start()
    {
        base.Start();
        LineSpeed = settings.StuffStartSpeed;
    }

    public override void SetStart()
    {
        PercentToSpawn = startPercentToSpawn;
    }

    public override float GetStartMoveSpeed()
    {
        return startPercentToSpawn;
    }

    new void SetNewSpeed(float newSpeed)
    {

        if (newSpeed < 0)
            newSpeed = 0;
        PercentToSpawn = newSpeed;
        Debug.Log(gameObject.name + "has spawn rate: " + PercentToSpawn);
    }

    GameObject GenerateRandomBadStuff(int line)
    {
        BadStuff.BadType Type = (BadStuff.BadType)Random.Range(0, 3);
        return GenerateRandomBadStuff(line, Type);
    }

    GameObject GenerateRandomBadStuff(int line, BadStuff.BadType Type)
    {
        Sprite testSprite = RandomSpriteByType(Type);
        Vector2 SpriteSize = new Vector2(testSprite.rect.width / testSprite.pixelsPerUnit, testSprite.rect.height / testSprite.pixelsPerUnit);
        GameObject test = Instantiate(BaseObj, SpawnCoordsByLine(line) - RandomOffset(SpriteSize, new Vector2(1, 1)), Quaternion.identity);
        test.GetComponent<SpriteRenderer>().sprite = testSprite;
        bubbleButt bb = test.AddComponent<bubbleButt>();
        bb.BubbleSize = (bubbleButt.Size)SupportM.ToLimit(((int)Type) + 1, 1, 3);
        bb.SpawnMode = bubbleButt.Mode.Always;
        bb.SpriteWidth = SpriteSize.x;
        test.name = "BadStuff";
        test.tag = "Stuff";
        BadStuff temp;
        temp = test.AddComponent<BadStuff>();
        temp.BadInfo = this.BadInfo;
        temp.badType = Type;
        test.GetComponent<BoxCollider2D>().size = SpriteSize;
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

    GameObject generateBonusStuff(int line, BonusStuff.BonusType type = BonusStuff.BonusType.Default)
    {
        Vector2 Line = SpawnCoordsByLine(line);
        GameObject temp = BaseObj;
        switch (type)
        {
            case global::BonusStuff.BonusType.Default:
                temp = Instantiate(DefaultBonus, Line, Quaternion.identity);
                break;
            case global::BonusStuff.BonusType.DamageImmunity:
                temp = Instantiate(DamageImmunBonus, Line, Quaternion.identity);
                break;
            case global::BonusStuff.BonusType.UnlimitedTouches: //CHANGE
                temp = Instantiate(DefaultBonus, Line, Quaternion.identity);
                break;
            
        }
        temp.SetActive(false);
        return temp;
    }

    protected override void MakeASpecial()
    {
        AddObjToQueue(EmptyLines(3));
        GameObject spawnedBonus = Instantiate(DamageImmunBonus, new Vector2(0, SpawnCoords.y), Quaternion.identity);
        spawnedBonus.SetActive(false);
        AddObjToQueue(new GameObject[]{ null, null, spawnedBonus, null, null});
        AddObjToQueue(EmptyLines(2));
        AddObjToQueue(BossLines(4));
        AddObjToQueue(EmptyLines(2));
    }

    protected override Queue<GameObject[]> BossLines(int count)
    {
        Queue<GameObject[]> temp = new Queue<GameObject[]>();
        GameObject[] tempLine;
        for (int j=0;j<count;j++)
        {
            tempLine = new GameObject[5];
            for (int i = 0; i < 5; i++)
                tempLine[i] = GenerateRandomBadStuff(i - 2, global::BadStuff.BadType.Boss);
            temp.Enqueue(tempLine);
        }
        return temp;
    }

    protected override Vector2 SpawnCoordsByLine(int line)
    {
        return new Vector2((lineSize + lineOffset) * line, SpawnCoords.y); //5.5
    }

    public override GameObject GenerateObjOnLine(int line)
    {
        if (Random.value <= (PercentToBonus / 100))
            return generateBonusStuff(line);
        else
            return GenerateRandomBadStuff(line);
    }

    public override GameObject MakeNiceObject(GameObject obj)
    {
        throw new System.NotImplementedException();
    }

    protected override void AdditionalShit()
    {
        return;
    }
}
