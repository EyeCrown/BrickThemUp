using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public static class Globals
{
    public enum GameState { OnMenu, OnGame };

    public static GameState gameState = GameState.OnMenu;

}
