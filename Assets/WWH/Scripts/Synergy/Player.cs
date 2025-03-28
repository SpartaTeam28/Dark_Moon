using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    Dosa,
    KumGeok,
    KiSeng,
    Buddhist,
    Hunter,
    Confusianism
}

public class Player : MonoBehaviour
{
    public PlayerType playerType;
}
