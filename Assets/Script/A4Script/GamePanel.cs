using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class GamePanel : MonoBehaviour
{
    public Text Score;
    public Text CountTime;
    public Button Quit_bt;
    public Text Scard;
    public float _CountTime = 0;
    public int min,sec,msecStr;
    public int DownTime = 10;
    public static int score=0;
    private bool iSStart = false;
    private bool iSGameOver=false;
    public static Dictionary<string, UnityEvent> EventCenter = new Dictionary<string, UnityEvent>();
    // Start is called before the first frame update
    void Start()
    {
      
    }
    public static void AddList(string name,UnityAction ac)
    {
        EventCenter[name].AddListener(ac);
    }
    public static void RemoveList(string name,UnityAction ac)
    {
        EventCenter[name].RemoveListener(ac);
    }
    public static void Fire(string name)
    {
        EventCenter[name]?.Invoke();
    }
    private void OnEnable()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        if (iSStart == false)
            return;
        MyTime();
    }
    public void SetStart() => iSStart = true;
    public void SetEnd() => iSStart = false;
    public void Quit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void MyTime()
    {
         msecStr =(int)(msecStr+ Time.deltaTime*100);
        
       if (msecStr > 100)
        {
            sec++;
            msecStr = 0;
        }
       if(sec>60)
        {
            min++;
            sec = 0;
        }
        CountTime.text = string.Format("{0:D2}:{1:D2}:{2:D2}", min, sec, msecStr);
    }
    public IEnumerator MyDownTime()
    {
       
        while(DownTime>=0)
        {
            Scard.text = "Scared :"+DownTime.ToString();
            yield return new WaitForSeconds(1);
            DownTime--;
            if (DownTime < 0)
            {
                Scard.gameObject.SetActive(false);
                DownTime = 10;
                yield break;
            }
                
         }
    }
    private void OnDestroy()
    {
        score = 0;
    }
}
