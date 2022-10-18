using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    private Camera cam;
    private Vector3 lastPosition;
    private  CircleCollider2D _collider2D;
    public static TrailRenderer trailRenderer;

    [SerializeField] private GameObject slidingIcon;

    private void Awake()
    {
        instance = this;
        cam = Camera.main;
        _collider2D = GetComponent<CircleCollider2D>();
        trailRenderer =GetComponent<TrailRenderer>();
        TurnOffCollider();
    }

    private void Start()
    {
        var color = Saver.instance.colorPalette.mainColor;
        trailRenderer.startColor = color;
        trailRenderer.endColor = color;
    }

    private float maxTimeForTap = 0.3f;
    private float timer;
    private bool holding;
    void Update()
    {

        if (  AudioVisualizer.isPaused)
        {
            return;
        }

      
        
        

#if UNITY_EDITOR
        var pos = cam.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;
        if (holding)
        {
            timer += Time.deltaTime;
            if (timer > maxTimeForTap)
            {
                Combo.instance.StartSliding();
                slidingIcon.SetActive(true);
            }
        }
        else
        {
            slidingIcon.SetActive(false);
        }
        
       
        
        if (Input.GetMouseButtonDown(0))
        {
            holding = true;
            timer = 0;
            TurnOnCollider();
           
        }
        else if (Input.GetMouseButtonUp(0))
        {
            holding = false;
            TurnOffCollider();
        }
#endif
        if (Input.touchCount > 0)
        {
            var pos2 = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
            pos2.z = 0;
            transform.position = pos2;
            if (holding)
            {
                timer += Time.deltaTime;
                if (timer > maxTimeForTap)
                {
                    Combo.instance.StartSliding();
                    slidingIcon.SetActive(true);
                }
            }
            else
            {
                slidingIcon.SetActive(false);
            }
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                holding = true;
                timer = 0;
                TurnOnCollider();
                //  transform.position = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                holding = false;
                TurnOffCollider();
            }
        }

    }
    private void TurnOnCollider()
    {
        _collider2D.enabled = true;
        trailRenderer.enabled = true;
        //trailRenderer.Clear();
    }
    private void TurnOffCollider()
    {
        _collider2D.enabled = false;
        trailRenderer.enabled = false;
        trailRenderer.Clear();
    }


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