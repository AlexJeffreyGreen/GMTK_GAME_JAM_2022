using Assets.Scripts.Dialogs;
using Assets.Scripts.Farmables;
using Assets.Scripts.Scriptables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Tilemap _groundTileMap;
    public Tilemap _plantTileMap;
    public Tilemap _selectorTileMap;
    public TileBase _selectorTile;
    public TileBase _groundTile;
    public TileBase _tilledTile;
    public TileBase _grassTile;

    public TileBase[] _carrots;
    public List<FarmableObject> FarmableObjCollection;

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

        this.FarmableObjCollection = new List<FarmableObject>();

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
        foreach(FarmableObject obj in this.FarmableObjCollection)
        {
            this._plantTileMap.SetTile(obj.Position, this._carrots[obj.CurrentLife]);
            Debug.Log($"Farmable: {obj.Name} - {obj.Position} - Life: {obj.CurrentLife}");

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
        if (Input.GetMouseButtonDown(0))
        {
            if (this._groundTileMap.HasTile(this.CurrentPosition))
            {
                if (!this.FarmableObjCollection.Select(x=>x.Position).Contains(this.CurrentPosition))
                {
                    FarmableObject farmableObject = new FarmableObject("Carrot", this.CurrentPosition, 3);
                    //this.FarmableObjCollection.Add(Instantiate())
                    this.FarmableObjCollection.Add(farmableObject);

                    StartCoroutine(farmableObject.Growth());

                    
                }
                this._plantTileMap.SetTile(this.CurrentPosition, _tilledTile);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (this._groundTileMap.HasTile(this.CurrentPosition))
            {
                this._plantTileMap.SetTile(this.CurrentPosition, _grassTile);
            }
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

}
