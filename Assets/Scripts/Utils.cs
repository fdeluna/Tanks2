using UnityEngine;
using UnityEngine.AI;

public static class Utils
{
    /// <summary>
    /// Extension for get a random point within a range around a NavMeshAgent or a specific position
    /// </summary>
    /// <param name="navMeshAgent"> NavMeshAgent </param>
    /// <param name="min"> Min range distance  </param>
    /// <param name="max"> Max range distance </param>
    /// <param name="targetPosition"> Center position to find the random point around it </param>
    /// <returns> Random position </returns>
    public static Vector3 RandomPointAroundTarget(this NavMeshAgent navMeshAgent, float min, float max, Vector3 targetPosition)
    {        
        Vector3 randomPoint = targetPosition + Random.insideUnitSphere * Random.Range(min, max);
        randomPoint.y = 0;
        NavMeshHit hit;

        while (!NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            randomPoint = navMeshAgent.transform.position + Random.insideUnitSphere * Random.Range(min, max);
        }

        return hit.position;
    }

    /// <summary>
    /// Clamps vector3 to a camera within a margins
    /// </summary>
    /// <param name="vector3"> Vector3 </param>
    /// <param name="camera"> Camera to clamp the vector 3 </param>
    /// <param name="minMargin"> Min viewport margin </param>
    /// <param name="maxMargin"> Max viewport margin </param>
    /// <returns> clamped vector3 </returns>
    public static Vector3 ClampVector3ToViewPort(this Vector3 vector3, Camera camera, float minMargin = 0, float maxMargin = 1)
    {
        Vector3 viewPosition = camera.WorldToViewportPoint(vector3);
        viewPosition.x = Mathf.Clamp(viewPosition.x, minMargin, maxMargin);
        viewPosition.y = Mathf.Clamp(viewPosition.y, minMargin, maxMargin);
        vector3 = camera.ViewportToWorldPoint(viewPosition);

        return vector3;
    }

    /// <summary>
    /// Checks if the given point is inside the viewport
    /// </summary>
    /// <param name="camera"> camera to check the position</param>
    /// <param name="position"> position to check </param>
    /// <returns> true is inside the viewport limits </returns>
    public static bool IsPointInViewPort(Camera camera, Vector3 position)
    {
        Vector3 viewPosition = camera.WorldToViewportPoint(position);        
        return viewPosition.x >= 0 && viewPosition.x <= 1 && viewPosition.y >= 0 && viewPosition.y <= 1;
    }
}