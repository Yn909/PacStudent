using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject obj;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = GameObject.Find("Player").transform.position;
        GamePanel.AddList("Over", () =>
        {
            StartCoroutine(recover());

        });
    }
    IEnumerator recover()
    {
        yield return new WaitForSeconds(1);
        GameObject ob = Instantiate(obj);
        ob.transform.position = pos;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
