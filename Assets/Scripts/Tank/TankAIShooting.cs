using System.Collections;
using UnityEngine;

namespace Complete.Tank
{
    public class TankAIShooting : TankShooting
    {
        public float m_RecoveryTime = 0.5f;
        [HideInInspector] public bool fire = false;

        private float m_fireForce = 0;

        public void Shoot(float force)
        {
            if (!m_Fired)
            {
                m_Fired = true;
                m_fireForce = Mathf.Clamp(force, m_MinLaunchForce, m_MaxLaunchForce);
                StartCoroutine(FireCO());
            }
        }

        public IEnumerator FireCO()
        {
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