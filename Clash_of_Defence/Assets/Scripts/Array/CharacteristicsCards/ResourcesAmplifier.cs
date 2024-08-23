using UnityEngine;

public class ResourcesAmplifier:BaseCharacteristics
{
    [SerializeField] protected Resources ResourcesAdd;
    public Resources GetResourcesAdd { get { return ResourcesAdd; } }
    [SerializeField] protected Transform[] TransformForder;
    public Transform GetTransformForder(bool left)
    {
        if (left)
            return TransformForder[0];
        else return TransformForder[1];
    }
}

