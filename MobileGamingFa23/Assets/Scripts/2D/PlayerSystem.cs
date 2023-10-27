using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSystem : MonoBehaviour
{
    GameObject Player;
    Rigidbody2D RB;
    Vector2 PosMouse;
    
    float S_walk=1f;
    float ScreenSize;
    float CenterOfScreen;
    public float ClickCount;
    public float ClickTime;
    public float ClickDelay=0.5f;

    int DirFace=0;


    // Start is called before the first frame update
    void Start()
    {
        Player = this.gameObject;

        
    }

    // Update is called once per frame
    void Update()
    {
        SetVariables();//where variables you make in the script
        if(Input.GetMouseButton(0))
        {
            ClickCount++;
            if(ClickCount==1)
            {
                ClickTime+=Time.deltaTime;
            }

            if(ClickCount > 1 && Time.deltaTime - ClickTime < ClickDelay)
            {
                ClickCount=0;
                ClickTime=0;
                WalkCtrl();
            }
            else if(ClickCount>2 || Time.deltaTime - ClickTime > 1)
            {
               ClickCount=0;
                ClickTime=0;
                JumpCtrl();
            }
        }
        
    }
    void SetVariables()
    {
        PosMouse = Input.mousePosition; //Debug.Log(PosMouse.x);
        
        //lets determine where the center of our screen is
        ScreenSize = Screen.width;
        CenterOfScreen = ScreenSize/2;

        RB=Player.GetComponent<Rigidbody2D>();
    }

    void WalkCtrl()
    {
        //determine direction to go when void is called from OnPointerClick
        if(PosMouse.x<CenterOfScreen)
        {
            //walk left
            DirFace= -1;
        }
        if(PosMouse.x>CenterOfScreen)
        {
            //walk right
            DirFace=1;
        }
        RB.AddForce(new Vector2(DirFace,0) * S_walk, ForceMode2D.Impulse);
    }

    void JumpCtrl()
    {
        Debug.Log("JUMP NOW!");
        RB.AddForce(new Vector2(0,0) * S_walk, ForceMode2D.Impulse);

    }

}
