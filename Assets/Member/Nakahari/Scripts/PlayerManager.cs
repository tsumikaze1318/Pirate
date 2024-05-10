using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] _playerPrefabs;

    public void OnPlayerJoined(PlayerInput input)
    {
        int index = input.playerIndex;

        if(index < _playerPrefabs.Length)
        {
            GameObject selectedPrefab = _playerPrefabs[index];
            GameObject playerInstance = Instantiate(selectedPrefab,transform.position,Quaternion.identity);

            input.transform.SetParent(playerInstance.transform, false);
            input.GetComponent<PlayerInput>().enabled = true;
        }
    }
}
