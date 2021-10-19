using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : TicTacToePlayer
{
    public Player(string name) : base(name) { }

    public override string Turn(int sectorIndex)
    {
        return base.Turn(sectorIndex);
    }
}