using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    public Text ScoreText, TimeText;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("score",0);
        //PlayerPrefs.SetInt("min", 0);
        //PlayerPrefs.SetInt("sec",0);
        //PlayerPrefs.SetInt("msecStr", 0);
        ScoreText.text = "Score:" + PlayerPrefs.GetInt("score");
        TimeText.text = "Time:"+string.Format("{0:D2}:{1:D2}:{2:D2}", PlayerPrefs.GetInt("min"), PlayerPrefs.GetInt("sec"), PlayerPrefs.GetInt("msecStr"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Level1()
    {
        SceneManager.LoadScene(1);
    }
    public void Level2()
    {
        SceneManager.LoadScene(2);
    }
}
