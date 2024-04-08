using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchHandler : MonoBehaviour
{
    private Touch actingTouch;
    private int storedFingerId = -1;
    protected Vector2 touchStartPosition;
    protected Vector2 touchEndPosition;

    // Minimum distance for a drag gesture
    public float minDragDistance = 20f;

    // Variable to store the duration of drag
    protected float dragDuration;

    protected bool UIhit;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        TouchUpdateLoop();
    }

    public void TouchUpdateLoop()
    {
        if (Input.touchCount > 0)
        {
            if (storedFingerId == -1)
            {
                foreach (Touch t in Input.touches)
                {
                    if (t.phase == TouchPhase.Began)
                    {
                        //Debug.Log("found touch " + t.fingerId);
                        storedFingerId = t.fingerId;
                        actingTouch = t;
                        break;
                    }
                }
            }
            else
            {
                foreach (Touch t in Input.touches)
                {
                    if (t.fingerId == storedFingerId)
                    {
                        actingTouch = t;
                        break;
                    }
                }
            }

            if (storedFingerId != -1)
            {
                if (actingTouch.phase == TouchPhase.Moved || actingTouch.phase == TouchPhase.Stationary)
                {
                    dragDuration += Time.deltaTime;
                }
                else if (actingTouch.phase == TouchPhase.Began)
                {
                    //Debug.Log(actingTouch.fingerId + " began");
                    touchStartPosition = actingTouch.position;
                    UIhit = EventSystem.current.IsPointerOverGameObject(actingTouch.fingerId);
                }
                else if (actingTouch.phase == TouchPhase.Ended)
                {
                    //Debug.Log(actingTouch.fingerId + " ended");
                    touchEndPosition = actingTouch.position;
                    if (!UIhit)
                    {
                        if (Vector2.Distance(touchStartPosition, touchEndPosition) > minDragDistance)
                        {
                            HandleDrag();
                        }
                        else
                        {
                            HandleTap();
                        }
                    }

                    dragDuration = 0;
                    storedFingerId = -1;
                }
                else
                {
                    Debug.Log(actingTouch.fingerId + " error");
                    dragDuration = 0;
                    storedFingerId = -1;
                }
            }
        }
    }
    

    public virtual void HandleTap()
    {
        
    }

    public virtual void HandleDrag()
    {

    }

    
}
