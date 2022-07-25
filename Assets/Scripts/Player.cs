using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    private Camera cam;
    private Vector3 lastPosition;
  //  private CircleCollider2D _collider2D;

    private void Awake()
    {
        instance = this;
        cam = Camera.main;
      //  _collider2D = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        
        

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
           // _collider2D.enabled = true;
            transform.position = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        // else if (Input.GetMouseButtonUp(0))
        // {
        //     TurnOffCollider();
        // }
#endif
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                //_collider2D.enabled = true;
                transform.position = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
            }
            // else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            // {
            //     TurnOffCollider();
            // }
        }

    }

    // private void TurnOffCollider()
    // {
    //     _collider2D.enabled = false;
    // }

    //    private void OnTriggerEnter2D(Collider2D col)
    // {
    //     TurnOffCollider();
    // }


    //
    // private Vector2 GetCurrentPos()
    // {
    //     return GetTouchPos(Input.GetTouch(0));
    // }
    //
    // private Vector2 GetTouchPos(Touch touch)
    // {
    //     return cam.ScreenToWorldPoint(touch.position);
    // }

    // public Vector3 GetMovingDirection()
    // {
    //     return ball.transform.position - lastPosition;
    // }
}