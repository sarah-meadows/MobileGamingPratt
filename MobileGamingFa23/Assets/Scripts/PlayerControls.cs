using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerControls : MonoBehaviour
{
    GameObject PlayerObj;
    
    RectTransform TouchPad;
    RectTransform Toggle;


    Vector3 PosPO;
    Vector3 PosTarget;

    float DirX;
    float DirY;

    float MoveSpeed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        PlayerObj=this.gameObject;
        PosTarget=PlayerObj.transform.position;

        TouchPad = GameObject.Find("TouchPad").GetComponent<RectTransform>();
        Toggle = GameObject.Find(TouchPad.name + "/Toggle").GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {
        TouchPadCtrl();
        MovementCtrl();
    }

    void TouchPadCtrl()
    {
        //We need to get position of toggle
        //we also need to convert the max position of our toggle to 1/-1;
        Vector2 PosToggle = Toggle.localPosition;
        var XAxis=PosToggle.x;
        var YAxis=PosToggle.y;

        var CalibrateValue = Mathf.Abs(TouchPad.localScale.x);

        //if our toggle is not centered, then it is moving
        DirX = XAxis/CalibrateValue;
        DirY = YAxis/CalibrateValue;


    }

    void MovementCtrl()
    {
        /*** Roles of void: 
        *   needs to control where the player moves to 
        *   I'll need to get loc of where we click
        *   Location is determined by touchpad
        */
        Rigidbody RB = PlayerObj.GetComponent<Rigidbody>();
        RB.AddForce(new Vector3(DirX,0,DirY)*MoveSpeed);

        Debug.Log(DirX);
    }
}
