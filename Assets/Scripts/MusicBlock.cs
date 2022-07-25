using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MusicBlock : MonoBehaviour
{
    private Rigidbody2D rb;
 //   [SerializeField] private float speed = 3;
    private static float moveSpeed = 3f;

    [SerializeField] private Gradient colorGradient;
    private float spawnedTime;
    private SpriteRenderer renderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        rb.velocity = Vector2.down * moveSpeed;
        spawnedTime = Time.timeSinceLevelLoad;
    }

    private void FixedUpdate()
    {
        float lifeTime = Time.timeSinceLevelLoad - spawnedTime;
        renderer.color = colorGradient.Evaluate(GetTimingValue(lifeTime));
    }

    public static float GetTimingValue(float lifeTime)
    {
        float diff = Mathf.Abs( AudioVisualizer.staticStartDelay - lifeTime);       // float value = (diff < 1)? (1 - diff) : 0;
        return (diff < 1) ? (1 - diff) : 0;
    }


    private void OnMouseDown()
    {
        float lifeTime = Time.timeSinceLevelLoad - spawnedTime;
        ScoreManager.instance.MusicBlockWasClicked(lifeTime, transform.position);
        Spawner.instance.DisposeMusicBlock(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Combo.instance.StopCombo();
        gameObject.SetActive(false);
        Spawner.instance.DisposeMusicBlock(gameObject);
    }

    public void SetMoveSpeed(float value)
    {
        moveSpeed = value;
    }
}