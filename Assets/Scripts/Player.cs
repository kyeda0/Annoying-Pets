using System;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour,PlayerInterface
{
    private Rigidbody2D rigidbody2D;
    private  float speed;
    private Sprite[] allSkin;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;
    public int coin;
    public float maxHealth;
    [HideInInspector] public  float health; 
    private float maxLane;
    private float minLane;
    private float currentLane = 0;
    private float distantLane = 2;
    public event Action OnGameOverEvent;
    public event Action <float,float> OnTakeDamage;
    public event Action <Player.LevelSpeed> OnCheckSpeed;
    public event Action <int> OnCheckCoins;

    public bool isMoving;
    public LevelSpeed currentlevelSpeed;
    private int spriteIndex;
    
    private void Start()
    {
        coin = PlayerPrefs.GetInt("Coins");
        spriteIndex = PlayerPrefs.GetInt("SkinsIndex");
        rigidbody2D = GetComponent<Rigidbody2D>();
        health = maxHealth;
        minLane = -1;
        maxLane = 1;
        ChangeSpeed(LevelSpeed.Normal);
        allSkin = Resources.LoadAll<Sprite>("Skins");
        GetComponent<SpriteRenderer>().sprite = allSkin[spriteIndex];
        ChangeCoins();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 tochPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(tochPos.x < transform.position.x && tochPos.y < 2f && isMoving == true)
            {
                ChangeLane(-1);
            }
            else if(tochPos.x > transform.position.x && tochPos.y < 2f && isMoving == true)
            {
                ChangeLane(1);
            }

        }
    }

    private void FixedUpdate()
    {
        Movement();
    }


    private void ChangeLane(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction,minLane, maxLane );
    }

    public  void Movement()
    {
        var targetPos =  new Vector2(currentLane * distantLane,rigidbody2D.position.y);
        var newPos = Vector2.Lerp(rigidbody2D.position,targetPos,speed * Time.fixedDeltaTime);
        rigidbody2D.MovePosition(newPos);
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        OnTakeDamage?.Invoke(health,maxHealth);
        if(health <= 0)
        {
            Kill();
        }
    }

    public  void Kill()
    {
        OnGameOverEvent?.Invoke();
    }


    private void SetSpeedLevel()
    {
        switch (currentlevelSpeed)
        {
            case LevelSpeed.Slow:
                speed = minSpeed;
                break;
            case LevelSpeed.Fast:
                speed = maxSpeed;
                break;
            case LevelSpeed.Normal:
                speed = 4f;
                break;
            case LevelSpeed.ZeroSpeed:
                speed = 0f;
                break;
        }
    }

    public void ChangeSpeed(LevelSpeed levelSpeed)
    {
        currentlevelSpeed = levelSpeed;
        SetSpeedLevel();
        OnCheckSpeed?.Invoke(currentlevelSpeed);
    }

    public void ChangeCoins()
    {
        OnCheckCoins?.Invoke(coin);
    }
    public enum LevelSpeed
    {
        Slow,
        Fast,
        Normal,
        ZeroSpeed
    }
}
