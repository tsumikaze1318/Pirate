using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    // オブジェクトが特定のタグに触れた際に削除
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("UnderGround"))
        {
            Destroy(this.gameObject);
        }
    }
}
