using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToePlayer: ITicTacToePlayer
{
    //CONST
    private int[,] winConditions = new int[8, 3] { { 0, 1, 2 }, { 0, 3, 6 }, { 0, 4, 8 }, {1, 4, 7 }, { 2, 4, 6 }, { 2, 5, 8 }, { 3, 4, 5 }, { 6, 7, 8 } };
    private string name;

    private int score;

    private static int countTurns;

    private static List<int> freeSectors;

    private List<int> playerSectors;

    public static List<GameObject> allSectors;

    private Sides side;
    public enum Sides { none, circle, cross };

    public TicTacToePlayer(string name, int score)
    {
        this.name = name;
        this.score = score;
        allSectors = BoardController.sectors;
        playerSectors = new List<int>();
        freeSectors = new List<int>();
        for(int i = 0; i < 9; i++)
        {
            freeSectors.Add(i);
        }
        side = Sides.none;
        countTurns = 0;
    }

    public string GetName()
    {
        return name;
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int score)
    {
        this.score += score;
    }

    public void RemoveScore(int score)
    {
        this.score -= score;
    }

    public List<int> GetFreeSectors()
    {
        return freeSectors;
    }

    public void RemoveFreeSector(int sectorIndex)
    {
        freeSectors.Remove(sectorIndex);
    }

    public List<int> GetPlayerSectors()
    {
        return playerSectors;
    }

    public void AddPlayerSectors(int sectorIndex)
    {
        playerSectors.Add(sectorIndex);
    }

    public void SetSide(Sides side)
    {
        this.side = side;
    }

    public Sides GetSide()
    {
        return side;
    }

    private void SetStateSector(Sides side, int sectorIndex)
    {
        GameObject sector = allSectors[sectorIndex];
        switch (side)
        {
            case Sides.circle:
                sector.GetComponent<Renderer>().material = BoardController.circle;
                sector.GetComponent<Sector>().state = Sides.circle;
                break;
            case Sides.cross:
                sector.GetComponent<Renderer>().material = BoardController.cross;
                sector.GetComponent<Sector>().state = Sides.cross;
                break;
            default:
                Debug.Log("Erorr, unknown state");
                break;
        }
    }

    public bool IsTie()
    {
        return countTurns >= 9;
    }

    public string IsGameOver()
    {
        if (playerSectors.Count < 3)
        {
            return "";
        }
        for (int i = 1; i < 5; i++)
        {
            if (CheckingOneOfTheWinningConditions(i))
            {
                return GameSituations.win;
            }
        }
        if (IsTie())
        {
            return "Tie";
        }
        return "";
    }

    private bool CheckingOneOfTheWinningConditions(int step)
    {
        for(int i = 0; i < 8; i++)
        {
            int counter = 0;
            for(int k = 0; k < 3; k++)
            {
                if(playerSectors.Contains(winConditions[i,k]))
                {
                    counter++;
                }
            }
            if (counter >= 3)
            {
                return true;
            }
        }
        return false;

    }

    public virtual string Turn(int sectorIndex)
    {
        RemoveFreeSector(sectorIndex);
        AddPlayerSectors(sectorIndex);
        SetStateSector(side, sectorIndex);
        countTurns++;
        string condition = IsGameOver();
        return condition;
    }
}
