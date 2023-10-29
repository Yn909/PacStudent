using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A3AudioPlay : MonoBehaviour
{
    public AudioClip StartAU, NomalAu;
    private new AudioSource audio;
    private bool iSPlayStartAU = false,iSplay=false;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = StartAU;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GamePanel.GetStart() == false&&iSplay==false)
        {
            audio.clip = StartAU;
            if(!audio.isPlaying)
            audio.Play();
        }
        if(GamePanel.GetStart() == true&&iSplay==false)
        {
            audio.Stop();
            iSplay = true;
        }
        if (!audio.isPlaying&& iSPlayStartAU==false)
        {
            audio.clip = NomalAu;
            audio.Play();
            iSPlayStartAU = true;
            audio.loop = true;
        }
    }
}
