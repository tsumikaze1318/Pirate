using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    // ��C�̃X�N���v�g���i�[���郊�X�g
    [SerializeField]
    private List<FireBullet> _fireBullets = new List<FireBullet>();

    private void Start()
    {
        // �O�̂��߃��X�g���N���A����
        _fireBullets.Clear();
        // �q�K�w�ɑ��݂����C�̃X�N���v�g��z��Ŏ擾
        var cannons = GetComponentsInChildren<FireBullet>();
        // �z��̒����Ń��[�v
        for (int i = 0; i < cannons.Length; i++)
        {
            // ���X�g�Ɋi�[
            _fireBullets.Add(cannons[i]);
            // �o�ߎ��Ԃ�i�ŏ���������
            _fireBullets[i].SetInitialTime(i);
        }
    }
}
