using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SwitchUnitState(Player player)
    {
        player._state = CommonParam.UnitState.Immovable;
    }
}


//todo:
/*
 SwitchUnitSttate�̌Ăяo���Ƃ�������߂�
�N���[�P���̍U��������̃v���C���[�̐�����ԏ��������
�N���[�P���̍U�����UI�쐬�\��
UnityleaRningMaterial
 */