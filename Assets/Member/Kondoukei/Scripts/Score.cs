using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]
    private CommonParam.UnitType _unitType = CommonParam.UnitType.Player1;
    private int _score;
    [SerializeField]
    private Text _scoreText;
    // Start is called before the first frame update
    void Start()
    {
        if (_scoreText == null)
        {  _scoreText = gameObject.GetComponent<Text>(); }
    }

    // Update is called once per frame
    void Update()
    {
        _score = GameManager.Instance.Scores[(int)_unitType];
        _scoreText.text = _score.ToString();
    }
}
