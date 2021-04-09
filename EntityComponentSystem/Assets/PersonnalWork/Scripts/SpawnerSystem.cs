using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.Rendering;

public class SpawnerSystem : JobComponentSystem
{
    private EndSimulationEntityCommandBufferSystem m_EntityCommandBufferSystem;
    
    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }
    
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new SpawnJob
        {
            CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer()
        }.ScheduleSingle(this, inputDeps);
        m_EntityCommandBufferSystem.AddJobHandleForProducer(job);
        return job;
    }

    struct SpawnJob: IJobForEachWithEntity<Spawner, LocalToWorld>
    {
        public EntityCommandBuffer CommandBuffer;

        public void Execute(Entity entity, int index, [ReadOnly] ref Spawner spawner,
            [ReadOnly] ref LocalToWorld location)
        {
            for (int x = 0; x < spawner.Erows; x++)
            {
                for (int z = 0; x < spawner.Ecols; z++)
                {
                    var instance = CommandBuffer.Instantiate(spawner.Prefab);
                    var pos = math.transform(location.Value, new float3(x, noise.cnoise(new float2(x, z) * 0.21f), z));
                }
            }
            CommandBuffer.DestroyEntity(entity);
        }
    }
}
