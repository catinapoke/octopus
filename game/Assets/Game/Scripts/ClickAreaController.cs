using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClickAreaController : hasSpeed {

    public Image Back;
    private RectTransform BackRect;
    private Outline BackOutLine;
    Vector2[] BackPoints;//LeftUp,RightDown 
    public Image OkayArea;
    private RectTransform OkayAreaRect;
    Vector2[] OkayAreaPoints;
    public Image Slider;
    private RectTransform SliderRect;
    [Range(0, 1)] public float percent;
    public Vector3 startPos;
    public Vector3 endPos;
    private bool right;
    public bool tempTap;
    public float min, max;
    public Vector3 prevPosition;

    // Use this for initialization
    protected override void Start () {
        BackRect = Back.GetComponent<RectTransform>();
        BackPoints = GetPoints(BackRect);
        BackOutLine = Back.GetComponent<Outline>();
        DisplayLocalCorners(BackRect);
        OkayAreaRect = OkayArea.GetComponent<RectTransform>();
        OkayAreaPoints = GetPoints(OkayAreaRect);
        SliderRect = Slider.GetComponent<RectTransform>();
        startPos = new Vector3(BackPoints[0].x, (BackPoints[0].y + BackPoints[1].y) / 2);
        endPos = new Vector3(BackPoints[1].x, (BackPoints[0].y + BackPoints[1].y) / 2);
        right = true;
        percent = 0f;
        SliderRect.localPosition = startPos;
        min = Mathf.Min(OkayAreaPoints[0].x, OkayAreaPoints[1].x);
        max = Mathf.Max(OkayAreaPoints[0].x, OkayAreaPoints[1].x);
        prevPosition = SliderRect.localPosition;
    }


    /*
     *     GetComponent<RectTransform> ().offsetMax = Vector2.Lerp (oldPositionMax, newPositionMax, percentage);
     *     GetComponent<RectTransform> ().offsetMin = Vector2.Lerp (oldPositionMin, newPositionMin, percentage);
     *     GetComponent<RectTransform> ().rotation.eulerAngles.z =  Mathf.Lerp (0f, 90f, percentage);
     */

	// Update is called once per frame
	protected override void Update () {
        MoveSlider();
        //tempTap = onOkayArea();
	}

    public void TurnOnOutline()
    {
        BackOutLine.enabled = true;
    }

    public void TurnOffOutline()
    {
        BackOutLine.enabled = false;
    }

    Vector2[] GetPoints(RectTransform rt)
    {
        Vector3[] v = new Vector3[4];
        rt.GetLocalCorners(v);
        return new Vector2[] { v[0], v[2] };
    }

    void MoveSlider()
    {
        percent += moveSpeed * Time.deltaTime;

        if (percent>=1)
            if (right)
            {
                percent = 0;
                right = false;
                //Swap
                Vector3 temp = startPos;
                startPos = endPos;
                endPos = temp;
            }
            else 
            {
                right = true;
                percent = 0;

                Vector3 temp = startPos;
                startPos = endPos;
                endPos = temp;
            }
        prevPosition = SliderRect.localPosition;
        SliderRect.localPosition = Vector2.Lerp(startPos, endPos, percent);

    }

    public bool onOkayArea()
    {
        Debug.Log("Slider on" + SliderRect.localPosition);
        Debug.Log(min + " " + max);
        if (SliderRect.localPosition.x >= min && SliderRect.localPosition.x <= max )
            return true;
        return false;
    }

    public bool wasOnOkayArea()
    {
        if (prevPosition.x >= min && prevPosition.x <= max)
            return true;
        return false;
    }

    void DisplayLocalCorners(RectTransform rt)
    {
        Vector3[] v = new Vector3[4];

        //rt.rotation = Quaternion.AngleAxis(45, Vector3.forward);
        rt.GetLocalCorners(v);

        Debug.Log("Local Corners of " + rt.gameObject.name);
        for (var i = 0; i < 4; i++)
        {
            Debug.Log("Local Corner " + i + " : " + v[i]);
        }
    }
}
