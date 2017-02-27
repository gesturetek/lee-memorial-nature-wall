using UnityEngine;
using System.Collections;

public class petals : MonoBehaviour {

    public ParticleSystem pSystem;
    ParticleSystem.Particle[] m_Particles;
    public float m_Drift = 0.01f;

    private void LateUpdate(){
        
    }


    void Lift() {
        InitializeIfNeeded();
        int numParticlesAlive = pSystem.GetParticles(m_Particles);

        // Change only the particles that are alive
        for (int i = 0; i < numParticlesAlive; i++)
        {
            m_Particles[i].velocity += Vector3.up * m_Drift;
        }

        // Apply the particle changes to the particle system
        pSystem.SetParticles(m_Particles, numParticlesAlive);
    }
    void InitializeIfNeeded()
    {
        if (pSystem == null)
            pSystem = GetComponent<ParticleSystem>();

        if (m_Particles == null || m_Particles.Length < pSystem.maxParticles)
            m_Particles = new ParticleSystem.Particle[pSystem.maxParticles];
    }
}
