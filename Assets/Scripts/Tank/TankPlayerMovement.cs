using UnityEngine;

namespace Complete
{
    public class TankPlayerMovement : TankMovement
    {
        private string m_MovementAxisName;          // The name of the input axis for moving forward and back.
        private string m_TurnAxisName;              // The name of the input axis for turning.

        private float m_MovementInputValue;         // The current value of the movement input.
        private float m_TurnInputValue;             // The current value of the turn input.

        protected override void Start()
        {
            base.Start();
            // The axes names are based on player number.
            m_MovementAxisName = "Vertical" + m_PlayerNumber;
            m_TurnAxisName = "Horizontal" + m_PlayerNumber;
        }


        protected override void Update()
        {
            // Store the value of both input axes.
            m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
            m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

            base.Update();
        }

        private void FixedUpdate()
        {
            // Adjust the rigidbodies position and orientation in FixedUpdate.
            Move();
            Turn();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            // When the tank is turned on, make sure it's not kinematic.
            m_Rigidbody.isKinematic = false;

            // Also reset the input values.
            m_MovementInputValue = 0f;
            m_TurnInputValue = 0f;
        }



        private  void Move()
        {
            m_Moving = Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f ? true : false;
            // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
            Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

            // Apply this movement to the rigidbody's position.
            Vector3 desiredPosition = m_Rigidbody.position + movement;

            // Clamp movement to viewport
            Vector3 viewPosition = Camera.main.WorldToViewportPoint(desiredPosition);
            viewPosition.x = Mathf.Clamp(viewPosition.x, 0.05f, 0.95f);
            viewPosition.y = Mathf.Clamp(viewPosition.y, 0.05f, 0.95f);
            desiredPosition = Camera.main.ViewportToWorldPoint(viewPosition);

            m_Rigidbody.MovePosition(desiredPosition);
            m_Velocity = m_Rigidbody.velocity;
        }


        private  void Turn()
        {
            // Determine the number of degrees to be turned based on the input, speed and time between frames.
            float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

            // Make this into a rotation in the y axis.
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

            // Apply this rotation to the rigidbody's rotation.
            m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
        }

    }
}