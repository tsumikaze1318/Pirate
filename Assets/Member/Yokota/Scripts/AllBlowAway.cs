using DG.Tweening;
using UnityEngine;

public class AllBlowAway : MonoBehaviour
{
    private ExecuteKraken[] _executeKrakens;

    private void Start()
    {
        _executeKrakens = GetComponentsInChildren<ExecuteKraken>();
    }

    public void Attack()
    {
        foreach (var kraken in _executeKrakens)
        {
            kraken.transform.DOMoveY(-16.5f, 2f);
            kraken.Attack();
        }
    }
}
