using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BoardController : MonoBehaviour
{
    public Text gameSituationStateText;

    private Enemy enemy;
    private Player player;

    private const int countSectors = 9;

    private GameObject sectorPrefab;
    public static List<GameObject> sectors;

    public static Material cross;
    public static Material circle;

    public enum States { none, cross, circle };
    private enum Sides { cicle, cross };

    private bool isPlayerTurnFirst;
    private bool isGameOver = false;

    private void Awake()
    {
        cross = Resources.Load<Material>("Materials/Sides/CrossMaterial");
        circle = Resources.Load<Material>("Materials/Sides/CircleMaterial");
        sectorPrefab = Resources.Load<GameObject>("Prefabs/SectorPrefab");
    }

    void Start()
    {
        sectors = new List<GameObject>();

        CreateTicTacToeBoard();

        enemy = new Enemy("Enemy");
        player = new Player("Player");

        SetSides();
        SetFirstTurn();

        if (!isPlayerTurnFirst)
        {
            int randomInt = 0;
            enemy.Turn(randomInt);
        }

    }

    private void SetFirstTurn()
    {
        if(player.GetSide() == TicTacToePlayer.Sides.cross)
        {
            isPlayerTurnFirst = true;
        }
        else
        {
            isPlayerTurnFirst = false;
        }
    }

    private void SetSides()
    {
        while (IsEqualSides())
        {
            GetRandomSide();
        }
        
    }

    private bool IsEqualSides()
    {
        return player.GetSide() == enemy.GetSide();
    }

    private void GetRandomSide()
    {
        List<TicTacToePlayer> players = GetListOfPlayers();
        SetSideForEachPlayer(players);
        
    }

    private List<TicTacToePlayer> GetListOfPlayers()
    {
        List<TicTacToePlayer> players = new List<TicTacToePlayer>();
        players.Add(player);
        players.Add(enemy);
        return players;
    }

    private void SetSideForEachPlayer(List<TicTacToePlayer> players)
    {
        for (int i = 0; i < players.Count; i++)
        {
            int randomNumber = GetRandomNumber();
            TicTacToePlayer.Sides side = SwitchSide(randomNumber);
            players[i].SetSide(side);
        }
    }

    private int GetRandomNumber()
    {
        return Random.Range(0, 2);
    }

    private TicTacToePlayer.Sides SwitchSide(int sideIndex)
    {
        switch (sideIndex)
        {
            case 0:
                return TicTacToePlayer.Sides.circle;
            case 1:
                return TicTacToePlayer.Sides.cross;
            default:
                return TicTacToePlayer.Sides.none;
        }
    }

    private void CreateTicTacToeBoard()
    {
        for (int i = 0; i < countSectors; i++)
        {
            GameObject sector = CreateSector(i);
            SetupSector(sector,i);
        }
    }

    private GameObject CreateSector(int index)
    {
        float sectorX = index % 3;
        float sectorY = index / 3;
        Vector3 position = new Vector3(sectorX, sectorY, 0);
        Quaternion rotation = sectorPrefab.transform.rotation;

        GameObject sector = Instantiate(sectorPrefab, position, rotation);
        return sector;
    }

    private void SetupSector(GameObject sector, int id)
    {
        sector.transform.SetParent(gameObject.transform);
        sector.GetComponent<Sector>().sectorIndex = id;
        sectors.Add(sector);
    }

    private bool CheckingCondition(string playerCondition)
    {
        if (playerCondition != "")
        {
            gameSituationStateText.text = playerCondition;
            isGameOver = true;
            return true;
        }
        return false;
    }

    public void PlayerTapped(int sectorIndex)
    {
        if (isGameOver)
        {
            return;
        }
        string playerCondition = player.Turn(sectorIndex);
        if (CheckingCondition(playerCondition)) return;
        string enemyCindition = enemy.Turn(sectorIndex);
        if (CheckingCondition(enemyCindition)) return;
    }
}
