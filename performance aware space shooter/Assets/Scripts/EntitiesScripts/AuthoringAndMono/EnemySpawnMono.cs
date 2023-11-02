using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawnMono : MonoBehaviour
{
    public float2 FieldDimensions;
    public int NumberOfSpawns;
    public GameObject EnemyPrefab;
    public uint RandomSeed;
}

public class EnemyBaker : Baker<EnemySpawnMono>
{
    public override void Bake(EnemySpawnMono authoring)
    {
        TransformUsageFlags flags = new TransformUsageFlags();
        Entity entity = GetEntity(flags);

        AddComponent(entity, new EnemySpawnProperties
        {
            FieldDimension = authoring.FieldDimensions,
            NumberOfSpawns = authoring.NumberOfSpawns,
            EnemyPrefab = GetEntity(authoring.EnemyPrefab, TransformUsageFlags.Dynamic),
        });
        AddComponent(entity, new SpawnRandom
        {
            Value = Unity.Mathematics.Random.CreateFromIndex(authoring.RandomSeed)
        });
    }
}
