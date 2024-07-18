using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    Fire,
    wind,
    bright,
}
[System.Serializable]
public struct EffectData
{
    public EffectType EffectType;
    public ParticleSystem Particle;
}

public class EffectManager : SingletonMonoBehaviour<EffectManager>
{
    [SerializeField]
    private List<EffectData> effectData = new List<EffectData>();

    private GameObject dummyParticle = null;

    // Start is called before the first frame update
    void Start()
    {
        PlayEffect (EffectType.wind);
    }
    private void Update()
    {
        if (Input.GetKeyDown (KeyCode.P)) 
        {
            StopEffect();
        }
    }
    //instantiateオブジェクトのクローン
    public void PlayEffect(EffectType effectType)
    {
        var particle = effectData[(int)effectType].Particle;
        dummyParticle = Instantiate(particle.gameObject, Vector3.zero, Quaternion.identity);
        particle.Play();
    }
    public void StopEffect()
    {
        var particle = dummyParticle.GetComponent<ParticleSystem>();
        particle.Stop();
        Destroy(particle.gameObject);
    }
}

/*
    private GameObject doll = null; 
    doll = Instantiate(doll, Vector3.zero, Quaternion.identity); 
 */
