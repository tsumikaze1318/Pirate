using DG.Tweening;
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

    private Camera[] _cameras = new Camera[4];

    private ParticleSystem _confetti;

    private int[] _posX = new int[4];

    private int _phase = 0;

    private async void Start()
    {
        for (int i = 0; i < _posX.Length; i++)
        {
            _posX[i] = GameManager.ScoreToPlayerNum.ElementAt(GameManager.ScoreToPlayerNum.Count - 1 - i).Key;
            Debug.Log(_posX[i]);
        }

        _executeCannon = GetComponentInChildren<ExecuteCannon>();
        _executeShark = GetComponentInChildren<ExecuteSharkShoot>();
        _executeKraken = GetComponentInChildren<ExecuteKraken>();

        _cameras = GetComponentsInChildren<Camera>();
        _confetti = GetComponentInChildren<ParticleSystem>();

        _confetti.Pause();

        await Task.Delay(5000);

        AnimationCannon();
    }

    private void AnimationCannon()
    {
        int cannonTarget = 3 - (_posX[_phase] * 2);

        _gimmicks[_phase].transform
            .DOMoveX(cannonTarget, 1)
            .OnComplete(async () =>
            {
                _phase++;
                _executeCannon.Fire();

                await Task.Delay(3000);

                AnimationShark();
            });
    }

    private async void AnimationShark()
    {
        _gimmicks[_phase].transform.position
            = _gimmicks[_phase].transform.position
            + new Vector3(3 - (_posX[_phase] * 2) -1.35f, 0, 0);

        _executeShark.ThrowingBall();

        _phase++;

        await Task.Delay(3000);

        AnimationKraken();
    }

    private async void AnimationKraken()
    {
        float appearYPos = -16f;

        _gimmicks[_phase].transform
                .DOMoveY(appearYPos, 3f);

        await Task.Delay(3000);

        float targetXPos = 3 - (_posX[_phase] * 2);
        float fakeTargetXPos = 3 - (_posX[_phase + 1] * 2);

        float moveTime = 1f;
        int moveMillionTime = (int)moveTime * 1000;

        if (Random.Range(0, 2) == 0) 
        {
            FocusTarget(moveTime, targetXPos);
            await Task.Delay(moveMillionTime);
            FocusFakeTarget(moveTime, fakeTargetXPos);
            await Task.Delay(moveMillionTime);
            FocusTarget(moveTime, targetXPos);
            await Task.Delay(moveMillionTime);
        }
        else
        {
            FocusFakeTarget(moveTime, fakeTargetXPos);
            await Task.Delay(moveMillionTime);
            FocusTarget(moveTime, targetXPos);
            await Task.Delay(moveMillionTime);
            FocusFakeTarget(moveTime, fakeTargetXPos);
            await Task.Delay(moveMillionTime);
        }

        _gimmicks[_phase].transform
            .DOMoveX(0, 3)
            .SetEase(Ease.InOutQuad);
        await Task.Delay(3000);

        _executeKraken.Attack();
        await Task.Delay(3000);

        _gimmicks[_phase].transform
            .DOMoveX(targetXPos, moveTime);
        await Task.Delay(moveMillionTime);

        _phase++;

        await Task.Delay(6000);

        AnimationCamera();
    }

    private void FocusTarget(float moveTime, float targetXPos)
    {
        _gimmicks[_phase].transform
                .DOMoveX(targetXPos * 3, moveTime)
                .SetEase(Ease.InOutQuad);
    }

    private void FocusFakeTarget(float moveTime, float fakeTargetXPos)
    {
        _gimmicks[_phase].transform
                .DOMoveX(fakeTargetXPos * 3, moveTime)
                .SetEase(Ease.InOutQuad);
    }

    private async void AnimationCamera()
    {
        foreach (var cam in _cameras)
        {
            cam.transform.DOMove(new Vector3(3 - (_posX[_phase] * 2), -2, -12), 1);
        }

        await Task.Delay(1000);

        _confetti.Play();
    }
}
