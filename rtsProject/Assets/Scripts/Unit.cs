using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour 
{

    public int health;
    public int attack;
    public int defense;
    public int movement;
    public int attackRange;

    public string unitName = "unit";
    
    public Tile currentTile;

    private Vector3 lastPosition;
    private float interpolationParam = 0.0f;

    /*public Unit(int health, int attack, int defense, int movement, int attackRange)
    {
        this.health = health;
        this.attack = attack;
        this.defense = defense;
        this.movement = movement;
        this.attackRange = attackRange;
    }*/
    
    // Use this for initialization
	public virtual void Start () 
    {
        currentTile = World.Instance().GetTileFromPosition(transform.position);
        World.Instance().WarpUnit(this, GetCoords());
        lastPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (interpolationParam <= 1.0f)
        {
            Vector3 destination = World.Instance().GetPositionFromCoords(GetCoords());
            transform.position = VectorInterpolate(lastPosition, destination, interpolationParam);
            interpolationParam += Time.deltaTime;
        }
	}

    public virtual void ActivateSpecial()
    {

    }

    public Point GetCoords()
    {
        if(currentTile == null)
        {
            return new Point(-1, -1);
        }

        return currentTile.coords;
    }

    public void OnMouseUpAsButton()
    {
        if(Input.GetMouseButtonUp(0))
        {
            World.Instance().Select(GetCoords());
        }
        if(Input.GetMouseButtonUp(1))
        {
            World.Instance().MoveTo(GetCoords());
        }
    }

    public void BeginInterpolatedMove(Point coords)
    {
        lastPosition = transform.position;
        interpolationParam = 0.0f;
    }

    public float NLerp(float from, float to, float t)
    {
        //t = 4 * Mathf.Pow(t - 0.5f, 3f) + 0.5f;
        t = 0.36f * Mathf.Atan(10f * (t - 0.5f)) + 0.5f;
        return from + (to - from) * t;
    }
    
    public Vector3 VectorInterpolate(Vector3 from, Vector3 to, float t)
    {
        return new Vector3(NLerp(from.x, to.x, t),NLerp(from.y, to.y, t),NLerp(from.z, to.z, t));
    }
}
