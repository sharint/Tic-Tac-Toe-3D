using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardController : MonoBehaviour
{
    private Enemy enemy;
    private Player player;

    private const int countSectors = 9;
    private const int enemySectorIndex = -1; // default value

    private GameObject sectorPrefab;
    public static List<GameObject> sectors;

    public static Material cross;
    public static Material circle;

    private bool isPlayerTurnFirst;
    public static bool isGameOver;

    private UiViewController uiViewController;
    private SaveDataController saveDataController;

    private void Awake()
    {
        cross = Resources.Load<Material>("Materials/Sides/CrossMaterial");
        circle = Resources.Load<Material>("Materials/Sides/CircleMaterial");
        sectorPrefab = Resources.Load<GameObject>("Prefabs/SectorPrefab");
    }

    void Start()
    {
        saveDataController = new SaveDataController();
        isGameOver = false;
        sectors = new List<GameObject>();

        SetUiViewController();

        CreateTicTacToeBoard();

        CreateTicTacToePlayers();
        

        uiViewController.SetDefaultValuesText(enemy,player);

        SetSides();
        SetFirstTurn();

        if (!isPlayerTurnFirst)
        {
            enemy.Turn(enemySectorIndex);
        }

    }

    private void SetUiViewController()
    {
        GameObject uiViewControllerGameObject = GameObject.FindGameObjectWithTag("UI Controller");
        uiViewController = uiViewControllerGameObject.GetComponent<UiViewController>();
    }

    private void CreateTicTacToePlayers()
    {
        saveDataController.Load(SaveDataController.SaveDataTypes.StartGame);
        int enemyScore = 0;
        int playerScore = saveDataController.playerScore;
        string playerName = saveDataController.playerName;
        string enemyName = saveDataController.enemyName;

        player = new Player(playerName, playerScore);
        enemy = new Enemy(enemyName, enemyScore);
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

    private bool CheckingCondition(string playerCondition, TicTacToePlayer ticTacToePlayer)
    {
        if (playerCondition == GameSituations.win)
        {
            isGameOver = true;
            ticTacToePlayer.AddScore(100);
            return true;
        }
        else if (playerCondition == "Tie")
        {
            isGameOver = true;
            uiViewController.gameSituationStateText.text = playerCondition;
            StartCoroutine(EndGameSplashScreen());
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
        if (CheckingCondition(playerCondition, player))
        {
            EndGameSetup(enemy, player, GameSituations.win, "+");
            return;
        }

        string enemyCindition = enemy.Turn(enemySectorIndex);
        if (CheckingCondition(enemyCindition, enemy))
        {
            EndGameSetup(player, enemy, GameSituations.lose, "-");
            return;

        }
    }

    private void HideSectors()
    {
        foreach(GameObject sector in sectors)
        {
            sector.SetActive(false);
        }
    }

    private void EndGameSetup(TicTacToePlayer loserTicTacToePlayer, TicTacToePlayer winnerTicTacToePlayer, string gameSitution, string plusOrMinus)
    {
        loserTicTacToePlayer.RemoveScore(100);
        saveDataController.playerScore = player.GetScore();
        saveDataController.winner = winnerTicTacToePlayer.GetName();
        saveDataController.time = uiViewController.timer.text;
        saveDataController.Save(SaveDataController.SaveDataTypes.EndGame);
        HideSectors();
        uiViewController.gameSituationStateText.text = player.GetName() + " " + gameSitution + "\n" + plusOrMinus + "100 очков";
        StartCoroutine(EndGameSplashScreen());
    }

    private IEnumerator EndGameSplashScreen()
    {
        ScreenFader.Fader(3);
        yield return new WaitForSeconds(7);
        LoadingController.Load(1);
    }
}
