using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBrain : MonoBehaviour
{
    //Script variables: (WE DECLARE)
    float MovementFactorX=1f;
    float MovementFactorY=1f;
    float MoveBoost=1f;
    public float Walk = 4f;
    public float Jump= 0.5f;
    public float VertJump= 0.5f;


    bool IsGrounded;

    GameObject Player;
    GameObject Avatar;
    SpriteRenderer Expression;
    public Sprite[] Faces;

    Rigidbody RB;

    float CurHealth;
    float TotalHealth = 100; 
    float CurStamina;
    float TotalStamina = 100;

    RectTransform HealthGauge;
    RectTransform StaminaGauge;

    public int CurMoney;

    TMP_Text MoneyGauge;


    // Start is called before the first frame update
    void Start()
    {
        Player=this.gameObject;
        Avatar=GameObject.Find(Player.name +"/Avatar");
        Expression=GameObject.Find(Player.name +"/Avatar/Body/Head/Face").GetComponent<SpriteRenderer>();

        RB = Player.GetComponent<Rigidbody>(); //(WE SET)

        CurHealth=0;
        HealthGauge = GameObject.Find("HealthBar/Gauge").GetComponent<RectTransform>();
        CurStamina = 0;
        StaminaGauge = GameObject.Find("StaminaBar/Gauge").GetComponent<RectTransform>();

        MoneyGauge = GameObject.Find("MoneyGauge").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        /** 
        We need to control the player status
        How the player moves
        What the player detects
        The player stamina level
        When/If the player gets damaged or healed
        */
        PlayerMovement();
        PlayerHealth();
        PlayerStamina();
        PlayerBank();
    }

    void OnCollisionStay(Collision other)
    {
        if(other.gameObject.tag == "Ground")
        {
            IsGrounded=true;
            Player.GetComponent<Animator>().SetBool("Airborn",false);
        }
    }
    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "Ground")
        {
            IsGrounded=false;
            Player.GetComponent<Animator>().SetBool("Airborn",true);

        }

    }
    void OnCollisionEnter(Collision other)
    {
        print(other.gameObject.name);

        //Cause Damage
        if(other.gameObject.tag == "Enemy")
        {
            CurHealth -= 10;
            StartCoroutine(ChangeFace(1));

        }

        //Cause Money Increase
        if(other.gameObject.tag =="Money")
        {
            CurMoney+=1;
            Destroy(other.gameObject);
        }
    }
    IEnumerator ChangeFace(int type)
    {
        yield return new WaitForSeconds(0);
        Expression.sprite = Faces[type];
        yield return new WaitForSeconds(1);
        Expression.sprite = Faces[0];

    }

    void PlayerMovement()
    {
        /**
        if DirX=0, then we are not pressing any buttons
        if DirX +greater than 0, then we are pressing R button
        if DirX -greater than 0, then we are pressing L button
       
        in other words, if DirX is NOT 0, we are wanting to move
        */

        var AC = Player.GetComponent<Animator>();
        var DirX = Input.GetAxis("Horizontal");
        var DirY = Input.GetAxis("Jump");
        var BoostX = Input.GetAxis("Fire3");
       
        //locks the player in place
        Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, 0);
        Player.transform.Rotate(0, 0, 0, Space.Self);

        AC.SetFloat("CurStamina", CurStamina);
        print(AC.GetFloat("CurStamina"));
        //This Controls Walking
        if(DirX != 0)
        {
            if(CurStamina > TotalStamina * -1)
            {
               //Player.transform.position += new Vector3((MoveBoost * Walk * DirX) * MovementFactorX, 0, 0) * Time.deltaTime;
               RB.AddForce( new Vector3((MoveBoost * Walk * DirX) * MovementFactorX, 0, 0), ForceMode.VelocityChange);
            }
            AC.SetFloat("WalkS",MoveBoost);

            if(MovementFactorX!=0)
            {
                AC.SetBool("Walk",true);
            }
            if(MovementFactorX==0)
            {
                AC.SetBool("Walk",false);
                MoveBoost=1;
            }

            if(BoostX!=0)
            {
                MoveBoost=2;
            }
            else
            {
                MoveBoost=1;
            }

            if(DirX>0)
            {
                Avatar.transform.eulerAngles= new Vector3(0,0,0);
            }
            if(DirX<0)
            {
                Avatar.transform.eulerAngles= new Vector3(0,180,0);
            }

           

        }
        else
        {
            AC.SetBool("Walk", false);
        }

        //This Controls Jumping
        if(DirY != 0)
        {
            if (MovementFactorY != 0)
            {
                AC.SetBool("Jump", true);
                RB.AddForce(new Vector3(0, (MoveBoost * Jump * DirY) * MovementFactorY, 0), ForceMode.VelocityChange);
            }
            if (MovementFactorY==0)
            {
                AC.SetBool("Jump",false);
            }
        }
        else
        {
            AC.SetBool("Jump",false);
           
        }

    }

    void PlayerHealth()
    {
        //Example: full health would mean the Right value in inspector would equal zero
        //We start at 100 health. So 100-100=0.
        
        HealthGauge.offsetMax = new Vector2(CurHealth,0);

        if(CurHealth >= TotalHealth * 100)
        {
            print("GameOver");
        }
        if (CurHealth == TotalHealth * -1)
        {
            StartCoroutine(ChangeFace(3));
        }
    }
    void PlayerStamina()
    {
        bool UsingJump;
        bool UsingRun;
        var DirY = Input.GetAxis("Jump");
        var BoostX = Input.GetAxis("Fire3");

        if(DirY != 0){UsingJump=true;}else{UsingJump=false;}
        if(BoostX != 0){UsingRun=true;}else{UsingRun=false;}

        if(CurStamina <= 0)
        {
            if(!UsingJump || !UsingRun)
            {
                CurStamina += 0.5f;
            }
            if(DirY!=0){MovementFactorY=0f;}
            if(BoostX!=0){MovementFactorX=0f;}else{MovementFactorX=1f;}
        }
        if(CurStamina >= TotalStamina * -1)
        {
            if (UsingRun)
            {
                CurStamina -= 1f;
            }

            if (UsingJump)
            {
                CurStamina-=0.7f;
            }
            
            if(DirY!=0){MovementFactorY=1f;}
            if(BoostX!=0){MovementFactorX=1f;}
        }

        
        StaminaGauge.offsetMax = new Vector2(CurStamina,0);

    }
    void PlayerBank()
    {
        MoneyGauge.text = CurMoney.ToString(); 

    }
}
