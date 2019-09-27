using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour {

    public float LineWidth;
    public float LineOffset;
    public bool end;
    public GameObject Player;
    private PlayerController Pc;
    //public GameObject HpBar;
    public GameObject FinalPanel;
    //private Animator FinalPanelAnimator;
    public GameObject Points;
    private GameSpeedIncreaser GSI;
    public GameObject TapPanel;
    public GameObject PausePanel;
    private SpawnStuff spawnStuff;
    public GameObject BaseStuffObj;
    public State GameState;
    public Text UnPauseTimer;
    public int UnPauseSeconds;
    public float StuffStartSpeed;
    public static Settings settings;

    [Header("Bubble settings")]
    public GameObject Bubble;
    public Sprite[] BubblesSprites;
    public float[] Speed;

    public enum State
    {
        Play,
        Pause
    }


    public delegate void MethodContainer();
    public event MethodContainer onPause;
    public event MethodContainer onUnPause;
    //settings.onPause += Handler.Handle;

    // Use this for initialization
    void Start () {
        settings = this;
        GameState = State.Play;
        Pc = Player.GetComponent<PlayerController>();
        FinalPanel.SetActive(false);
        GSI = Camera.main.GetComponent<GameSpeedIncreaser>();
        spawnStuff = Camera.main.GetComponent<SpawnStuff>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        bubbleButt.Bubble = this.Bubble;
        bubbleButt.BubblesSprites = this.BubblesSprites;
        bubbleButt.Speed = this.Speed;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        if (scene.name == "Main")
            Camera.main.GetComponent<GameSpeedIncreaser>().settings.Restart();
        Debug.Log(mode);
    }
    /*

    void Awake()
    {
        Debug.Log("Settings is awake");
        if (Pc != null)
        {
            Debug.Log("aaaand it isn't null");
            Restart();
        }
            
    }*/
	
	// Update is called once per frame
	//void Update () {}
    
    public void EndOfGame()
    {
        end = true;
        //HpBar.SetActive(false);
        Points.SetActive(false);
        FinalPanel.SetActive(true);
        FinalPanel.GetComponent<FinalPanelController>().ShowResults();
        Player.SetActive(false);
        TapPanel.SetActive(false);
        DeleteObjectsWithTag("Stuff");
        
    }

    private void DeleteObjectsWithTag(string Tag)
    {
        GameObject[] Objects;
        Objects = GameObject.FindGameObjectsWithTag(Tag);
        foreach (GameObject temp in Objects)
        {
            Destroy(temp);
        }
    }

    public void Restart()
    {
        GameState = State.Play;
        Time.timeScale = 1;
        Player.SetActive(true);
        Pc.SetStartStats();
        //HpBar.SetActive(true);
        Points.SetActive(true);
        TapPanel.SetActive(true);
        GSI.enabled = true;
        GSI.SetItBack();
        end = false;
        DeleteObjectsWithTag("Stuff");
        spawnStuff.Restart();
    }

    private void SetPause()
    {
        GameState = State.Pause;
        Time.timeScale = 0;
        //PausePanel.SetActive(true);
        if (onPause != null)
        {
            onPause();
        }
                    
    }

    private void UnPause()
    {
        GameState = State.Play;
        Time.timeScale = 1;
        //PausePanel.SetActive(false);
        if (onUnPause != null)
        {
            onUnPause();
        }
    }
    /*
        
            */
    //Pause after time
    IEnumerator PauseWithTimer(int seconds)
    {
        //PausePanel.SetActive(false);
        UnPauseTimer.gameObject.SetActive(true);
        for (int i=3;i>0;i--)
        {
            UnPauseTimer.text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }
        UnPauseTimer.text = "";
        UnPause();
        UnPauseTimer.gameObject.SetActive(false);
    }

    public void SwitchGameState()
    {
        if (GameState == State.Play)
            SetPause();
        else
            StartCoroutine(PauseWithTimer(UnPauseSeconds));
            //UnPause();
    }
}
