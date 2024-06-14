using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        print($"�v���C���[#{playerInput.user.index}�������I");
    }

    // �v���C���[�ގ����Ɏ󂯎��ʒm
    public void OnPlayerLeft(PlayerInput playerInput)
    {
        print($"�v���C���[#{playerInput.user.index}���ގ��I");
    }
}
