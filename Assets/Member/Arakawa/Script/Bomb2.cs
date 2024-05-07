using UnityEngine;

namespace ExplosionSample
{
    public class Bomb2 : MonoBehaviour
    {
        [Header("�����܂ł̎���[s]")]
        [SerializeField]
        private float _time = 3.0f;

        [Header("������Prefab")][SerializeField] private Explosion _explosionPrefab;

        private void Start()
        {
            // ��莞�Ԍo�ߌ�ɔ���
            Invoke(nameof(Explode), _time);
        }

        private void Explode()
        {
            // �����𐶐�
            var explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            explosion.Explode();

            // ���g�͏�����
            Destroy(gameObject);
        }
    }
}
