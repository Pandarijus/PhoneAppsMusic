using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Spawner : Disposer
{
    public static Spawner instance;
    [SerializeField] Transform[] blockSpawnTransforms;
    private float cooldown = 0.25f;
    private float speed = 5f;
    private float cooldownTimer;
    private int explosionLifetime,animationLifetime;
    public const int COLLIMN_COUNT = 6;
    private float startRatio;
    private float minSpeed = 4f;
    private float maxSpeed = 10f;

   // [SerializeField] private Slider slider;
   //[SerializeField] private SliderGetter cooldownSliderGetter,speedSliderGetter; //SLIDER
    void Awake()
    {
        startRatio = cooldown / speed;
     //   slider.onValueChanged.AddListener(SetSpeed);
        instance = this;
        explosionLifetime = 3000;//(int) explosionPrefab.GetComponent<ParticleSystem>().main.startLifetimeMultiplier*1000;
        animationLifetime = (int) scoreTextPrefab.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length*1000;
        MusicBlock.ChangeAllSpeeds(speed);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
      //  cooldown = startRatio * speed;
        MusicBlock.ChangeAllSpeeds(speed);
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
        cooldownTimer = cooldown; //cooldownSliderGetter.value; //SLIDER
    }
    public void SpawnMusicBlock(int collumn)
    {
        var musicBlock = GetMusicBlock();
        musicBlock.transform.position = blockSpawnTransforms[collumn].position;
        //musicBlock.GetComponent<MusicBlock>().SetMoveSpeed(speedSliderGetter.value); //SLIDER
       // musicBlock.GetComponent<MusicBlock>().SetMoveSpeed(speed);
        StartCooldown();
    }

    public int GetRandomCollumn()
    {
       return Random.Range(0, COLLIMN_COUNT);
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
    public async Task SpawnExplosion(Vector3 blockPosition,Color blockColor)
     {
         //Debug.Log("Spawning explosion"+explosionLifetime);
         //  var explosion = GetMusicBlock(explosionPrefab);
       var playerPos = Player.instance.transform.position;
       var dir = blockPosition - playerPos;
       var explosion = GetExplosion();
       explosion.transform.position = blockPosition;
       explosion.transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg-90);
           var main =  explosion.GetComponentInChildren<ParticleSystem>().main;
           main.startColor =blockColor;// new ParticleSystem.MinMaxGradient(blockColor); //
       await Task.Delay(explosionLifetime);
       DisposeExplosion(explosion);
     }
    public async Task SpawnAnimatedTextFieled(int addScore,Vector3 blockPosition,Color blockColor)
    {
        GameObject g = GetScoreText();
     //   g.transform.parent = canvasTransform;
        g.transform.position = blockPosition;
        g.GetComponent<MyDisplay>().Display(addScore);
        g.GetComponent<MyDisplay>().SetColor(blockColor);
        await Task.Delay(animationLifetime);
        DisposeScoreText(g);
    }
}
