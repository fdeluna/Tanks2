using UnityEngine;
using UnityEngine.AI;

namespace Utils
{
    public static class Utils
    {
        public static Vector3 RandomPoint(this NavMeshAgent navMeshAgent, float min, float max)
        {
            Vector3 randomPoint = navMeshAgent.transform.position + Random.insideUnitSphere * Random.Range(min,max);
            NavMeshHit hit;
            
            while (!NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                randomPoint = navMeshAgent.transform.position + Random.insideUnitSphere * Random.Range(min, max);
            }

            return hit.position;
        }
    }
}
