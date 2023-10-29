using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    Tweener tweener;
    Transform taget;
    public void Start()
    {
      switch(transform.parent.name)
        {
            case "Point1":
                taget = GameObject.Find("Point3").transform;
                break;
            case "Point2":
                taget = GameObject.Find("Point4").transform;
                break;
            case "Point3":
                taget = GameObject.Find("Point1").transform;
                break;
            case "Point4":
                taget = GameObject.Find("Point2").transform;
                break;
        }
        Destroy(gameObject,15f);
        tweener = GetComponent<Tweener>();
        tweener.AddTween(transform, transform.position,taget.position, 10);
    }

}
