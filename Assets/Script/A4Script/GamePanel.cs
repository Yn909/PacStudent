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
    public Text _DownTimeStart;
    public float _CountTime = 0;
    public int min,sec,msecStr;
    public int DownTime = 10;
    public static int score=0;
    private static bool iSStart = false;
    private bool isover = false;
    public GameObject OverPanel;
    
    public List<Image> lives = new List<Image>();
    public static int live = 2;
    public static Dictionary<string, UnityAction> EventCenter = new Dictionary<string,UnityAction>();
    public Transform p1, p2, p3, p4;
    // Start is called before the first frame update
    void Start()
    {
        AddList("UpdateScore", UpDateScore);
        StartCoroutine(DownTimeStart());
        AddList("GhostChange", () =>
        {
            StartCoroutine(MyDownTime());
        });
        AddList("GhostChange", () =>
        {
            Scard.gameObject.SetActive(true);
            StopCoroutine(MyDownTime());
            DownTime = 10;
            Scard.text = "Scared :" + DownTime.ToString();
            StartCoroutine(MyDownTime());
        });
        AddList("Over", () =>
        {
            if (live == 0)
            {
                OverPanel.SetActive(true);
                iSStart = false;
                Invoke("LoadStart", 3);
                Fire("Gameover");
            }
            lives[live].gameObject.SetActive(false);
            live--;
          
        });
    }
    public void LoadStart()
    {
        if (score > PlayerPrefs.GetInt("score"))
        {
            PlayerPrefs.SetInt("min", min);
            PlayerPrefs.SetInt("sec", sec);
            PlayerPrefs.SetInt("msecStr", msecStr);
            PlayerPrefs.SetInt("score", score);
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void UpDateScore()
    {
        Score.text = "Score:"+ score.ToString();
    }
    public static void AddList(string name,UnityAction ac)
    {
        if (EventCenter.ContainsKey(name))
        {
            EventCenter[name] += ac;
        }
        else EventCenter.Add(name, ac);
       
    }
    public static void RemoveList(string name,UnityAction ac)
    {
        if (EventCenter.ContainsKey(name))
        {
            EventCenter[name] -= ac;
        }
    }
    public static void Fire(string name)
    {
        if(EventCenter.ContainsKey(name))
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
        if(p1.childCount==0&&p2.childCount==0&&p3.childCount==0&&p4.childCount==0&&isover==false)
        {
            isover = true;
            Fire("Over");
        }
    }
    public static bool GetStart() => iSStart;
    public static bool GetEnd() => iSStart;
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
    int counttime = 3;
    public IEnumerator DownTimeStart()
    {
        while(counttime>=0)
        {
            if (counttime == 0)
            {
                _DownTimeStart.text = "GO!";
                yield return new WaitForSeconds(1);
                _DownTimeStart.gameObject.SetActive(false);
                iSStart = true;
            }
            if (counttime>0)
            _DownTimeStart.text = counttime.ToString();
            counttime--;
            yield return new WaitForSeconds(1);
            
       
        }
    }
    public IEnumerator MyDownTime()
    {
       
        while(DownTime>=0)
        {
            Scard.text = "Scared :"+DownTime.ToString();
            yield return new WaitForSeconds(1);
            DownTime--;
            if (DownTime == 3)
            {
                Fire("Recover");
            }
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
        EventCenter.Clear();
        live = 2;
        RemoveList("UpdateScore", Update);
        score = 0;
    }
}
