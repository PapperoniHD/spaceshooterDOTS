using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int points;
    public int wave = 0;
    public int enemyCount = 0;
    public float spawnRadius = 20f;
    public int spawnAmount = 5;
    public GameObject player;
    public GameObject enemyPrefab;

    public static GameManager GM;

    public TextMeshProUGUI pointsUI;
    public TextMeshProUGUI waveUI;

    // Start is called before the first frame update
    void Start()
    {
        GM = this;
    }

    // Update is called once per frame
    void Update()
    {
        pointsUI.SetText("Points: " + points);
        waveUI.SetText("Wave: " + wave);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SpawnEnemies();
        }

        NextWave();
    }

    void NextWave()
    {
        if (enemyCount <= 0) 
        {
            spawnAmount = wave * 2 + spawnAmount;
            wave++;
            SpawnEnemies();
        }
        if (enemyCount < 0)
        {
            enemyCount = 0;
        }
        
    }
    void SpawnEnemies()
    {
       
        for (int i = 0; i < spawnAmount; i++)
        {
            var spawnPoint = RandomPointOnCircleEdge(spawnRadius);
            Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
        }    
    }
    public Vector3 RandomPointOnCircleEdge(float radius)
    {
        var vector2 = UnityEngine.Random.insideUnitCircle.normalized * radius;
        return vector2;
    }

}
