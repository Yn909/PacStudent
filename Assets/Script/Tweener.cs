using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{

    //private Tween activeTween;
    private List<Tween> activeTweens = new List<Tween>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FrameCalculation();

    }
    public void FrameCalculation()
    {
        for (int i = 0; i < activeTweens.Count; i++)
        {
            Tween activeTween = activeTweens[i];
            if ((activeTween.Target.position - activeTween.EndPos).sqrMagnitude > 0.1f)
            {
                float ResultTime = (Time.time - activeTween.StartTime) / activeTween.Duration;
                activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, ResultTime);
            }
            else
            {
                activeTween.Target.position = activeTween.EndPos;
                activeTweens.Remove(activeTween);
            }
        }
    }
    public bool TweenExists(Transform target)
    {
        for(int i=0;i<activeTweens.Count;i++)
            if(activeTweens[i].Target==target)
                return true;
        return false;
    }
    public bool AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration)
    {
        if (TweenExists(targetObject)==false)
        {
            activeTweens.Add(new Tween(targetObject, startPos, endPos, Time.time, duration));
            return true;
        }
        return false;        
    }

    public void StopTween(Transform target)
    {
        Tween targetTween = null;
        foreach (Tween t in activeTweens)
        {
            if (t.Target == target)
            {
                targetTween = t;
            }
        }
        if(targetTween != null) activeTweens.Remove(targetTween);
    }
}
