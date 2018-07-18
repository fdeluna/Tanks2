using System.Collections;
using UnityEngine;

namespace Complete.Tank
{
    /// <summary>
    ///  AI inputs for Tank shooting
    /// </summary>
    public class TankAIShooting : TankShooting
    {
        public float m_RecoveryTime = 0.5f;        // Tank fire rate

        private float m_fireForce = 0; 

        /// <summary>
        /// Ai tank shooting
        /// </summary>
        /// <param name="force"> shooting force</param>
        public void Shoot(float force)
        {
            if (!m_Fired)
            {
                m_Fired = true;
                m_fireForce = Mathf.Clamp(force, m_MinLaunchForce, m_MaxLaunchForce);
                StartCoroutine(FireCO());
            }
        }

        /// <summary>
        ///  Coroutine to simulate player input
        /// </summary>
        /// <returns></returns>
        private IEnumerator FireCO()
        {
            m_Fired = true;
            PlaySFX();
            while (m_CurrentLaunchForce < m_fireForce)
            {
                m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;
                m_AimSlider.value = m_CurrentLaunchForce;
                yield return null;
            }

            Fire();
            m_AimSlider.value = 0;
            yield return new WaitForSeconds(m_RecoveryTime);
            m_Fired = false;
        }
    }
}