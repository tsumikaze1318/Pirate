using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    // ���ݔ͈͂ɓ����Ă���I�u�W�F�N�g�i�[���X�g
    private List<GameObject> canGrabObjects = new List<GameObject>();
    // ����ł���I�u�W�F�N�g
    private GameObject grabObject = null;
    // �v���C���[�{��
    private GameObject playerObj = null;
    // �v���C���[�N���X
    private Player player = null;
    // 1�t���[���O�̃v���C���[��EulerAngle
    private Vector3 preRot;
    // ���񂾃I�u�W�F�N�g�ƃv���C���[�Ƃ̃x�N�g��
    private Vector3 offset = Vector3.zero;
    // ���ݔ͈�
    private MeshCollider grabCollider;

    private void Start()
    {
        // Null�`�F�b�N
        playerObj ??= GetComponentInParent<Player>().gameObject;
        player ??= GetComponentInParent<Player>();
        grabCollider ??= GetComponent<MeshCollider>();
    }

    private void Update()
    {
        // ���X�|�[�����̂Ƃ��R���C�_�[�𖳌����A����ȊO�͗L��
        if (player._respawn) grabCollider.enabled = false;
        else grabCollider.enabled = true;

        // ��������ł��Ȃ����return
        if (grabObject == null) return;

        // ����EulerAngle���擾
        Vector3 nowRot = playerObj.transform.localEulerAngles;
        // 1�t���[���ŕω�����y���̊p�x�̃��W�A�����v�Z
        float rad = (nowRot.y - preRot.y) * Mathf.Deg2Rad;

        // �x�N�g���̉�]�v�Z
        float x = offset.x * Mathf.Cos(rad) + offset.z * Mathf.Sin(rad);
        float y = offset.y;
        float z = offset.x * -Mathf.Sin(rad) + offset.z * Mathf.Cos(rad);
        // offset���X�V
        offset = new Vector3(x, y, z);

        // ����ł���I�u�W�F�N�g�̈ʒu�Ɖ�]���X�V
        grabObject.transform.position = playerObj.transform.position + offset;
        grabObject.transform.localEulerAngles += nowRot - preRot;

        // preRot���X�V
        preRot = nowRot;
    }

    /// <summary>
    /// �������ފ֐�
    /// </summary>
    public void Grab()
    {
        // ��������ł�����return
        if (grabObject != null) return;

        // ���X�g����null�����ׂč폜
        canGrabObjects.RemoveAll(item => item == null);
        // �I�u�W�F�N�g�Ƃ̋�����r�ϐ�
        float minDis = float.MaxValue;

        // ���ݔ͈͂̃I�u�W�F�N�g�̐��������[�v
        foreach (GameObject obj in canGrabObjects)
        {
            // �I�u�W�F�N�g�Ƃ̋������Z�o
            float distance = Mathf.Abs((obj.transform.position - transform.position).magnitude);
            // ��������ԋ߂����
            if (distance < minDis)
            {
                // minDis���X�V
                minDis = distance;
                // ����ł���I�u�W�F�N�g���X�V
                grabObject = obj;
            }
        }

        // ��������ł�����
        if (grabObject != null)
        {
            // ���X�g���N���A
            canGrabObjects.Clear();
            // �v���C���[�ƃI�u�W�F�N�g�Ƃ̃x�N�g�����Z�o
            offset = grabObject.transform.position - playerObj.transform.position;
            // �I�u�W�F�N�g��EulerAngles���擾
            preRot = playerObj.transform.localEulerAngles;
            // �����ǋL
            Physics.IgnoreCollision(grabObject.GetComponent<Collider>(), player._playerCollider, true);
        }
    }

    /// <summary>
    /// ����ł�����̂�������֐�
    /// </summary>
    public void Release()
    {
        // ��������ł��Ȃ����return
        if (grabObject == null) return;
        // �����ǋL
        Physics.IgnoreCollision(grabObject.GetComponent<Collider>(), player._playerCollider, false);
        // ����ł����I�u�W�F�N�g�̐e�I�u�W�F�N�g��null
        grabObject.transform.parent = null;
        // ���݃I�u�W�F�N�g��null
        grabObject = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ��������ł�����return
        if (grabObject != null) return;
        // ���߂�I�u�W�F�N�g�łȂ����return
        if (other.gameObject.layer != 6) return;

        // ���X�g�Ɋi�[
        canGrabObjects.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        // ���[�v�̃J�E���g
        int count = 0;

        foreach (GameObject obj in canGrabObjects)
        {
            // ���ꂽ�I�u�W�F�N�g�Ɠ����Ƃ�
            if (obj == other.gameObject)
            {
                // ���̃I�u�W�F�N�g��Remove���ďI��
                canGrabObjects.RemoveAt(count);
                break;
            }
            // �J�E���g�A�b�v
            count++;
        }
    }
}
