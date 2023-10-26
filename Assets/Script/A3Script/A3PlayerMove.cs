using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A3PlayerMove : MonoBehaviour
{
    public List<Transform> nextpoint;
    public Animator aim;
  [SerializeField] private float MoveSpeed = 2;
    int index = 0;
    private Tweener tweener;
    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
        tweener.AddTween(transform, transform.position, nextpoint[index].position, MoveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }
    public void PlayerMove()
    {
        if(Vector3.Distance(transform.position,nextpoint[index].position)==0)
        {
            index++;
            
            if (index > 3)
                index = index % 4;
            switch (index)
            {
                case 0:
                    aim.SetTrigger("Right");
                    break;
                case 1:
                    aim.SetTrigger("Down");
                    break;
                case 2:
                    aim.SetTrigger("Left");
                    break;
                case 3:
                    aim.SetTrigger("Up");
                    break;
            }
            tweener.AddTween(transform, transform.position, nextpoint[index].position, MoveSpeed);
        }
    }
}
