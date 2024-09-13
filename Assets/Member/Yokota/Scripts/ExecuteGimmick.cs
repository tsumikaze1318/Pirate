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

    private Camera[] _cameras;

    private ParticleSystem _confetti;

    [SerializeField]
    private SceneFadeInput _fadeInput;

    private int[] _posX;

    private int _phase = 0;
    private int _callGimmickCount = 0;

    private async void Start()
    {
        _callGimmickCount = GameManager.PlayerNumToScore.Count;

        _cameras = new Camera[_callGimmickCount];
        _posX = new int[_callGimmickCount];

        for (int i = 0; i < _callGimmickCount; i++)
        {
            _posX[i] = GameManager
                .PlayerNumToScore
                .ElementAt(_callGimmickCount - 1 - i).Key - 1;

            Debug.Log(_posX[i]);
        }

        _executeCannon = GetComponentInChildren<ExecuteCannon>();
        _executeShark = GetComponentInChildren<ExecuteSharkShoot>();
        _executeKraken = GetComponentInChildren<ExecuteKraken>();

        _cameras = GetComponentsInChildren<Camera>();
        _confetti = GetComponentInChildren<ParticleSystem>();

        _confetti.Pause();

        await Task.Delay(2000);

        switch (_posX.Length)
        {
            case 2:
                AnimationKraken();
                break;
            case 3:
                AnimationShark();
                break;
            case 4:
                AnimationCannon();
                break;
            default:
                Debug.Log("Error");
                return;
        }
    }

    private void AnimationCannon()
    {
        int cannonTarget = 3 - (_posX[_phase] * 2);

        _gimmicks[0].transform
            .DOMoveX(cannonTarget, 1)
            .OnComplete(async () =>
            {
                Debug.Log("aaa");
                _phase++;
                _executeCannon.Fire();

                await Task.Delay(1000);

                AnimationShark();
            });
    }

    private async void AnimationShark()
    {
        _gimmicks[1].transform.position
            = _gimmicks[1].transform.position
            + new Vector3((3 - (_posX[_phase] * 2)) - 1.35f, 0, 0);

        _executeShark.ThrowingBall();

        _phase++;

        await Task.Delay(1000);

        AnimationKraken();
    }

    private async void AnimationKraken()
    {
        float appearYPos = -16f;

        _gimmicks[2].transform
                .DOMoveY(appearYPos, 2f);

        await Task.Delay(2000);

        float targetXPos = 3 - (_posX[_phase] * 2);
        float fakeTargetXPos = 3 - (_posX[_phase + 1] * 2);

        float moveTime = 1f;
        int moveMillionTime = (int)moveTime * 1000;

        if (Random.Range(0, 2) == 0) 
        {
            FocusFakeTarget(moveTime, fakeTargetXPos);
            await Task.Delay(moveMillionTime);
            FocusTarget(moveTime, targetXPos);
            await Task.Delay(moveMillionTime);
        }
        else
        {
            FocusTarget(moveTime, targetXPos);
            await Task.Delay(moveMillionTime);
            FocusFakeTarget(moveTime, fakeTargetXPos);
            await Task.Delay(moveMillionTime);
        }

        _executeKraken.Attack();

        _gimmicks[2].transform
            .DOMoveX(0, 3)
            .SetEase(Ease.InOutQuad);
        await Task.Delay(3000);

        _gimmicks[2].transform
            .DOMoveX(targetXPos, moveTime);
        await Task.Delay(moveMillionTime);

        _phase++;
        Debug.Log(_phase);
        Debug.Log(_posX[_phase]);

        await Task.Delay(2000);

        AnimationCamera();
    }

    private void FocusTarget(float moveTime, float targetXPos)
    {
        _gimmicks[2].transform
                .DOMoveX(targetXPos * 3, moveTime)
                .SetEase(Ease.InOutQuad);
    }

    private void FocusFakeTarget(float moveTime, float fakeTargetXPos)
    {
        _gimmicks[2].transform
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
        _fadeInput.SetAcceptInput(true);
    }
}
