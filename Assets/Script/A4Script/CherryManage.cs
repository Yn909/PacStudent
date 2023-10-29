using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryManage : MonoBehaviour
{
    public List<Transform> Point = new List<Transform>();
    public GameObject cherry;
   public float CountTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InitCherry();
    }
    public void InitCherry()
    {
        CountTime += Time.deltaTime;
        if (CountTime > 15)
        {
            CountTime = 0;
           GameObject obj =Instantiate(cherry);
            int n = Random.Range(0, 3);
            obj.transform.position=Point[n].transform.position;
            obj.transform.parent = Point[n].transform;
        }
    }
}
