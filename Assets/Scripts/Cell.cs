using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector3 position;
    //public CellTag zone;
    //public CellSideTag side;

    /*
    public Cell(Vector3 position, CellTag zone, CellSideTag side)
    {
        this.position = position;
        this.zone = zone;
        this.side = side;
    }

    public override string ToString()
    {
        return position + " " + zone + " " + side;
    }

     * 
     * public class DecorationAsset : ScriptableObject 
     * {
     *  public GameObject prefab;
     *  public Vector 2 area;
     *  public CellTag zone;
     *  
     *  [Range(0,1)]
     *  public float chances;
     * }
     * 
     * var cell = cells.availPos[id];
     * var zoneC = zoneChances(cell.zone);
     * var rand = Random.Range(0,1f);
     * 
     * if(rand<= zoneC)
     * {
     *  var possibleElements = decorationAssets.Where(x => x.zone).ToList();
     *      if(possibleElements.Count > 0)
     *      {
     *          var decoration = pickOneAsset(possibleElements);
     *          var pos = cell.position;
     *          var rot = getRotation(cell.side);
     *          
     *          if(isInsideRoom(pos,decoration.area) && !isOverlap(pos, decoration.area))
     *          {
     *              cells = removeArea(cells, pos, decoration.area);
     *              props.Add(new Decoration(pos, rot, decoration));
     *          }
     *      }
     * }
     * 
     */

}
