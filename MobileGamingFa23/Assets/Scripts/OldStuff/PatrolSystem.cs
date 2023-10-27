using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSystem : MonoBehaviour
{
    public GameObject Target;
    public GameObject[] Spots;
    public bool IsLoop; //if true, make it loop, if false go back and forth
    int SLength;
    public int CurL=0;
    public int Reverse=0; //if not a loop, 1 makes the curL go in reverse


    public float MoveSpeed;



    // Start is called before the first frame update
    void Start()
    {
        //For testing: print(this.GetComponent<Transform>().childCount);
        SLength = this.GetComponent<Transform>().childCount;
        Spots = new GameObject[SLength];

        for(int i = 0; i < SLength; i++)
        {
            Spots[i] = GameObject.Find(this.gameObject.name + "/Spot (" + i + ")");

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 Location = Spots[CurL].transform.position;
        Target.transform.position = Vector2.MoveTowards(Target.transform.position,Location,MoveSpeed*Time.deltaTime);

        if(Vector2.Distance(Target.transform.position, Location) <= 0.02f)
        {
            if(CurL+1 < SLength && Reverse==0)
            {
                CurL++;
            }
            if(CurL-1 >= 0 && Reverse==1)
            {
                CurL--;
            }
        }

        if(CurL+1 == SLength)//If surpasses array count
        {
            if(IsLoop){ CurL=0; }
            else{ Reverse=1;}
        }
        if(CurL-1 == -1 && Reverse==1)
        {
            Reverse=0;
        }
    }
}
