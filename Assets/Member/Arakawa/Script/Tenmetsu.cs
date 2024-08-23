using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tenmetsu : MonoBehaviour
{
    [SerializeField]
    private Renderer _target;

    [SerializeField]
    private float _cycle = 1;

    private double _time;

    private void Update()
    {
        _time += Time.deltaTime;

        var repeatValue = Mathf.Repeat((float)_time, _cycle);
        
        _target.enabled = repeatValue >= _cycle * 0.5f;

    }
}