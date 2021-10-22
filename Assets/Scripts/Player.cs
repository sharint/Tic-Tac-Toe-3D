using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : TicTacToePlayer
{
    public Player(string name, int score) : base(name,score) { }

    public override string Turn(int sectorIndex)
    {
        return base.Turn(sectorIndex);
    }
}