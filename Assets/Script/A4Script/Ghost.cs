using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Animator aim;
    public List<Transform> WayPoint = new List<Transform>();
    public int remainder;
    int index = 0;
    public float time = 0.1f;
    private Tweener tweener;
    bool isstart = false;
    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
        aim = GetComponent<Animator>();

        GamePanel.AddList("GhostChange",()=> {
            aim.SetBool("Scared",true);
            aim.SetBool("R", false);
            aim.SetBool("D", false);
            aim.SetBool("U", false);
            aim.SetBool("L", false);
            aim.SetBool("Dead", false);
            aim.SetBool("Recover", true);
        });
        GamePanel.AddList("Recover", () => {
            aim.SetBool("Scared", false);
            aim.SetBool("R", false);
            aim.SetBool("D", false);
            aim.SetBool("U", false);
            aim.SetBool("L", false);
            aim.SetBool("Dead", false);
            aim.SetBool("Recover", true);
            Invoke("CutState", 3);
        });
    }


     public void CutState()
    {
        aim.SetBool("Recover", false);
        aim.SetBool("Scared", false);
        aim.SetBool("R", false);
        aim.SetBool("D", true);
        aim.SetBool("U", false);
        aim.SetBool("L", false);
        aim.SetBool("Dead", false);
        
    }
      

    void Move()
    {
        tweener.AddTween(transform, transform.position, WayPoint[index].position, time);
    }
    // Update is called once per frame
    void Update()
    {
        if (GamePanel.GetStart() == false)
            return;
     if(isstart==false&&GamePanel.GetStart()==true)
        {
            isstart = false;
            Move();
        }
        if (Vector3.Distance(transform.position,WayPoint[index].position)==0)
        {
            index++;
            if(index>WayPoint.Count-1)
            {
                index = index % WayPoint.Count;
                index += 2;
            }
            tweener.AddTween(transform, transform.position, WayPoint[index].position, time);
        }
    }
}
