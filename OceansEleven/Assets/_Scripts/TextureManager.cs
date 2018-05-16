using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureManager : MonoBehaviour
{
    [SerializeField] private Material _p1Material;
    [SerializeField] private Material _p2Material;
    [SerializeField] private Material _p3Material;
    [SerializeField] private Material _p4Material;

    public Material P1Material { get { return _p1Material; } }
    public Material P23Material { get { return _p2Material; } }
    public Material P31Material { get { return _p3Material; } }
    public Material P4Material { get { return _p4Material; } }

    private void Awake()
    {
        SingletonManager.RegisterSingleton(this);
    }

    public Material GetMaterialFromPlayerNumber(int playerNumber)
    {
        Material material = _p1Material;
        switch (playerNumber)
        {
            case 2:
                material = _p2Material;
                break;
            case 3:
                material = _p3Material;
                break;
            case 4:
                material = _p4Material;
                break;
        }
        return material;
    }
}
