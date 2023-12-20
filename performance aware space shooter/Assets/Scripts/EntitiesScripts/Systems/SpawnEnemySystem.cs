using System.Diagnostics;
using Unity.Burst;
using Unity.Entities;
using Unity.VisualScripting.FullSerializer;
using Unity;
using Unity.Transforms;

[BurstCompile]
[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct SpawnEnemySystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<EnemySpawnProperties>();
    }
    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {

    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;
        var enemyEntity = SystemAPI.GetSingletonEntity<EnemySpawnProperties>();
        var enemyAspect = SystemAPI.GetAspect<SpawnAspect>(enemyEntity);

        var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

        for (var i = 0; i < enemyAspect.NumberEnemiesToSpawn; i++)
        {
            var newEnemy = ecb.Instantiate(enemyAspect.EnemyPrefab);
            var newEnemyTransform = enemyAspect.GetRandomSpawnTransform();
            ecb.SetComponent(newEnemy, newEnemyTransform);
        }
        
        ecb.Playback(state.EntityManager);
    }
}
