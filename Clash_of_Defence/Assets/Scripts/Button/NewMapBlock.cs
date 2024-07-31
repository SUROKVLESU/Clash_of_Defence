using UnityEngine;

public class NewMapBlock : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameController.instance.MapController.CancellationSelected();
        Vector3Int NewVector3Int = new Vector3Int((int)transform.position.x, 0, (int)transform.position.z);
        GameController.instance.MapController.DestroyButtonNewMapBlock();
        GameController.instance.MapController.AddMapBlock(new Vector2Int(NewVector3Int.x, NewVector3Int.z));
        GameController.instance.MapController.NewArrayNewPositionMapBlock(NewVector3Int);
        Instantiate(GameController.instance.NewMapBlock).transform.position=NewVector3Int;
    }
}

