using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFish : Spawner {

    [SerializeField]
    private float[] Percents;
    private float percentsSum;
    [SerializeField]
    private GameObject[] Fish;

   

    protected override void AdditionalShit()
    {
        percentsSum = SupportM.Sum(Percents);
    }

    int ChooseFish(float num)
    {
        float percent_type = 0.0f;
        for (int i = 0; i < Percents.Length; i++)
        {
            percent_type += Percents[i];
            if (num < percent_type)
                return i;
        }
        return 0;
    }

    public override GameObject GenerateObjOnLine(int line)
    {
        bool right = (Random.Range(0, 2) == 1) ? true : false;
        float type = Random.Range(0, percentsSum);
        int num = ChooseFish(type);
        GameObject pr = Instantiate(Fish[num], SpawnCoordsByLine(line, right), Quaternion.identity);
        pr.GetComponent<SpriteRenderer>().flipX = !right;
        FishController fc = pr.GetComponent<FishController>();
        fc.CoefRight(right);
        fc.SetParentSpawner(this);
        pr.SetActive(false);
        return pr;
        
    }

    protected override IEnumerator SpawningSpecial()//it will be bonus
    {
        yield break;
        /*
        protected IEnumerator SpawningSpecial()
        {
            while (true)
            {
                if (settings.end)
                    yield return null;
                yield return new WaitForSeconds(SecondsToSpecial);
                MakeASpecial();
            }
        }*/
    }

    protected override void MakeASpecial()
    {
        Debug.Log("SPESHOL");
        return;
        throw new System.NotImplementedException();
    }

    public override GameObject MakeNiceObject(GameObject obj)
    {
        throw new System.NotImplementedException();
    }

    protected Vector2 SpawnCoordsByLine(int line, bool right)
    {
        return new Vector2(SpawnCoords.x * (1 - SupportM.bool2int(right) * 2), SpawnCoords.y);
    }

    protected override Vector2 SpawnCoordsByLine(int line)
    {
        return SpawnCoordsByLine(line, false);
    }

    
}
