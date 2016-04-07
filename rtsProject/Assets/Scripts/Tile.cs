using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour 
{
    public Point coords = Point.zero;

    public Unit occupant = null;

    public int moveCost = 1;

    public bool passable = false;

    private Renderer rend;
    private Color originalColor;
    
    // Use this for initialization
	void Start () 
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void SetMaterial(Material mat)
    {
        if(rend == null)
        {
            rend = GetComponent<Renderer>();
        }
        rend.material = mat;
    }

    public void SetColor(Color color)
    {
        rend.material.color = color;
    }

    public void ResetColor()
    {
        rend.material.color = originalColor;
    }

    void OnMouseUpAsButton()
    {
        if (Input.GetMouseButtonUp(0))
        {
            World.Instance().Select(coords);
        }
        
        if (Input.GetMouseButtonUp(1))
        {
            World.Instance().MoveTo(coords);
        }
    }

    void OnMouseEnter()
    {
        if(!World.Instance().IsSelected(this))
        {
            SetColor(Color.blue);
        }
    }

    void OnMouseExit()
    {
        if(!World.Instance().IsSelected(this))
        {
            ResetColor();
        }
    }
}
