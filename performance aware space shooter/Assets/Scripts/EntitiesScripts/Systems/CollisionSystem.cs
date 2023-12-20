using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;

namespace Systems
{
    [BurstCompile]
    public partial struct CollisionSystem : ISystem
    {

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndVariableRateSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<EndFixedStepSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<SimulationSingleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {


            // foreach (var asteroidAspect in SystemAPI.Query<AsteroidAspect>().WithAll<AsteroidProperties.Tag>()) {
            // 	foreach (var asteroidAspect2 in SystemAPI.Query<AsteroidAspect>().WithAll<AsteroidProperties.Tag>()) {
            // 		var buffer = SystemAPI.GetSingleton<EndVariableRateSimulationEntityCommandBufferSystem.Singleton>();
            // 		if (asteroidAspect.Entity != asteroidAspect2.Entity) {
            // 			if (math.distance(asteroidAspect.GetTransform.Position.x, asteroidAspect2.GetTransform.Position.x) < 1.0f && 
            // 			    math.distance(asteroidAspect.GetTransform.Position.y, asteroidAspect2.GetTransform.Position.y) < 1.0f) {
            // 				asteroidAspect.DestroyAsteroid(buffer.CreateCommandBuffer(state.WorldUnmanaged), asteroidAspect.Entity);
            // 				asteroidAspect2.DestroyAsteroid(buffer.CreateCommandBuffer(state.WorldUnmanaged), asteroidAspect2.Entity);
            // 			}
            // 		}
            // 	}
            // }

            var ecb = SystemAPI.GetSingleton<EndFixedStepSimulationEntityCommandBufferSystem.Singleton>();
            state.Dependency = new CollisionEvent
            {
                CollisionLookup = SystemAPI.GetComponentLookup<CollisionComponent>(),
                DestroyLookup = SystemAPI.GetComponentLookup<DestroyComponent>(),
                ECB = ecb.CreateCommandBuffer(state.WorldUnmanaged)
            }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
        }

    }

    [BurstCompile]
    public struct CollisionEvent : ICollisionEventsJob
    {

        [ReadOnly] public ComponentLookup<CollisionComponent> CollisionLookup;
        [ReadOnly] public ComponentLookup<DestroyComponent> DestroyLookup;
        public EntityCommandBuffer ECB;

        [BurstCompile]
        public void Execute(Unity.Physics.CollisionEvent collisionEvent)
        {
            // revert methods inside
            if (!CollisionLookup.HasComponent(collisionEvent.EntityA) || !CollisionLookup.HasComponent(collisionEvent.EntityB)) return;

            if (DestroyLookup.HasComponent(collisionEvent.EntityA))
            {
                ECB.DestroyEntity(collisionEvent.EntityA);
            }

            if (DestroyLookup.HasComponent(collisionEvent.EntityB))
            {
                ECB.DestroyEntity(collisionEvent.EntityB);
            }
        }
    }
}
