using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class ExecuteGimmick : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _gimmicks = new List<GameObject>();

    private AllBlowAway _allBlowAway;
    private ExecuteCannon _executeCannon;
    private ExecuteSharkShoot _executeShark;
    private ExecuteKraken _executeKraken;

    private Camera[] _cameras;

    private ParticleSystem _confetti;

    [SerializeField]
    private SceneFadeInput _fadeInput;

    private PlayerModelVanish[] _players;

    private Dictionary<int, int> _playerToScore = new Dictionary<int, int>();

    private int[] _posX;
    private int _phase = 0;

    private async void Start()
    {
        await Task.Delay(2000);

        _players = FindObjectsOfType<PlayerModelVanish>();

        _playerToScore = TestDictionary.KeyValuePairs;

        _cameras = new Camera[_playerToScore.Count];
        _posX = new int[_playerToScore.Count];

        // 全員０点
        if (_playerToScore.Values.All(x => x == 0))
        {
            // 全てのクラーケンを呼び出す
            _allBlowAway = GetComponentInChildren<AllBlowAway>();
            _allBlowAway.Attack();
            return;
        }

        for (int i = 0; i < _playerToScore.Count; i++)
        {
            _posX[i] = 3 - ((_playerToScore.ElementAt(i).Key - 1) * 2);
            Debug.Log(_posX[i]);
        }

        _executeCannon = GetComponentInChildren<ExecuteCannon>();
        _executeShark = GetComponentInChildren<ExecuteSharkShoot>();
        _executeKraken = GetComponentInChildren<ExecuteKraken>();
        _confetti = GetComponentInChildren<ParticleSystem>();
        _cameras = GetComponentsInChildren<Camera>();

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
                return;
        }
    }

    private void AnimationCannon()
    {
        if (TiedScore()) return;

        int cannonTarget = _posX[_phase];

        _gimmicks[0].transform
            .DOMoveX(cannonTarget, 1)
            .OnComplete(async () =>
            {
                _phase++;
                _executeCannon.Fire();

                await Task.Delay(1000);

                AnimationShark();
            });
    }

    private async void AnimationShark()
    {
        if (TiedScore()) return;

        _gimmicks[1].transform.position
            = _gimmicks[1].transform.position
            + new Vector3(_posX[_phase] - 1.35f, 0, 0);

        _executeShark.ThrowingBall();

        _phase++;

        await Task.Delay(1000);

        AnimationKraken();
    }

    private async void AnimationKraken()
    {
        if (TiedScore()) return;

        float appearYPos = -16f;

        _gimmicks[2].transform
                .DOMoveY(appearYPos, 2f);

        await Task.Delay(2000);

        float targetXPos = _posX[_phase];
        float fakeTargetXPos = _posX[_phase + 1];

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
            cam.transform.DOMove(new Vector3(_posX[_phase], -2, -12), 1);
        }

        await Task.Delay(1000);

        _confetti.Play();
        _fadeInput.SetAcceptInput(true);
        foreach (var player in _players)
        {
            player.DoWinAnimation();
        }
    }

    private bool TiedScore()
    {
        bool tiedScore = true;

        for (int i = 0; i < _playerToScore.Count - (_phase + 1); i++)
        {
            if (_playerToScore.ElementAt(i + _phase).Value
                != _playerToScore.ElementAt(i + _phase + 1).Value)
            {
                tiedScore = false;
                break;
            }
        }

        if (tiedScore)
        {
            // クラーケンが空振りする
            _executeKraken.Attack();
            _gimmicks[2].transform.position += new Vector3(_posX[_phase - 1], 0f, 0f);
            _gimmicks[2].transform.DOMoveY(-16.5f, 2f);

            return tiedScore;
        }

        return tiedScore;
    }
}
