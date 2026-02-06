using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;


public class GameObjectSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObjects> gameObjects = new List<GameObjects>();
    [SerializeField] private float[] spawnPosX = new float[3]{-2f,0f,2f};
    [SerializeField] private float repeatTime;
    [SerializeField] private float timeToSpawn;
    public Player targetPlayer;
    private GameObjects lastGameObject;
    private float lastPosX;

    public event Action<GameObjects> OnSpawnedBlock;

    private void Start()
    {
        targetPlayer.OnCheckSpeed += CheckSpeed;
    }
    public void StartSpawnGameObject()
    {
        InvokeRepeating(nameof(SpawnGameObject),timeToSpawn,repeatTime);
    }

    public void StopSpawnGameObject()
    {
        CancelInvoke(nameof(SpawnGameObject));
    }
    private void SpawnGameObject()
    {
        var randomGameObject = GetGameObject();
        var block = Instantiate(randomGameObject);
        var randomPosX = GetPosX();
        block.transform.position = new Vector2(randomPosX,6f);
        block.player = targetPlayer;
        lastGameObject = randomGameObject;
        lastPosX = randomPosX;
        OnSpawnedBlock?.Invoke(block);
    }


    private float GetPosX()
    {
        float randomPosX;

        do
        {
            randomPosX = spawnPosX[UnityEngine.Random.Range(0,spawnPosX.Length)];
        }
        while(lastPosX == randomPosX);

        return randomPosX;
    }

    private GameObjects GetGameObject()
    {
        GameObjects randomGameObject;
        do
        {
            randomGameObject = gameObjects[UnityEngine.Random.Range(0,gameObjects.Count)];
        }
        while(lastGameObject == randomGameObject);

        return randomGameObject;
    }

    private void SwicthSpeedSpawn(float repeatTimeUp)
    {
        repeatTime = repeatTimeUp;
        StopSpawnGameObject();
        StartSpawnGameObject();
    }

    private void CheckSpeed(Player.LevelSpeed levelSpeed)
    {
        switch (levelSpeed)
        {
            case Player.LevelSpeed.Fast:
                SwicthSpeedSpawn(0.7f);
                break;
            case Player.LevelSpeed.Normal:
                SwicthSpeedSpawn(1.3f);
                break;
            case Player.LevelSpeed.Slow:
                SwicthSpeedSpawn(1.7f);
                break;
        }
    }

}
