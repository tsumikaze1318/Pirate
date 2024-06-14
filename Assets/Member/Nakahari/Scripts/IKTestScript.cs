using CustomPrimitiveColliders;
using UnityEngine;

public class IKTestScript : MonoBehaviour
{
    bool _ikActive;
    float _ikWeight;
    public GameObject _targetObj;
    Animator _animator;
    // Start is called before the first frame update

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK()
    {
        if(_ikActive)
        {
            _ikWeight += Time.deltaTime;
            if (_ikWeight > 1.0f)
            {
                _ikWeight = 1.0f;
            }
        }
        else
        {
            if (_ikWeight > 0.0f)
            {
                _ikWeight -= Time.deltaTime;
                if (_ikWeight < 0.0f)
                {
                    _ikWeight = 0.0f;
                }
            }
        }

        if(_targetObj != null)
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, _ikWeight);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, _ikWeight);
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, _ikWeight);
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, _ikWeight);
            Vector3 pos = _targetObj.transform.position;
            pos.z = transform.position.z;
            _animator.SetIKPosition(AvatarIKGoal.RightHand, pos);
            _animator.SetIKRotation(AvatarIKGoal.RightHand, _targetObj.transform.rotation);
            _animator.SetIKPosition(AvatarIKGoal.LeftHand, pos);
            _animator.SetIKRotation(AvatarIKGoal.LeftHand, _targetObj.transform.rotation);
        }
        Debug.Log(_targetObj);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _ikActive = true;
        }
        else
        {
            _ikActive = false;
        }
    }

}
