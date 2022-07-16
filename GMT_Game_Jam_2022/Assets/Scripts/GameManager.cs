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
    public TileBase _grassTile;
    public TileBase[] _d6Dice;

    //public TileBase[] _carrots;
    [SerializeField]
    private DiceTile _diceTilePrefab;
    public List<DiceTile> DiceCollection;

    //public DialogManager DialogManager;
    //public int HeroCurrentLevel;
    //public TextMeshProUGUI PlayerLevel;
    
    //[SerializeField]
    //private List<DialogQuestScriptableObject> dialogQuestScriptableObjects;

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

    //void test()
    //{ 
    //    for (int i = 0; i < 10; i++)
    //    {
    //        DialogBase dialogB;
    //        DialogQuestScriptableObject dialogQuestScriptableObject = this.GetRandomDialog();
    //        if (i % 2 == 0)
    //        {
    //            dialogB = DialogFactory.Create<QuestDialog>(dialogQuestScriptableObject);
    //        }
    //        else
    //        {
    //            dialogB = DialogFactory.Create<ConversationDialog>(dialogQuestScriptableObject);
    //        }

    //        DialogManager.EnqueueDialog(dialogB);
    //    }
    //}
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

        this.MouseMovement();
        this.MouseInput();
        this.UpdateFarmableTiles();
        //this.UpdateHeroUI();
    }

    private void UpdateFarmableTiles()
    {
        //foreach(FarmableObject obj in this.FarmableObjCollection)
        //{
        //    //this._plantTileMap.SetTile(obj.Position, this._carrots[obj.CurrentLife]);
        //    //this._diceTileMap.SetTile(this._d6Dice[0])
        //    Debug.Log($"Farmable: {obj.Name} - {obj.Position} - Life: {obj.CurrentLife}");

        //}
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
        if (Input.GetMouseButtonDown(0))
        {
            if (this._groundTileMap.HasTile(this.CurrentPosition))
            {
                if (!this.DiceCollection.Select(x=>x.Position).Contains(this.CurrentPosition))
                {
                    DiceTile diceTile = Instantiate(this._diceTilePrefab, this._diceTileMap.transform);
                    DiceData data = this.GetRandomDiceData("d6");
                    diceTile.SetTileDetails(this.CurrentPosition, data.Name, data.Value);
                    this._diceTileMap.SetTile(this.CurrentPosition, data.Tile);
                }
                //this._plantTileMap.SetTile(this.CurrentPosition, _tilledTile);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
           // if (this._groundTileMap.HasTile(this.CurrentPosition))
           // {
           //     this._plantTileMap.SetTile(this.CurrentPosition, _grassTile);
           // }
        }
    }

    Vector3Int GetPositionInGrid()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return this._groundTileMap.WorldToCell(mousePos);
    }

    //void inputTest()
    //{
    //    DialogBase b = this.DialogManager.DequeueDialog();
    //    if (b != null)
    //    {
    //        DialogManager.GetComponentInChildren<TextMeshProUGUI>().text = b.Message;
    //    }
    //    else
    //    {
    //        DialogManager.GetComponentInChildren<TextMeshProUGUI>().text = "";
    //    }
    //}

    ////TODO: Take into consideration levels and such
    ///// <summary>
    ///// Get Random Dialog from Scriptables
    ///// </summary>
    ///// <returns></returns>
    //public DialogQuestScriptableObject GetRandomDialog()
    //{
    //    return dialogQuestScriptableObjects[Random.Range(0, dialogQuestScriptableObjects.Count - 1)];
    //}

    //private void UpdateHeroUI()
    //{
    //    this.PlayerLevel.text = "Level: " + HeroCurrentLevel.ToString();
    //}

    DiceData GetRandomDiceData(string name)
    {
        DiceData diceData = new DiceData();
        switch (name)
        {
            case "d6":
                diceData.Value = Random.Range(1, 6);
                diceData.Name = $"D6 - {diceData.Value}";
                diceData.Tile = this._d6Dice.First();
                break;
        }
        return diceData;
    }

}

public class DiceData
{
    public string Name { get; set; }
    public int Value { get; set; }
    public TileBase Tile { get; set; }
}
