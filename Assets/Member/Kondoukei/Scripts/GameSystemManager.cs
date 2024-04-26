using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystemManager : MonoBehaviour
{
    [SerializeField]
    private CommonParam.UnitType _unitType = CommonParam.UnitType.Player1;
    public int Score;
    public Text ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = Score.ToString();
    }
}
