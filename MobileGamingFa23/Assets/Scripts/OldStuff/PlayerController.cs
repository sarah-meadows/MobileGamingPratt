using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //we CREATE the variables
    GameObject Player;
    Animator AC;
    Rigidbody2D  RB;
    
    public float WalkSpeed;


    // Start is called before the first frame update
    void Start()
    {
        //now we SET the variables
        Player = this.gameObject;
        AC = Player.GetComponent<Animator>();
        RB = Player.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Call your custom void here:
        PlayerMove();
    }

    void PlayerMove()
    {
        //DirX=+1 when moving Right; DirX=-1 when moving left
        var DirX = Input.GetAxis("Horizontal");

        RB.AddForce(new Vector2(DirX * WalkSpeed,0), ForceMode2D.Impulse);
        



    }
}
