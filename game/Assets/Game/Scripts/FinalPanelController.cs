using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalPanelController : MonoBehaviour {

    public GameObject Player;
    public Text PointsText;
    public Button RestartButton;
    private PlayerController Pc;
    private Animator animator;
    [SerializeField]
    private Settings settings;

    // Use this for initialization
    void Start () {
        Pc = Player.GetComponent<PlayerController>();
        animator = gameObject.GetComponent<Animator>();
        RestartButton.onClick.AddListener(Close);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowResults()
    {
        animator.SetBool("NeedOpen", true);
        animator.Play("In");
        PointsText.text = Pc.GetPoints().ToString();
    }

    public void Close()
    {
        Debug.Log("ButtonClicked");
        animator.SetBool("NeedOpen", false);
        animator.Play("Out");
        settings.Restart();
    }

    public void OpenScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
