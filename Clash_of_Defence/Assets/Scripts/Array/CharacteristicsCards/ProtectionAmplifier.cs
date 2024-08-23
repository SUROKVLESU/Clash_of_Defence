using UnityEngine;

public class ProtectionAmplifier:BaseCharacteristics
{
    [SerializeField] protected Attributes ProtectionAdd;
    public Attributes GetProtectionAdd{get{return ProtectionAdd;}}
    [SerializeField] protected Transform[] TransformForder;
    public Transform GetTransformForder(bool left)
    {
        if (left)
        return TransformForder[0];
        else return TransformForder[1];
    }
}

