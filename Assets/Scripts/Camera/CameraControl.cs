using UnityEditor;
using UnityEngine;

namespace Complete.Controllers
{
    public class CameraControl : MonoBehaviour
    {

        public float m_DampTime = 0.2f;                 // Approximate time for the camera to refocus.
        public float m_ScreenEdgeBuffer = 4f;           // Space between the top/bottom most target and the screen edge.
        // CE-101
        [Header("Camera Zoom")]
        public float m_MaxSize = 17f;                   // The biggest orthographic size the camera can be.
        public float m_MinSize = 6.5f;                  // The smallest orthographic size the camera can be.
        [HideInInspector] public Transform[] m_Targets; // All the targets the camera needs to encompass.
        [Header("Boundaires")]
        public float m_TopBoundaire = 0.5f;             // Top Camera viewport boundary
        public float m_BotBoundaire = 0.5f;             // Bottom Camera viewport boundary
        public float m_RightBoundaire = 0.5f;           // Right Camera viewport boundary
        public float m_LeftBoundaire = 0.5f;            // Left Camera viewport boundary

        private Vector3 _topBoundry;                    // Top boundry World position for Camera boundaries
        private Vector3 _bottomBoundry;                 // Bottom boundry World position for Camera boundaries
        private Vector3 _leftBoundry;                   // Left boundry X World position for Camera boundaries
        private Vector3 _rightBoundry;                  // Right boundry  World position for Camera boundaries
        private Camera m_Camera;                        // Used for referencing the camera.
        private float m_ZoomSpeed;                      // Reference speed for the smooth damping of the orthographic size.
        private Vector3 m_MoveVelocity;                 // Reference velocity for the smooth damping of the position.
        private Vector3 m_DesiredPosition;              // The position the camera is moving towards.              

        private void Awake()
        {
            m_Camera = GetComponentInChildren<Camera>();
            SetWorldBoundaires();
        }

        private void FixedUpdate()
        {
            // Move the camera towards a desired position.
            Move();

            // Change the size of the camera based.
            Zoom();
        }

        private void Move()
        {
            // Find the average position of the targets.
            FindAveragePosition();

            // Smoothly transition to that position
            Vector3 movePosition = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);

            transform.position = movePosition;

            // CE-101
            // Apply Boundaires
            Vector3 applyBoundaires = transform.position;
            applyBoundaires.x = Mathf.Clamp(applyBoundaires.x, _leftBoundry.x, _rightBoundry.x);
            applyBoundaires.z = Mathf.Clamp(applyBoundaires.z, _bottomBoundry.z, _topBoundry.z);

            transform.position = applyBoundaires;
        }


        private void FindAveragePosition()
        {
            Vector3 averagePos = new Vector3();
            int numTargets = 0;

            // Go through all the targets and add their positions together.
            for (int i = 0; i < m_Targets.Length; i++)
            {
                // If the target isn't active, go on to the next one.
                if (!m_Targets[i].gameObject.activeSelf)
                    continue;

                // Add to the average and increment the number of targets in the average.
                averagePos += m_Targets[i].position;
                numTargets++;
            }

            // If there are targets divide the sum of the positions by the number of them to find the average.
            if (numTargets > 0)
                averagePos /= numTargets;

            // Keep the same y value.
            averagePos.y = transform.position.y;

            // The desired position is the average position;
            m_DesiredPosition = averagePos;
        }


        private void Zoom()
        {
            // Find the required size based on the desired position and smoothly transition to that size.
            float requiredSize = FindRequiredSize();
            // CE-101
            m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, ref m_ZoomSpeed, m_DampTime);
        }


        private float FindRequiredSize()
        {
            // Find the position the camera rig is moving towards in its local space.
            Vector3 desiredLocalPos = transform.InverseTransformPoint(m_DesiredPosition);

            // Start the camera's size calculation at zero.
            float size = 0f;

            // Go through all the targets...
            for (int i = 0; i < m_Targets.Length; i++)
            {
                // ... and if they aren't active continue on to the next target.
                if (!m_Targets[i].gameObject.activeSelf)
                    continue;

                // Otherwise, find the position of the target in the camera's local space.
                Vector3 targetLocalPos = transform.InverseTransformPoint(m_Targets[i].position);

                // Find the position of the target from the desired position of the camera's local space.
                Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

                // Choose the largest out of the current size and the distance of the tank 'up' or 'down' from the camera.
                size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));

                // Choose the largest out of the current size and the calculated size based on the tank being to the left or right of the camera.
                size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / m_Camera.aspect);
            }

            // Add the edge buffer to the size.
            size += m_ScreenEdgeBuffer;

            // Make sure the camera's size is between Max and Min.            
            size = Mathf.Clamp(size, m_MinSize, m_MaxSize);

            return size;
        }

        public void SetStartPositionAndSize()
        {
            // Find the desired position.
            FindAveragePosition();

            // Set the camera's position to the desired position without damping.
            transform.position = m_DesiredPosition;

            // Find and set the required size of the camera.
            m_Camera.orthographicSize = FindRequiredSize();
        }
        /// <summary>
        /// CE-101
        /// Viewport projection  to scene to get camera limits
        /// </summary>
        private void SetWorldBoundaires()
        {
            RaycastHit hitTop;
            RaycastHit hitBottom;
            RaycastHit hitRight;
            RaycastHit hitLeft;

            Physics.Raycast(m_Camera.ViewportPointToRay(new Vector3(0.5f, m_TopBoundaire, Vector3.Distance(transform.position, m_Camera.transform.position))), out hitTop);
            Physics.Raycast(m_Camera.ViewportPointToRay(new Vector3(0.5f, m_BotBoundaire, Vector3.Distance(transform.position, m_Camera.transform.position))), out hitBottom);
            Physics.Raycast(m_Camera.ViewportPointToRay(new Vector3(m_RightBoundaire, 0.5f, Vector3.Distance(transform.position, m_Camera.transform.position))), out hitRight);
            Physics.Raycast(m_Camera.ViewportPointToRay(new Vector3(m_LeftBoundaire, 0.5f, Vector3.Distance(transform.position, m_Camera.transform.position))), out hitLeft);

            _topBoundry = hitTop.point;
            _bottomBoundry = hitBottom.point;
            _rightBoundry = hitRight.point;
            _leftBoundry = hitLeft.point;
        }

        //CE-101
        void OnDrawGizmos()
        {
            // boundaries Gizmos                                           
            if (!EditorApplication.isPlaying)
            {
                m_Camera = GetComponentInChildren<Camera>();
                SetWorldBoundaires();
            }

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(_topBoundry, 2);
            Gizmos.DrawSphere(_bottomBoundry, 2);
            Gizmos.DrawSphere(_rightBoundry, 2);
            Gizmos.DrawSphere(_leftBoundry, 2);

            // Zoom Gizmos            
            // Max Camera size
            Gizmos.color = Color.red;
            float maxVerticalHeightSeen = m_MinSize * 2.0f;
            DrawWireCube(m_Camera.transform.position, transform.rotation, new Vector3((maxVerticalHeightSeen * m_Camera.aspect), maxVerticalHeightSeen, 0));

            Gizmos.color = Color.magenta;
            // Min Camera size
            float minVerticalHeightSeen = m_MaxSize * 2.0f;
            DrawWireCube(m_Camera.transform.position, transform.rotation, new Vector3((minVerticalHeightSeen * m_Camera.aspect), minVerticalHeightSeen, 0));

            // Current Camera size
            Gizmos.color = Color.green;
            float currentVerticalHeightSeen = m_Camera.orthographicSize * 2.0f;
            DrawWireCube(m_Camera.transform.position, transform.rotation, new Vector3((currentVerticalHeightSeen * m_Camera.aspect), currentVerticalHeightSeen, 0));

        }


        // DrawWireCube rotate
        // https://answers.unity.com/questions/894217/confusion-with-gizmo-matrix-when-using-gizmo-cube.html
        public static void DrawWireCube(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Matrix4x4 cubeTransform = Matrix4x4.TRS(position, rotation, scale);
            Matrix4x4 oldGizmosMatrix = Gizmos.matrix;

            Gizmos.matrix *= cubeTransform;

            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

            Gizmos.matrix = oldGizmosMatrix;
        }
    }
}