using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class CapturePanelController : MonoBehaviour {
	/// <summary> a struct of grid containing row and column </summary>
	struct StructGrid{
		public int col, row;
		public StructGrid(int c, int r){
			this.col = c;
			this.row = r;
		}
	}

	const int MAX_GRID = 5;

	public delegate void PlayerCapturedAlien(int index);
	public event PlayerCapturedAlien OnPlayerCapturedAlien;

	[Header("References")]
	public PanelGetAlien panelGetAlien;
	public Sprite emptySlotSprite;

	[Header("Data")]
	public AlienDisplay[] charDisp;
	public GameObject[] cellObjs;
	public SearchCell[,] PanelsGridCell = new SearchCell[MAX_GRID,MAX_GRID];

	int tempSelectedAlienIndex;
	public List<Alien> alienSelected;
	List<StructGrid> strgrid;

	#region randomize system
	/// <summary> CALL THIS METHOD TO INITIATE RANDOMIZING ALIENS </summary>
	public void InitCaptureAlien()
	{
		alienSelected = new List<Alien>();
		InitGridMatrix();
		SelectRandomAlien();
		RandomizeAlienPositionInGrid();
	}

	///<summary>initialize the matrix of grids. (PanelsGridCell)</summary>
	void InitGridMatrix()
	{
		int i = 0;
		for(int row = 0;row<MAX_GRID;row++){
			for(int col = 0;col<MAX_GRID;col++){
				PanelsGridCell[col,row] = cellObjs[i].GetComponent<SearchCell>();
				PanelsGridCell[col,row].ResetContent();
				PanelsGridCell[col,row].cellContent.sprite = emptySlotSprite;
				i++;
			}
		}
	}

	/// <summary> Select random aliens to be appeared in the grid (limited to 3 aliens). (alienSelected)</summary>
	void SelectRandomAlien()
	{
		List<int> randomIds = new List<int>();
		for(int i = 0; i < PlayerData.Instance.alienData.Length;i++) randomIds.Add(i);

		for(int i = 0; i < 3; i++){
			int tempIndex = Random.Range(0,randomIds.Count);
			alienSelected.Add(PlayerData.Instance.alienData[randomIds[tempIndex]].GetComponent<Alien>());
//			print("ALIEN SELECTED = "+alienSelected[i].gameObject.name);
			randomIds.RemoveAt(tempIndex);
			if(charDisp[i] != null) charDisp[i].AssignCharacter(alienSelected[i]);
		}
	}

	/// <summary> Recursively search for available slot in grid to place alien pattern </summary>
	void RandomizeAlienPositionInGrid()
	{
		for(int i = 0;i<alienSelected.Count;i++){
			tempSelectedAlienIndex = i;
			InitAlienAvailableGridList(alienSelected[i]);
		}
	}

	/// <summary> gather and store all placeable grids for alien </summary>
	void InitAlienAvailableGridList(Alien alienObj)
	{
		int alienGridCol = alienObj.alienSO.rows[0].columns.Length;
		int alienGridRow = alienObj.alienSO.rows.Length;

		int availableCol = MAX_GRID - alienGridCol + 1;
		int availAbleRow = MAX_GRID - alienGridRow + 1;

		strgrid = new List<StructGrid>();

		for(int row = 0;row<availAbleRow;row++){
			for(int col =0;col<availableCol;col++){
				strgrid.Add(new StructGrid(col,row));
			}
		}

		CheckAvailableGrid(alienGridCol,alienGridRow);
	}

	void CheckAvailableGrid(int alienCol, int alienRow)
	{
		if(strgrid.Count == 0){
			//character is removed and not assigned to grid
			return;
		}

		int rnd = Random.Range(0,strgrid.Count);

		if(GridIsEmpty(strgrid[rnd]) == false){
			strgrid.RemoveAt(rnd);
			CheckAvailableGrid(alienCol,alienRow);
		}else{
			AssignCharacterDotsToCells(strgrid[rnd], alienCol, alienRow);
		}
	}

	/// <summary> check all dots inside the selected grid. if all dots are empty, alien can be placed. otherwise, search another random grid</summary>
	bool GridIsEmpty(StructGrid grid){
		for(int r = grid.row; r < grid.row + alienSelected[tempSelectedAlienIndex].alienSO.rows.Length; r++){
			for(int c = grid.col; c < grid.col + alienSelected[tempSelectedAlienIndex].alienSO.rows[0].columns.Length; c++){
				if(PanelsGridCell[c,r].full) return false;
			}
		}
		return true;
	}

	/// <summary>place alien pattern in the grid</summary>
	void AssignCharacterDotsToCells(StructGrid grid, int charCol, int charRow)
	{
		int charR = 0, charC = 0;
		for(int r = grid.row; r < grid.row + alienSelected[tempSelectedAlienIndex].alienSO.rows.Length; r++){
			for(int c = grid.col; c < grid.col + alienSelected[tempSelectedAlienIndex].alienSO.rows[0].columns.Length; c++){
				if(charR >= charRow) return;
				else{
					if(charC >= charCol){ charR++; charC = 0; }
						
					if(alienSelected[tempSelectedAlienIndex].alienSO.rows[charR].columns[charC] == Grid.Y)
						PanelsGridCell[c,r].AssignCellContent(
							alienSelected[tempSelectedAlienIndex].alienSO.spriteIcon,
							tempSelectedAlienIndex
						);
					
					charC++;
				}
			}
		}
	}
	#endregion

	public void SetPlayerCapturedAlien(int index)
	{
		
		if(OnPlayerCapturedAlien != null){
			OnPlayerCapturedAlien(alienSelected[index].alienSO.alienID-1);
		}
	}
}