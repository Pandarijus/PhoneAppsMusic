using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Spawner : Disposer
{
    public static Spawner instance;
    [SerializeField] Transform[] blockSpawnTransforms;
    private float cooldown = 0.4f;
    private float cooldownTimer;
    private int explosionLifetime,animationLifetime;
    [NonSerialized] public int collumnCount = 6;
   [SerializeField] private SliderGetter cooldownSliderGetter,speedSliderGetter;
    void Awake()
    {
        cooldownSliderGetter.value = cooldown;

     //   collumnCount = blockSpawnTransforms.Length;
        base.Awake();
        instance = this;
        explosionLifetime = 3000;//(int) explosionPrefab.GetComponent<ParticleSystem>().main.startLifetimeMultiplier*1000;
        animationLifetime = (int) scoreTextPrefab.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length*1000;
    }

    private void Update()
    {
        if(cooldownTimer > 0)
        cooldownTimer -= Time.deltaTime;
    }

    public bool CanSpawnNextBlock()
    {
        return cooldownTimer <= 0;
    }

    private void StartCooldown()
    {
        cooldownTimer = cooldownSliderGetter.value;
    }
    public void SpawnMusicBlock(int collumn)
    {
        var musicBlock = GetMusicBlock();
        musicBlock.transform.position = blockSpawnTransforms[collumn].position;
        musicBlock.GetComponent<MusicBlock>().SetMoveSpeed(speedSliderGetter.value);
        StartCooldown();
    }

    public int GetRandomCollumn()
    {
       return Random.Range(0, collumnCount);
    }
    // public async Task SpawnExplosion(Vector3 blockPosition)
    // {
    //     var playerPos = Player.instance.transform.position;
    //     var dir = Player.instance.GetMovingDirection();
    //     var explosion = GetExplosion();
    //   explosion.transform.position = playerPos;
    //   explosion.transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg-90);
    //   await Task.Delay(explosionLifetime);
    //   DisposeExplosion(explosion);
    //  }
    public async Task SpawnExplosion(Vector3 blockPosition)
     {
         //Debug.Log("Spawning explosion"+explosionLifetime);
         //  var explosion = GetMusicBlock(explosionPrefab);
       var playerPos = Player.instance.transform.position;
       var dir = blockPosition - playerPos;
       var explosion = GetExplosion();
       explosion.transform.position = blockPosition;
       explosion.transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg-90);
       await Task.Delay(explosionLifetime);
       DisposeExplosion(explosion);
     }
    public async Task SpawnAnimatedTextFieled(int addScore,Vector3 blockPosition)
    {
        GameObject g = GetScoreText();
     //   g.transform.parent = canvasTransform;
        g.transform.position = blockPosition;
        g.GetComponent<MyDisplay>().Display(addScore);
        await Task.Delay(animationLifetime);
        DisposeScoreText(g);
    }
}
