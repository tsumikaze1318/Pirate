using UnityEngine;

public class NewBehaviourScript: MonoBehaviour
{
    [SerializeField] ParticleSystem m_particle;
    [SerializeField] float m_force = 20;
    [SerializeField] float m_radius = 5;
    [SerializeField] float m_upwards = 0;
    Vector3 m_position;

    /*void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Explosion();
        }
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        Explosion();
    }

    public void Explosion ()
    {
        m_particle.Play();
        m_position = m_particle.transform.position;

        // ”ÍˆÍ“à‚ÌRigidbody‚ÉAddExplosionForce
        Collider[] hitColliders = Physics.OverlapSphere(m_position, m_radius);
        for(int i = 0; i < hitColliders.Length; i++)
        {
            var rb = hitColliders[i].GetComponent<Rigidbody>();
            if(rb)
            {
                rb.AddExplosionForce(m_force, m_position, m_radius, m_upwards, ForceMode.Impulse);
            }
        }
    }
}