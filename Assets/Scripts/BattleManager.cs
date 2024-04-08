using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public const int boardX = 8;
    public const int boardY = 8;
    public static BattleManager Singleton { get; private set; }

    public List<Animist> animistPrefabs;
    public List<Spirit> spiritPrefabs;
    public HealthBar healthBar;

    public Camera mainCamera;
    public GridBoard board;
    public BattleHandler battleHandler;
    public SelectedAnimistUI selectedAnimist;
    public SelectedSpiritUI selectedSpirit;

    public bool matchmakingIsPlayer1;

    public List<List<GamePiece>> gamePieces;
    public List<GamePiece> p1Pieces;
    public List<GamePiece> p2Pieces;
    public Animist p1Animist;
    public Animist p2Animist;

    public bool isTurnOver = false;

    public List<InputHistory> history;
    public InputPusher inputPusher;

    void Awake()
    {
        Singleton = this;
        gamePieces = new();
        p1Pieces = new();
        p2Pieces = new();
        history = new()
        {
            new()
        };
        board.InitBoard();
        //InstantiateStartingPieces(ApiConnection.Singleton.dataTransfer.SelfState, ApiConnection.Singleton.dataTransfer.EnemyState);
        InstantiateStartingPieces(ApiConnection.Singleton.dataTransfer.SelfState,ApiConnection.Singleton.dataTransfer.Response);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GamePiece GetPieceFromCoords((int x, int y) coords)
    {
        if (coords.x != -1 && coords.y != -1)
        {
            return gamePieces[coords.x][coords.y];
        }
        return null;
    }

    public void InstantiateStartingPieces()
    {
        p1Animist = Instantiate(animistPrefabs[0]);
        p1Animist.init(this, true, Random.Range(0, 8), Random.Range(0, 4), 100);

        FindEmptySlot(out int x, out int y, false);
        p2Animist = Instantiate(animistPrefabs[0]);
        p2Animist.init(this, false, x, y, 100);

        for (int i = 0; i < 4; i++)
        {
            FindEmptySlot(out x, out y, true);
            Spirit spirit = Instantiate(spiritPrefabs[0]);
            spirit.init(this, "Pebble", true, x, y, Random.Range(50, 100));

            FindEmptySlot(out x, out y, false);
            spirit = Instantiate(spiritPrefabs[0]);
            spirit.init(this, "Pebble", false, x, y, Random.Range(50, 100));
        }
    }
    /*
    public void InstantiateStartingPieces(BasicGameState selfState, BasicGameState enemyState)
    {
        for (int i = 0; i < selfState.gamePieceId.Count; i++)
        {
            if (selfState.gamePieceId[i] == 0)
            {
                p1Animist = Instantiate(animistPrefabs[0]);
                p1Animist.init(this, true, selfState.posX[i], selfState.posY[i], 100);
            }
            else
            {
                Spirit spirit = Instantiate(spiritPrefabs[0]);
                spirit.init(this, "Pebble", true, selfState.posX[i], selfState.posY[i], Random.Range(50, 100));
            }
        }
        for (int i = 0; i < enemyState.gamePieceId.Count; i++)
        {
            if (enemyState.gamePieceId[i] == 0)
            {
                p2Animist = Instantiate(animistPrefabs[0]);
                p2Animist.init(this, false, boardX - 1 - enemyState.posX[i], boardY - 1 - enemyState.posY[i], 100);
            }
            else
            {
                Spirit spirit = Instantiate(spiritPrefabs[0]);
                spirit.init(this, "Pebble", false, boardX - 1 - enemyState.posX[i], boardY - 1 - enemyState.posY[i], Random.Range(50, 100));
            }
        }
    }*/

    public void InstantiateStartingPieces(BasicGameState selfState, ClientBattleResponse response)
    {
        for (int i = 0; i < selfState.gamePieceId.Count; i++)
        {
            if (selfState.gamePieceId[i] == 0)
            {
                p1Animist = Instantiate(animistPrefabs[0]);
                p1Animist.init(this, true, selfState.posX[i], selfState.posY[i], 100);
            }
            else
            {
                Spirit spirit = Instantiate(spiritPrefabs[0]);
                spirit.init(this, "Pebble", true, selfState.posX[i], selfState.posY[i], Random.Range(50, 100));
            }
        }

        matchmakingIsPlayer1 = response.isPlayer1;
        List<int> ids;
        List<int> posXs;
        List<int> posYs;
        List<int> energies;
        if (matchmakingIsPlayer1)
        {
            ids = response.gamePieceId1;
            posXs = response.posX1;
            posYs = response.posY1;
            energies = response.energy1;
        }
        else
        {
            ids = response.gamePieceId2;
            posXs = response.posX2;
            posYs = response.posY2;
            energies = response.energy2;
        }

        for (int i = 0; i < ids.Count; i++)
        {
            if (ids[i] == 0)
            {
                p2Animist = Instantiate(animistPrefabs[0]);
                p2Animist.init(this, false, boardX - 1 - posXs[i], boardY - 1 - posYs[i], energies[i]);
            }
            else
            {
                Spirit spirit = Instantiate(spiritPrefabs[0]);
                spirit.init(this, "Pebble", false, boardX - 1 - posXs[i], boardY - 1 - posYs[i], energies[i]);
            }
        }
    }

    private void FindEmptySlot(out int x, out int y, bool isPlayer1)
    {
        if (isPlayer1)
        {
            x = Random.Range(0, 8);
            y = Random.Range(0, 4);
            while (gamePieces[x][y] != null)
            {
                x = Random.Range(0, 8);
                y = Random.Range(0, 4);
            }
        }
        else
        {
            x = Random.Range(0, 8);
            y = Random.Range(4, 8);
            while (gamePieces[x][y] != null)
            {
                x = Random.Range(0, 8);
                y = Random.Range(4, 8);
            }
        }
    }

    public void CreateMovementInput((int x, int y) start, (int x, int y) end)
    {
        history[^1].playerInputs.Add(new MovementInput(start.x, start.y, end.x, end.y));
        Debug.Log(history[^1].toJson());
        CreateInput();
    }

    public void CreateInput()
    {
        inputPusher.Push();
        history.Add(new());
    }

    public void SwapPosition(GamePiece x, GamePiece y)
    {
        int tempX = x.x;
        int tempY = x.y;
        x.SetPosition(y.x, y.y);
        y.SetPosition(tempX, tempY);
    }

    public void CommitTurn()
    {
        if (!isTurnOver)
        {
            //isTurnOver = true;
            /*
            p1Animist.hasHarvested = false;
            p2Animist.hasHarvested = false;
            
            battleHandler.StartCombat();*/
            StartCoroutine(ApiConnection.Singleton.requestResult());
        }
    }

    public void StartOfTurn()
    {

    }

    public void Harvest(Spirit spirit)
    {
        Animist animist = spirit.isPlayer1 ? p1Animist : p2Animist;

        if (animist.hasHarvested)
        {
            Debug.Log("Illegal Action");
            return;
        }

        if (spirit.isPlayer1)
        {
            FindEmptySlot(out int x, out int y, true);
            Spirit newSpirit = Instantiate(spiritPrefabs[0]);
            newSpirit.init(this, "Pebble", true, x, y, Random.Range(50, 100));
        }
        else
        {
            FindEmptySlot(out int x, out int y, false);
            Spirit newSpirit = Instantiate(spiritPrefabs[0]);
            newSpirit.init(this, "Pebble", false, x, y, Random.Range(50, 100));
        }
        animist.hasHarvested = true;
    }

    public void HarvestSelected()
    {
        if (selectedSpirit.selectedSpirit == null || selectedSpirit.selectedSpirit.energy <= 0)
        {
            return;
        }

        Harvest(selectedSpirit.selectedSpirit);
    }

    public void TestBattleResult(ClientBattleResponse response)
    {
        Debug.Log(Newtonsoft.Json.JsonConvert.SerializeObject(response));
    }
}