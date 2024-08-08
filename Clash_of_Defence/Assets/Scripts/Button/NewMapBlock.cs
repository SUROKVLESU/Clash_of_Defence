using UnityEngine;

public class NewMapBlock : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameController.instance.ButtonController.OnStartWaveButton();
        GameController.instance.MapController.CancellationSelected();
        Vector3Int NewVector3Int = new Vector3Int((int)transform.position.x, 0, (int)transform.position.z);
        GameController.instance.MapController.DestroyButtonNewMapBlock();
        GameController.instance.MapController.AddMapBlock(new Vector2Int(NewVector3Int.x, NewVector3Int.z));
        GameController.instance.MapController.NewArrayNewPositionMapBlock(NewVector3Int);
        GameObject mapBlock = Instantiate(GameController.instance.NewMapBlock);
        mapBlock.transform.position = NewVector3Int;
        GameController.instance.MapController.ArrayMapBlockGameObjects.Add(mapBlock);
    }
}

