using Assets.Scripts.Dialogs;
using Assets.Scripts.Dice;
using Assets.Scripts.Farmables;
using Assets.Scripts.Scriptables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Tilemap _groundTileMap;
    public Tilemap _plantTileMap;
    public Tilemap _selectorTileMap;
    public Tilemap _diceTileMap;
    public TileBase _selectorTile;
    public TileBase _groundTile;
    public TileBase _tilledTile;
    public TileBase _emptyDiceTile;
    public TileBase _growthTile;
    public TileBase[] _d6Dice;

    //public TileBase[] _carrots;
    [SerializeField]
    private DiceTile _diceTilePrefab;
    public List<DiceTile> DiceCollection;

    [SerializeField]
    private TextMeshProUGUI AttackDisplay;
    [SerializeField]
    private TextMeshProUGUI DefenseDisplay;
    //[SerializeField]
    //public int TotalDiceLeft;
    //TEMP
    private bool GameOver { get; set; } = false;

    [SerializeField]
    private TextMeshProUGUI HeroHPDisplay;
    [SerializeField]
    private Image HeroFace;
    [SerializeField]
    private Sprite[] HeroFaces;
    private bool UpdateFace = false;
    [SerializeField]
    private int HeroHP = 0;
    //public int TotalAttack = 0;
    //public int TotalDefense = 0;


    //public DialogManager DialogManager;
    //public int HeroCurrentLevel;
    //public TextMeshProUGUI PlayerLevel;

    //[SerializeField]
    //private List<DialogQuestScriptableObject> dialogQuestScriptableObjects;

    [SerializeField]
    private List<Vector3Int> SpawnPoints;

    public Quest CurrentQuest;

    [SerializeField]
    private Button AttackButton;
    [SerializeField]
    public Button DefenseButton;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        this.DiceCollection = new List<DiceTile>();

        //this._groundTile.jas

        //this.HeroCurrentLevel = 5;
    }

    void Start()
    {

       // this.test(); 
    }

    Vector3Int CurrentPosition
    {
        get
        {
            return this.GetPositionInGrid();
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Vector3Int gridPos = this.GetPositionInGrid();
        this.RollDice();
        this.MouseMovement();
        this.MouseInput();
        this.UpdateDiceGrowth();
        this.UpdateDefenseDisplay();
        this.UpdateAttackDisplay();
        this.UpdateHeroDisplay();
        if (this.HeroHP <= 0)
        {
            Debug.Log("Game Over");
        }
        //if (this.GameOver)
        //{
        //    Debug.Log("Game Over");
        //}
        //this.UpdateHeroUI();
    }

    private void RollDice()
    {
        foreach(Vector3Int vector3Int in this.SpawnPoints)
        {
            if (!this._diceTileMap.HasTile(vector3Int))
            {
                DiceTile diceTile = Instantiate(this._diceTilePrefab, this._diceTileMap.transform);
                DiceData data = this.GetRandomDiceData(vector3Int);
                diceTile.SetTileDetails(data);
                this.DiceCollection.Add(diceTile);
                this._diceTileMap.SetTile(vector3Int, this._emptyDiceTile);
                this.UpdateFace = true;
                StartCoroutine(diceTile.GrowDice());
            }
        }
    }

    private void UpdateDiceGrowth()
    {
        List<DiceTile> tmp = new List<DiceTile>();
        foreach(DiceTile tile in this.DiceCollection)
        {
            if (tile.DiceData.Age <= 1 && tile.DiceData.Age < tile.DiceData.MaxLifeSpan)
            {
                this._diceTileMap.SetTile(tile.DiceData.Position, this._growthTile);
            }
            else if (tile.DiceData.Age == 2 && tile.DiceData.Age < tile.DiceData.MaxLifeSpan)
            {
                this._diceTileMap.SetTile(tile.DiceData.Position, tile.DiceData.Tile);
            }
            else if (tile.DiceData.Age >= tile.DiceData.MaxLifeSpan)
            {
                tmp.Add(tile);
                this._diceTileMap.SetTile(tile.DiceData.Position, null);
            }
        }
        if (tmp.Count > 0)
        {
            this.DiceCollection.RemoveAll(x=>tmp.Contains(x));
            tmp.ForEach(x=>Destroy(x.gameObject));
        }
    }

    public void ClearAllSelectedDice()
    {
        List<DiceTile> tmp = new List<DiceTile>();
        foreach (DiceTile tile in this.DiceCollection)
        {
            if (tile.isSelected)
            {
                tmp.Add(tile);
                this._diceTileMap.SetTile(tile.DiceData.Position, null);
            }
        }
        if (tmp.Count > 0)
        {
            this.DiceCollection.RemoveAll(x => tmp.Contains(x));
            tmp.ForEach(x => Destroy(x.gameObject));
        }
    }

    void MouseMovement()
    {
        if (this._groundTileMap.HasTile(this.CurrentPosition))
        {
            this._selectorTileMap.ClearAllTiles();
            this._selectorTileMap.SetTile(this.CurrentPosition, this._selectorTile);
            //Debug.Log("Tile at Grid");
        }
        else
        {
            this._selectorTileMap.ClearAllTiles();
            //Debug.Log("No Tile at Grid");
        }
    }

    void MouseInput()
    {
       // Debug.Log($"Pos: {this.CurrentPosition.x} - {this.CurrentPosition.y}");
        if (Input.GetMouseButtonDown(0))
        {
            if (this.HeroHP >= 1)
            {
                if (this._groundTileMap.HasTile(this.CurrentPosition))
                {
                    DiceTile tile = this.DiceCollection.Where(x => x.DiceData.Position == this.CurrentPosition).FirstOrDefault();
                    if (tile != null)
                    {
                        if ((tile.DiceData.Age >= 2))
                        {
                            Debug.Log($"Clicked: {tile.DiceData.Value}");
                            tile.isSelected = !tile.isSelected;
                            this._diceTileMap.RemoveTileFlags(this.CurrentPosition, TileFlags.LockColor);
                            var a = this._diceTileMap.GetTileFlags(this.CurrentPosition);
                            if (tile.isSelected)
                            {
                                tile.DiceData.Mode = Mode.ATK;
                                _diceTileMap.SetColor(tile.DiceData.Position, Color.red);
                            }
                            else
                            {
                                tile.DiceData.Mode = Mode.NONE;
                                _diceTileMap.SetColor(tile.DiceData.Position, Color.white);
                            }

                        }
                    }

                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (this.HeroHP >= 1)
            {
                if (_groundTileMap.HasTile(this.CurrentPosition))
                {
                    DiceTile tile = this.DiceCollection.Where(x => x.DiceData.Position == this.CurrentPosition).FirstOrDefault();
                    if (tile != null)
                    {
                        if ((tile.DiceData.Age >= 2))
                        {
                            Debug.Log($"Clicked: {tile.DiceData.Value}");
                            tile.isSelected = !tile.isSelected;
                            this._diceTileMap.RemoveTileFlags(this.CurrentPosition, TileFlags.LockColor);
                            var a = this._diceTileMap.GetTileFlags(this.CurrentPosition);
                            if (tile.isSelected)
                            {
                                tile.DiceData.Mode = Mode.DEF;
                                _diceTileMap.SetColor(tile.DiceData.Position, Color.cyan);
                            }
                            else
                            {
                                tile.DiceData.Mode = Mode.NONE;
                                _diceTileMap.SetColor(tile.DiceData.Position, Color.white);
                            }
                        }
                    }
                }
            }
        }
    }

    Vector3Int GetPositionInGrid()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return this._groundTileMap.WorldToCell(mousePos);
    }


    DiceData GetRandomDiceData(Vector3Int pos)
    {
        DiceData diceData = new DiceData();
        diceData.Value = Random.Range(1, 6);
        diceData.Age = 0;
        diceData.MaxLifeSpan = Random.Range(4, 8);
        diceData.Name = $"D6 - {diceData.Value}";
        diceData.Tile = this._d6Dice[diceData.Value - 1];
        diceData.Position = pos;
        return diceData;
    }

    private void UpdateDefenseDisplay()
    {
        this.DefenseDisplay.text = $"{TotalDefense.ToString()}";
    }

    private void UpdateAttackDisplay()
    {
        this.AttackDisplay.text = $"{TotalAttack.ToString()}";
    }

    private void UpdateHeroDisplay()
    {
        if (this.HeroHP <= 0)
        {
            this.HeroFace.sprite = this.HeroFaces[this.HeroFaces.Length-1];
            this.UpdateFace = false;
        }
        else if (this.HeroHP % 2 == 0 && this.UpdateFace)
        {
            this.HeroFace.sprite = this.HeroFaces[Random.Range(0, this.HeroFaces.Length-1)];
            this.UpdateFace = false;
        }
        this.HeroHPDisplay.text = $"HP: {HeroHP.ToString()}";
    }
    public int TotalValue
    {
        get
        {
            return DiceCollection.Where(y => y.isSelected).Select(x => x.DiceData.Value).Sum();
        }
    }

    public int TotalDefense
    {
        get
        {
            return DiceCollection.Where(y => y.isSelected & y.DiceData.Mode == Mode.DEF).Select(x => x.DiceData.Value).Sum();
        }
    }

    public int TotalAttack
    {
        get
        {
            return DiceCollection.Where(y => y.isSelected & y.DiceData.Mode == Mode.ATK).Select(x => x.DiceData.Value).Sum();
        }
    }

    public void AttackAgainstQuest()
    {
        if (this.CurrentQuest == null) { Debug.Log("No Quest"); return; }
        this.CurrentQuest.CompleteQuest();
    }
    public void DefendAgainstQuest()
    {
        if (this.CurrentQuest == null) { Debug.Log("No Quest"); return; }
        this.CurrentQuest.CompleteQuest();
    }

}
public enum Mode
{
    ATK,
    DEF,
    NONE
}
public class DiceData
{
    public string Name { get; set; }
    public int Value { get; set; }
    public TileBase Tile { get; set; }
    public Vector3Int Position { get; set; }
    public int Age { get; set; }
    public int MaxLifeSpan { get; set; }
    public Mode Mode { get; set; } = Mode.NONE;

}
