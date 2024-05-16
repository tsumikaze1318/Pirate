using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    Grab,
    Strike
}

public class Kraken : MonoBehaviour
{
    // �v���C���[�̃I�u�W�F�N�g���i�[����List
    [SerializeField]
    private GameObject[] players;

    [SerializeField]
    private List<Material> materials;

    // �񋓌^����Y������}�e���A�����Ăяo��Dictionary
    private Dictionary<ActionType, Material> actionTypeToMaterial = new Dictionary<ActionType, Material>();

    // �U���͈�UI
    private GameObject cautionUI;

    private void Start()
    {
        // �v���C���[�^�O�̂����I�u�W�F�N�g�����ׂĎ擾
        players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < Enum.GetNames(typeof(ActionType)).Length; i++) 
        {
            actionTypeToMaterial.Add(ActionType.Strike, materials[i]);
        }
    }

    private void Update()
    {
        
    }
}
