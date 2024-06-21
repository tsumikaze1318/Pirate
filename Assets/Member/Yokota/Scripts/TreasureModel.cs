using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureModel : MonoBehaviour
{
    public TreasurePlace Place;

    public void DestroyTreasure(int plNum)
    {
        if (Place == TreasurePlace.Sprit) GameManager.Instance.AddScore(plNum, true);
        else GameManager.Instance.AddScore(plNum, false);

        TreasureInstance treasureInstance = GetComponentInParent<TreasureInstance>();
        treasureInstance.GenerateTreasure(Place);
    }
}

public enum TreasurePlace
{
    Main,
    Fore,
    Mizzen,
    Sprit,
    Null
}
