using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : hasSpeed {

    protected static int spawnerCount = 0;
    protected int spawnerNumber;

    [Header("Spawn config")]
    [SerializeField]
    protected float startPercentToSpawn;
    public float PercentToSpawn;
    public Vector2 SpawnCoords;
    public float SecondsToSpecial;
    public Vector2 way;

    [Header("Obj generate")]
    public GameObject BaseObj;
    public List<GameObject> spawnedObj;

    [Header("Line settings")]
    [SerializeField]
    protected float lineSize;
    [SerializeField]
    protected float lineOffset;
    [SerializeField]
    protected int lineCount;
    [SerializeField]
    protected int lineNumOffset;
    [SerializeField]
    protected GameObject lineCheck;
    [SerializeField]
    protected float LineSpeed;
    //Private
    [SerializeField]
    protected Settings settings;
    [SerializeField]
    protected LineController lineController;
    [SerializeField]
    protected Queue<GameObject[]> objQueue;
    protected GameObject[] EmptyLine;

    protected int SetLineNumOffset()
    {
        if (lineCount % 2 == 1)
            return lineCount / 2;
        else
            return lineCount / 2 - 1;
    }

    public int GetSpawnerNumber()
    {
        return spawnerNumber;
    }

    protected new virtual void Start () {
        spawnerNumber = spawnerCount;
        spawnerCount++;
        EmptyLine = new GameObject[lineCount];
        PercentToSpawn = startPercentToSpawn;
        objQueue = new Queue<GameObject[]>();
        lineNumOffset = SetLineNumOffset();
        lineController.EndLineEvent.AddListener(onEndingLine);
        lineController.spawnerNum = spawnerNumber;
        Spawn();
        StartCoroutine(SpawningSpecial());
        Debug.Log(string.Format("{0} spawner started",spawnerNumber));
    }

    protected virtual IEnumerator SpawningSpecial()
    {
        while(true)
        {
            if (settings.end)
                yield return null;
            yield return new WaitForSeconds(SecondsToSpecial);
            MakeASpecial();
        } 
    }

    public override void SetStart()
    {
        PercentToSpawn = startPercentToSpawn;
    }

    public override float GetStartMoveSpeed()
    {
        return startPercentToSpawn;
    }

    //Set new rate of spawn
    new void SetNewSpeed(float newSpeed)
    {
        if (newSpeed < 0)
            newSpeed = 0;
        PercentToSpawn = newSpeed;
        Debug.Log(gameObject.name + "has spawn rate: " + PercentToSpawn);
    }

    protected void onEndingLine()
    {
        Spawn();
    }

    protected abstract Vector2 SpawnCoordsByLine(int line);

    protected void Spawn()
    {
        if (settings.end)
            return;
        Debug.Log(gameObject.name + " Spawning process");
        if (objQueue.Count == 0)
            AddObjToQueue(GenerateLine());
        Debug.Log("Count = "  +  objQueue.Count);
        GameObject[] list = DequeueObjects();
        for (int line=0;line<lineCount;line++)
        {
            if (list[line] != null)
            {
                list[line].SetActive(true);
                spawnedObj.Add(list[line]);
            }
                
        }
        GameObject lineChecking = Instantiate(lineCheck, SpawnCoordsByLine(0), Quaternion.identity);
        LineCheckController LCC = lineChecking.GetComponent<LineCheckController>();
        LCC.spawnerNum = spawnerNumber;
        LCC.SetParentSpawner(this);
        LCC.way = this.way;
        LCC.SetNewSpeed(LineSpeed);
        spawnedObj.Add(lineChecking);
        AdditionalShit();
        Debug.Log(string.Format("{0} spawner spawn smthng", spawnerNumber));
    }

    public float GetLineSpeed()
    {
        return LineSpeed;
    }

    protected abstract void AdditionalShit();

    public void Restart()
    {
        spawnedObj = new List<GameObject>();
        objQueue = new Queue<GameObject[]>();
        Spawn();
    }

    protected GameObject GenerateObj(int line)
    {
        GameObject test = Instantiate(BaseObj, SpawnCoordsByLine(line), Quaternion.identity);
        test.SetActive(false);
        return test;
    }

    public abstract GameObject GenerateObjOnLine(int line);

    public abstract GameObject MakeNiceObject(GameObject obj);

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

    protected abstract void MakeASpecial();

    protected Queue<GameObject[]> EmptyLines(int count)
    {
        Queue<GameObject[]> temp = new Queue<GameObject[]>();
        for (int i=0;i<count;i++)
            temp.Enqueue(EmptyLine);
        return temp;
    }

    protected virtual Queue<GameObject[]> BossLines(int count)
    {
        Queue<GameObject[]> temp = new Queue<GameObject[]>();
        GameObject[] tempLine;
        for (int j=0;j<count;j++)
        {
            tempLine = new GameObject[lineCount];
            for (int i = 0; i < lineCount; i++)
                tempLine[i] = MakeNiceObject(GenerateObj(i - lineNumOffset));
            temp.Enqueue(tempLine);
        }
        return temp;
    }

    public void AddObjToQueue(GameObject[] ObjectList)
    {
        objQueue.Enqueue(ObjectList);
    }

    public void AddObjToQueue(Queue<GameObject[]> ObjectQueue)
    {
        foreach(GameObject[] objects in ObjectQueue)
            objQueue.Enqueue(objects);
    }

    protected GameObject[] DequeueObjects()
    {
        return objQueue.Dequeue();
    }

    protected GameObject[] GenerateLine()
    {
        GameObject[] temp = new GameObject[lineCount];
        for (int line = 0; line < lineCount; line++)
        {
            if (Random.value <= (PercentToSpawn / 100))
            {
                 temp[line] = GenerateObjOnLine(line - lineNumOffset);
            }
            else
            {
                temp[line] = null;
            }
        }
        return temp;
    }
}
