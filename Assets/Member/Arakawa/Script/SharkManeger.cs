using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SharkManeger : MonoBehaviour
{
    // ��C�̃X�N���v�g���i�[���郊�X�g
    [SerializeField]
    private List<SharkRandamInstance> _SharkInstance = new List<SharkRandamInstance>();
    private float _timeElapsed;

    public void SetInitialTime(float time)
    {
        _timeElapsed = time;
    }

    private void Start()
    {
        // �O�̂��߃��X�g���N���A����
        _SharkInstance.Clear();
        // �q�K�w�ɑ��݂����C�̃X�N���v�g��z��Ŏ擾
        var shrak = GetComponentsInChildren<SharkRandamInstance>();
        //�z��̒����Ń��[�v
        for (int i = 0; i < shrak.Length; i++)
        {
            _SharkInstance.Add(shrak[i]);


        }
        //for (int i = 0; i < shrak.Length; i++)
        //{

        //}
    }

    private void Update()
    {
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameEnd) return;
        _timeElapsed += Time.deltaTime;
        if(_timeElapsed > 3)
        {
            SharkInstance();
            _timeElapsed = 0;
        }
    }

    private void SharkInstance()
    {
        float rand = Random.Range(0f, 1f);
        
        if (rand < 0.1f)
        {
            _SharkInstance[0].RandamInstance();
            _SharkInstance[1].RandamInstance();
            _SharkInstance[2].RandamInstance();
        }
        else if (rand < 0.2f)
        {
            _SharkInstance[0].RandamInstance();
            _SharkInstance[2].RandamInstance();
            _SharkInstance[3].RandamInstance();
        }
        else if (rand < 0.3f)
        {
            _SharkInstance[1].RandamInstance();
            _SharkInstance[2].RandamInstance();
            _SharkInstance[3].RandamInstance();
        }
        else
        {
            int randInt = Random.Range(0, 4);
            _SharkInstance[randInt].RandamInstance();
        }
    }

    //async void Shark()
    //{
    //    var time = Random.Range(0f, 10f);
    //    if(time <= 3)
    //    {
    //        //�����ɏo������
    //        for (int i = 0; i < shrak.Length; i++)
    //        {
    //            _SharkInstance.Add(shrak[i]);

    //        }

    //    }
    //    else
    //    {
    //        //��̂�
    //        await Task.Delay(3000);
    //        //��̖�


    //    }
    //}
}
