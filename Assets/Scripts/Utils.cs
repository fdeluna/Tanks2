using UnityEngine;
using UnityEngine.AI;


public static class Utils
{
    public static Vector3 RandomPointAroundTarget(this NavMeshAgent navMeshAgent, float min, float max, Transform target = null)
    {
        Vector3 targetPosition = target == null ? navMeshAgent.transform.position : target.position;

        Vector3 randomPoint = targetPosition + Random.insideUnitSphere * Random.Range(min, max);
        randomPoint.y = 0;
        NavMeshHit hit;

        while (!NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            randomPoint = navMeshAgent.transform.position + Random.insideUnitSphere * Random.Range(min, max);
        }

        return hit.position;
    }

    public static Vector3 ClampVector3ToViewPort(this Vector3 vector3, Camera camera, float minMargin = 0, float maxMargin = 1)
    {
        Vector3 viewPosition = camera.WorldToViewportPoint(vector3);
        viewPosition.x = Mathf.Clamp(viewPosition.x, minMargin, maxMargin);
        viewPosition.y = Mathf.Clamp(viewPosition.y, minMargin, maxMargin);
        vector3 = camera.ViewportToWorldPoint(viewPosition);

        return vector3;
    }

    public static bool IsPointInViewPort(Camera camera, Vector3 position)
    {
        Vector3 viewPosition = camera.WorldToViewportPoint(position);        
        return viewPosition.x >= 0 && viewPosition.x <= 1 && viewPosition.y >= 0 && viewPosition.y <= 1;
    }
}