using Unity.Entities;
using Unity.Transforms;
public readonly partial struct SpawnAspect : IAspect
{
    public readonly Entity Entity;

    private readonly RefRO<EnemySpawnProperties> _enemyspawnProperties;
    private readonly RefRW<SpawnRandom> _spawnRandom;

    public int NumberEnemiesToSpawn => _enemyspawnProperties.ValueRO.NumberOfSpawns;
    public Entity EnemyPrefab => _enemyspawnProperties.ValueRO.EnemyPrefab;
}

