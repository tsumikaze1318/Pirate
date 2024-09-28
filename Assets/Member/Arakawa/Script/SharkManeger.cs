using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SharkManeger : MonoBehaviour
{
    // 大砲のスクリプトを格納するリスト
    [SerializeField]
    private List<SharkRandamInstance> _SharkInstance = new List<SharkRandamInstance>();
    private float _timeElapsed;

    public void SetInitialTime(float time)
    {
        _timeElapsed = time;
    }

    private void Start()
    {
        // 念のためリストをクリアする
        _SharkInstance.Clear();
        // 子階層に存在する大砲のスクリプトを配列で取得
        var shrak = GetComponentsInChildren<SharkRandamInstance>();
        //配列の長さでループ
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
    //        //同時に出す処理
    //        for (int i = 0; i < shrak.Length; i++)
    //        {
    //            _SharkInstance.Add(shrak[i]);

    //        }

    //    }
    //    else
    //    {
    //        //一体の
    //        await Task.Delay(3000);
    //        //二体目


    //    }
    //}
}
