using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureModel : MonoBehaviour
{
    public TreasurePlace Place;

    public void DestroyTreasure()
    {
        TreasureInstance treasureRandom = GetComponentInParent<TreasureInstance>();
        treasureRandom.RandomInstance(Place);
    }
}

public enum TreasurePlace
{
    Midship,
    Fore,
    Stern,
    Null
}
