using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Effect
{
    protected EffectType type;
    public enum EffectType
    {
        Default,
        InstantHp,
        DamageImmunity,
        UnlimitedTouches,
        Regen,
        FishBonus
    }

    protected float value;
    protected float time;
    public Effect(EffectType _type, float seconds, float _value = 0)
    {
        time = Time.time + seconds;
        type = _type;
        value = _value;
    }

    public Effect()
    { }

    public EffectType GetEffectType()
    {
        return type;
    }

    public float GetValue()
    {
        return value;
    }

    public float GetTime()
    {
        return time;
    }
}

public class BonusEffect : Effect
{
    public BonusEffect(BonusStuff.BonusType _type, float seconds)
    {
        type = BonusToBaseEffect(_type);
        time = Time.time + seconds;
    }
    public EffectType BonusToBaseEffect(BonusStuff.BonusType _type)
    {
        switch (_type)
        {
            case BonusStuff.BonusType.Default:
                return EffectType.InstantHp;
            case BonusStuff.BonusType.DamageImmunity:
                return EffectType.DamageImmunity; 
            case BonusStuff.BonusType.UnlimitedTouches:
                return EffectType.UnlimitedTouches;
        }
        return EffectType.Default;
    }
}

public class FishEffect:Effect
{
    public FishEffect(FishController.FishType _type, float seconds, float _value = 0)
    {
        type = FishToBaseEffect(_type);
        time = Time.time + seconds;
        value = _value;
    }
    public EffectType FishToBaseEffect(FishController.FishType _type)
    {
        switch (_type)
        {
            case FishController.FishType.Default:
                return EffectType.Regen;
            case FishController.FishType.Gold:
                return EffectType.Regen;
            case FishController.FishType.Bonus:
                return EffectType.FishBonus;
        }
        return EffectType.Default;
    }
}


public class PlayerController : hasSpeed {

    public GameObject Settings;
    private Settings settings;

    public int CurrentLine;
    public int points;
    [SerializeField]
    private float DownSpeed;
    [SerializeField]
    private float StartDownSpeed;
    [SerializeField]
    private float coefSpeedOnStart;
    [SerializeField]
    private float coefSpeedOnEnd;
    private float coefDifference;
    private float currentCoef;
    //From settings
    private float lineWidth;
    private float lineOffset;
    [SerializeField]
    private float JumpHeight;
    private float UpToHeight;
    private Vector2 TargetPosition;
    private float startDistance;
    public bool isMoving { get; private set; }
    [SerializeField]
    private int MaxHp;
    [SerializeField]
    private float CurrentHp;
    [SerializeField]
    private float regenSpeed;
    [SerializeField]
    private bool isRegen;
    [SerializeField]
    private float regenEndTime;
    [SerializeField]
    private bool isDead;
    [SerializeField]
    private float maxHeight;
    public Vector2 StartPosition;
    private bubbleButt bubbles;
    private List<Effect> effects;
    [SerializeField]
    private bool hasGodMode;

    // Use this for initialization
    protected override void Start() {
        settings = Settings.GetComponent<Settings>();
        lineOffset = settings.LineOffset;
        lineWidth = settings.LineWidth;
        hasGodMode = false;
        effects = new List<Effect>();
        SetStartStats();
        bubbles = gameObject.GetComponent<bubbleButt>();
        
    }

    void UnableEffect(Effect _effect)
    {
        switch (_effect.GetEffectType())
        {
            case Effect.EffectType.DamageImmunity:
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                hasGodMode = false;
                break;
            case Effect.EffectType.Regen://///////////////////////////////////////////////////////////////////////////////
                if (regenEndTime <= Time.time)
                {
                    Debug.Log("Time is "+ Time.time + "; endTime is "  + regenEndTime);
                    isRegen = false;
                    regenSpeed = 0;
                    regenEndTime = 0;
                }
                break;
        }
    }

    void SetEffect(Effect _effect)
    {
        switch (_effect.GetEffectType())
        {
            case Effect.EffectType.DamageImmunity:
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                hasGodMode = true;
                break;
            case Effect.EffectType.Regen:
                isRegen = true;
                float newregenspeed = _effect.GetValue() / _effect.GetTime();
                regenSpeed = regenSpeed>newregenspeed?regenSpeed:newregenspeed;
                regenEndTime = _effect.GetTime();
                break;
        }
    }

    private IEnumerator DeleteEffect(float time, Effect ef)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            UnableEffect(ef);
            effects.Remove(ef);
        }
    }

    // Update is called once per frame
    protected override void Update () {
        if(!isDead)
        {
            
            if (isMoving)
                if (TargetPosition == (Vector2)gameObject.transform.position)
                    isMoving = false;
                else
                {
                    gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, TargetPosition, moveSpeed * currentCoef * Time.deltaTime);
                    currentCoef = coefSpeedOnStart + coefDifference * Vector2.Distance((Vector2)gameObject.transform.position, TargetPosition) / startDistance;
                    bubbles.TryToSpawn();
                }
            else
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position,
                    new Vector2(gameObject.transform.position.x, -50), DownSpeed * Time.deltaTime);
            }

            if (CurrentHp < MaxHp)
            {
                CurrentHp += regenSpeed * Time.deltaTime;
                CurrentHp = CurrentHp > MaxHp ? MaxHp : CurrentHp;
            }
        }
        else
        {
            if (gameObject.transform.position.y>-6)
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position,
                    new Vector2(gameObject.transform.position.x, -50), DownSpeed * Time.deltaTime);
        }
        
    }

    public void SetGoingUp()
    {
        Debug.Log("Command to up");
            UpToHeight = gameObject.transform.position.y + JumpHeight;
            if (UpToHeight > maxHeight)
                UpToHeight = maxHeight;
            Debug.Log("Now target height is " + UpToHeight);
            StartMovingToTarget(new Vector2(gameObject.transform.position.x, UpToHeight));
    }

    public void ChangeLine(bool Right)
    {  
        if (Right)
        {
            Debug.Log("Command to move right");
            CurrentLine = CurrentLine < 2 ? CurrentLine + 1 : CurrentLine;
        }
        else
        {
            Debug.Log("Command to move left");
            CurrentLine = CurrentLine > -2 ? CurrentLine - 1 : CurrentLine;
        }
    }

    public void GoToStartOfLine(int lineNum)
    {
        StartMovingToTarget(new Vector2((lineWidth + lineOffset) * lineNum, StartPosition.y));
        CurrentLine = lineNum;
    }

    private void StartMovingToTarget(Vector2 target)
    {
        TargetPosition = target;
        currentCoef = coefSpeedOnStart;
        startDistance = Vector2.Distance((Vector2)gameObject.transform.position, target);
        isMoving = true;
    }

    public void TakeDamage(float Damage)
    {
        if (!hasGodMode)
        {
            CurrentHp = CurrentHp - Damage < 0 ? 0 : CurrentHp - Damage;
            CurrentHp = CurrentHp - Damage > MaxHp ? MaxHp : CurrentHp - Damage;
        }   
        if (CurrentHp <= 0)
        {
            isDead = true;
            settings.EndOfGame();
            Debug.Log("Player is dead");
        }
            
    }

    public void Die()
    {
        isDead = true;
        settings.EndOfGame();
        Debug.Log("Player is dead");
    }

    public float GetCurrentHp()
    {
        return CurrentHp;
    }
    
    public int GetMaxHp()
    {
        return MaxHp;
    }

    public int GetPoints()
    {
        return points;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject other = col.gameObject;
        Debug.Log("Collision with " + other.tag);
        if (other.tag == "Stuff")
        {
            StuffController temp = other.GetComponent<StuffController>();
            points = points + temp.Points < 0 ? 0 : points + temp.Points;
            this.TakeDamage(temp.Damage);
            if (temp.Type == StuffController.StuffType.Bonus)
            {
                BonusStuff stuf = (BonusStuff)temp;
                Effect tempEff = new BonusEffect(stuf.bonusType, stuf.EffectTime);
                SetEffect(tempEff);
                effects.Add(tempEff);
                StartCoroutine(DeleteEffect(stuf.EffectTime, tempEff));
            }
            Destroy(other);
        }
        else if (other.tag == "Fish")
        {
            Debug.Log("Fish collide");
            FishController Fc = other.GetComponent<FishController>();
            points = points + Fc.GetPoints() < 0 ? 0 : points + Fc.GetPoints();
            Effect tempEff = new FishEffect(Fc.Type, Fc.GetSeconds(), Fc.GetValue());
            SetEffect(tempEff);
            effects.Add(tempEff);
            StartCoroutine(DeleteEffect(Fc.GetSeconds(), tempEff));
            Destroy(other);
        }
    }

    

    public void SetStartStats()
    {
        DownSpeed = StartDownSpeed;
        base.SetStart();//MoveSpeed = StartMoveSpeed;
        CurrentHp = MaxHp;
        CurrentLine = 0;
        points = 0;
        isDead = false;
        currentCoef = 1;
        isMoving = false;
        coefDifference = coefSpeedOnStart - coefSpeedOnEnd;
        gameObject.transform.SetPositionAndRotation(StartPosition, Quaternion.identity);
        UpToHeight = gameObject.transform.position.y;
        effects.ForEach(delegate(Effect ef){
            UnableEffect(ef);
        });
        isRegen = false;
        regenEndTime = 0;
    }

    public void MultiplyDownSpeed(float coef)
    {
        DownSpeed *= coef;
    }
}
