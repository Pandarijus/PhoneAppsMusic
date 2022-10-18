using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MusicBlock : MonoBehaviour
{
    private Rigidbody2D rb;

    //   [SerializeField] private float speed = 3;
    private static float moveSpeed = 3f;

    private float spawnedTime;
    private SpriteRenderer renderer;

    public delegate void Change(float change);

    public static Change SpeedChange;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    public static void ChangeAllSpeeds(float speedChange)
    {
        SpeedChange?.Invoke(speedChange);
    }

    private void OnEnable()
    {
        rb.velocity = Vector2.down * moveSpeed;
        spawnedTime = Time.timeSinceLevelLoad;
        SpeedChange += SetMoveSpeed;
    }

    private void OnDisable()
    {
        SpeedChange -= SetMoveSpeed;
    }

    public void SetMoveSpeed(float value)
    {
        moveSpeed = value;
        rb.velocity = Vector2.down * moveSpeed;
    }

    private void FixedUpdate()
    {
        float lifeTime = Time.timeSinceLevelLoad - spawnedTime;
        renderer.color = GradientManager.Evaluate(GetTimingValue(lifeTime));
    }

   

    public static float GetTimingValue(float lifeTime)
    {
     float leeway = 0.8f;
     //+ 0.5f 
        float diff =
            Mathf.Abs(AudioVisualizer.MUSIC_START_DELAY - lifeTime); // float value = (diff < 1)? (1 - diff) : 0;
        return (diff < leeway) ? (1 - diff) : 0;
    }


    // private void OnMouseDown()
    // {
    //     Crunch();
    // }

    private void Crunch()
    {
        float lifeTime = Time.timeSinceLevelLoad - spawnedTime;
        ScoreManager.instance.MusicBlockWasClicked(lifeTime, transform.position, renderer.color);
        Spawner.instance.DisposeMusicBlock(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            Crunch();
        }
        else
        {
            //HealthManager.instance.TakeDamage();
            Combo.instance.StopCombo();
            gameObject.SetActive(false);
            Spawner.instance.DisposeMusicBlock(gameObject);
        }
    }
}