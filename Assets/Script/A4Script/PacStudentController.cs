using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    private Animator aim;
    private Tweener tweener;
    private KeyCode LastInput;
    private KeyCode CurrentInput;
  [SerializeField]  private float time = 0.1F;
    public bool iSCollionWall = true;
    public LayerMask layer;
    Vector3 startpos;
    Quaternion startqu;
    public AudioSource MoveAu,WithWallAu, EatingPelletAu;
    public ParticleSystem Dirt1, Collision1,Dirt2,Collisionl2, Dirt3, Collision13, Dirt4, Collisionl4;
    private void Awake()
    {
        MoveAu = GameObject.Find("PlayerMove").GetComponent<AudioSource>();
        WithWallAu = GameObject.Find("PlayerWithWall").GetComponent<AudioSource>();
        EatingPelletAu = GameObject.Find("AuPellet").GetComponent<AudioSource>();
        startpos = transform.position;
        startqu = transform.rotation;
        Init();
    }
    // Update is called once per frame
    void Update()
    {
        if (GamePanel.GetStart() == false)
            return;
        PlayerInput();
        iSTowards();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pellet")
        {
            GamePanel.score+=10;
            EatingPelletAu.Play();
            GamePanel.Fire("UpdateScore");
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Cherry")
        {
            GamePanel.score+=100;
            EatingPelletAu.Play();
            GamePanel.Fire("UpdateScore");
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag== "PowerPellet")
        {
            GamePanel.Fire("GhostChange");
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag=="Ghost")
        {
            GamePanel.Fire("Over");
            Destroy(gameObject);

        }
    }
    private void PlayerInput()
    {
        if (Input.GetKey(KeyCode.W))
            CurrentInput = KeyCode.W;
        if (Input.GetKey(KeyCode.S))
            CurrentInput = KeyCode.S;
        if (Input.GetKey(KeyCode.A))
            CurrentInput = KeyCode.A;
        if (Input.GetKey(KeyCode.D))
            CurrentInput = KeyCode.D;

    }
    private void OnDrawGizmos()
    {
     
        Debug.DrawRay(transform.position, transform.right, Color.red);
      
    }
    private void iSTowards()
    {
        aim.SetBool("Up",false);
        aim.SetBool("Down", false);
        aim.SetBool("Right", false);
        aim.SetBool("Left", false);
        switch (CurrentInput)
        {
            case KeyCode.W:
                if (IsWalk(CurrentInput))
                {

                    Dirt3.Play();
                    aim.SetBool("Up", true);
                    Move();
                }
                else
                {
                    Collision13.Play();
                    MoveAu.Stop();
                    if(iSCollionWall==true)
                    {
                        iSCollionWall = false;
                        WithWallAu.Play();
                    }
                    
                    tweener.StopTween(transform);
                }
                
                break;
            case KeyCode.S:
                if (IsWalk(CurrentInput))
                {
                    Dirt4.Play();

                    aim.SetBool("Down",true);
                    Move();
                }
                else
                {
                    Collisionl4.Play();
                    MoveAu.Stop();
                    if (iSCollionWall == true)
                    {
                        iSCollionWall = false;
                        WithWallAu.Play();
                    }
                    tweener.StopTween(transform);
                }
                    
                break;
            case KeyCode.A:
                if (IsWalk(CurrentInput))
                {
                    aim.SetBool("Left", true);
                    Dirt1.Play();
                  
                    Move();
                }
                else
                {
                    Collision1.Play();
                    MoveAu.Stop();
                    if (iSCollionWall == true)
                    {
                        iSCollionWall = false;
                        WithWallAu.Play();
                    }
                    tweener.StopTween(transform);
                }
                break;
            case KeyCode.D:
                if (IsWalk(CurrentInput))
                {
                    Dirt2.Play();
                    aim.SetBool("Right", true);
                    Move();
                   
                }
                else
                {
                    Collisionl2.Play();
                    MoveAu.Stop();
                    if (iSCollionWall == true)
                    {
                        iSCollionWall = false;
                        WithWallAu.Play();
                    }
                    tweener.StopTween(transform);
                }
                    
                break;
        }
    }
    private void Move()
    {
        if(!MoveAu.isPlaying)
           MoveAu.Play();
        iSCollionWall = true;
            tweener.AddTween(transform, transform.position,CurrentPoint, time);
    }
    Vector3 direction;
    Vector3 CurrentPoint;
    bool IsArrawy = false;
    bool IsWalk(KeyCode currentInput)
    {
        switch(currentInput)
        {
            case KeyCode.W:
                direction = transform.up;
                break;
            case KeyCode.S:
                direction = -transform.up;
                break;
            case KeyCode.A:
                direction = -transform.right;
                break;
            case KeyCode.D:
                direction = transform.right;
                break;
        }
        LastInput = CurrentInput;
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, direction, 1.1F,layer);
        if (hit2D)
        {
         if(hit2D.collider.gameObject)
            {

                return false;
            }
            
        }
       
        if (Vector3.Distance(transform.position,(transform.position + direction)) >0&&IsArrawy==false)
        {

            CurrentPoint = transform.position + direction;
            IsArrawy = true;
        }
        if(Vector3.Distance(transform.position,CurrentPoint)==0)
        {
            IsArrawy = false;
        }
        if (Vector3.Distance(transform.position, (transform.position + direction)) > 0 && IsArrawy == false)
        {

            CurrentPoint = transform.position + direction;
            IsArrawy = true;
        }
      
        return true;
    }
    void Init()
    {
        aim = GetComponent<Animator>();
        tweener = GetComponent<Tweener>();
    }

}
