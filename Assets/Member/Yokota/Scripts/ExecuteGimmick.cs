using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class ExecuteGimmick : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _gimmicks = new List<GameObject>();

    private ExecuteCannon _executeCannon;
    private ExecuteSharkShoot _executeShark;
    private ExecuteKraken _executeKraken;

    private int[] _posX = new int[4];

    private int _phase = 0;

    private void Start()
    {
        for (int i = 0; i < _posX.Length; i++)
        {
            _posX[i] = GameManager.ScoreToPlayerNum.ElementAt(TestDictionary.KeyValuePairs.Count - 1 - i).Value;
            Debug.Log(_posX[i]);
        }

        //for (int i = 0; i < _posX.Length; i++)
        //{
        //    _gimmicks[i].transform.position = _gimmicks[i].transform.position + new Vector3(_posX[i] * 2 - 3, 0, 0);
        //}

        _executeCannon = GetComponentInChildren<ExecuteCannon>();
        _executeShark = GetComponentInChildren<ExecuteSharkShoot>();
        _executeKraken = GetComponentInChildren<ExecuteKraken>();

        AnimationCannon();
    }

    private async void AnimationCannon()
    {
        float deltaDistance = 0f;
        float totalDistance = 0f;

        int cannonTarget = 3 - (_posX[_phase] * 2);

        while (Mathf.Abs(totalDistance) < Mathf.Abs(cannonTarget))
        {
            deltaDistance = Time.deltaTime * Mathf.Abs(cannonTarget);
            totalDistance += deltaDistance;
            
            if (cannonTarget < 0) _gimmicks[_phase].transform.position -= new Vector3(deltaDistance, 0, 0);
            else _gimmicks[_phase].transform.position += new Vector3(deltaDistance, 0, 0);

            await Task.Yield();
        }

        _gimmicks[_phase].transform.position
            = new Vector3(cannonTarget
            , _gimmicks[_phase].transform.position.y
            , _gimmicks[_phase].transform.position.z);

        _phase++;

        _executeCannon.Fire();

        await Task.Delay(5000);

        AnimationShark();
    }

    private async void AnimationShark()
    {
        _gimmicks[_phase].transform.position
            = _gimmicks[_phase].transform.position
            + new Vector3(3 - (_posX[_phase] * 2) -1.35f, 0, 0);

        _executeShark.ThrowingBall();

        _phase++;

        await Task.Delay(5000);

        AnimationKraken();
    }

    private async void AnimationKraken()
    {
        //_gimmicks[_phase].transform.position
        //    = _gimmicks[_phase].transform.position
        //    + new Vector3(3 - (_posX[_phase] * 2), 0, 0);

        Debug.Log((3 - (_posX[_phase] * 2)));

        float initialYPos = _gimmicks[_phase].transform.position.y;
        float targetYPos = -16f;

        float distance = targetYPos - initialYPos;

        float deltaDistance = 0f;
        float totalDistance = 0f;

        while (totalDistance < distance)
        {
            deltaDistance = Time.deltaTime * distance / 3;
            totalDistance += deltaDistance;

            _gimmicks[_phase].transform.position 
                += new Vector3(0, deltaDistance, 0);

            await Task.Yield();
        }

        await Task.Delay(500);

        deltaDistance = 0f;
        totalDistance = 0f;

        distance = _gimmicks[_phase].transform.position.x 
                 - (3 - (_posX[_phase] * 2));

        while (Mathf.Abs(totalDistance) < Mathf.Abs(distance))
        {
            deltaDistance = Time.deltaTime * distance / 0.2f;
            totalDistance += deltaDistance;

            _gimmicks[_phase].transform.position
                -= new Vector3(deltaDistance, 0, 0);

            await Task.Yield();
        }

        // ƒNƒ‰[ƒPƒ“UŒ‚
        _executeKraken.Attack();
    }
}
