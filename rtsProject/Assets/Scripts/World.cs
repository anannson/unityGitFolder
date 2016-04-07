using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Point
{
    public int x;
    public int y;

    public static Point zero = new Point(0, 0);

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static Point operator +(Point A, Point B)
    {
        return new Point(A.x + B.x, A.y + B.y);
    }

    public static Point operator -(Point A, Point B)
    {
        return new Point(A.x - B.x, A.y - B.y);
    }

    public override string ToString()
    {
        return "(" + x.ToString() + ", " + y.ToString() + ")";
    }
}

public class World : MonoBehaviour 
{

    public GameObject[] tiles;

    public Tile selectedTile = null;

    public Point mapSize = new Point(50,50);

    public UIController uiController;

    private Tile[,] map;

    private GameObject lastMouseHit= null;

    private static World instance = null;

    public static World Instance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
        GenerateMap();
    }
    
    // Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                lastMouseHit = hit.transform.gameObject;
                if(lastMouseHit != null)
                {
                    lastMouseHit.SendMessage("OnMouseDown", null, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject previousHit = lastMouseHit;
                lastMouseHit = hit.transform.gameObject;
                if (lastMouseHit != null)
                {
                    if(lastMouseHit == previousHit)
                    {
                        lastMouseHit.SendMessage("OnMouseUpAsButton", null, SendMessageOptions.DontRequireReceiver);
                    }
                    else
                    {
                        lastMouseHit.SendMessage("OnMouseUp", null, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }
	}

    public void GenerateMap()
    {
        map = new Tile[mapSize.x, mapSize.y];
        for (int x = 0; x <= mapSize.x - 1; x++ )
        {
            for (int y = 0; y <= mapSize.y - 1; y++)
            {
                GameObject g = (GameObject)Instantiate(tiles[Random.Range(0,tiles.Length)], new Vector3(x, 0, y), Quaternion.identity);
                Tile t = g.GetComponent<Tile>();
                t.coords = new Point(x, y);
                map[x, y] = t;
            }
        }
    }

    public Point GetCoordsFromPosition(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.z);
        print(x + ", " + y);
        return new Point(x,y);
    }

    public Vector3 GetPositionFromCoords(Point coords)
    {
        float x = coords.x;
        float z = coords.y;
        return new Vector3(x, 0.7f, z);
    }

    public Tile GetTileFromPosition(Vector3 position)
    {
        return GetTileFromCoords(GetCoordsFromPosition(position));
    }

    public Tile GetTileFromCoords(Point coords)
    {
        if (coords.x < 0 || coords.x >= mapSize.x || coords.y < 0 || coords.y >= mapSize.y)
        {
            return null;
        }
        return map[coords.x, coords.y];
    }

    public void Select(Point coords) 
    {
		Tile lastSelection = selectedTile;

		if (selectedTile != null) 
        {
			selectedTile.ResetColor ();
			selectedTile = null;

            UpdateUI();
		}

		if (coords.x < 0 || coords.x >= mapSize.x || coords.y < 0 || coords.y >= mapSize.y)
        {
            return;
        }
		
		if (lastSelection == map [coords.x, coords.y])
        {
            return;
        }
			
		selectedTile = map[coords.x, coords.y];
        print(selectedTile.coords);
		selectedTile.SetColor (Color.yellow);

        UpdateUI();
	}

	public bool IsSelected(Tile tile) 
    {
		return (tile == selectedTile);
	}

    public void ActivateSpecial()
    {
        if(selectedTile == null || selectedTile.occupant == null)
        {
            return;
        }
        selectedTile.occupant.ActivateSpecial();
    }

    public void UpdateUI()
    {
        if(selectedTile != null)
        {
            uiController.SetUnitInfo(selectedTile.occupant);
        }
    }

    public void WarpUnit(Unit unit, Point coords)
    {
        Tile t = GetTileFromCoords(coords);
        if(t == null || t.occupant != null)
        {
            return;
        }
        Tile oldTile = unit.currentTile;
        if(oldTile != null)
        {
            oldTile.occupant = null;
        }

        unit.currentTile = t;
        t.occupant = unit;
        unit.transform.position = GetPositionFromCoords(coords);
    }

    public void MoveUnit(Unit unit, Point coords)
    {
        Tile t = GetTileFromCoords(coords);
        if (t == null || t.occupant != null)
        {
            return;
        }
        Tile oldTile = unit.currentTile;
        if (oldTile != null)
        {
            oldTile.occupant = null;
        }

        unit.currentTile = t;
        t.occupant = unit;
    }

    public void MoveTo(Point coords)
    {
        if(selectedTile == null || selectedTile.occupant == null)
        {
            return;
        }

        selectedTile.occupant.BeginInterpolatedMove(coords);
        MoveUnit(selectedTile.occupant, coords);
        Select(coords);
    }

}
