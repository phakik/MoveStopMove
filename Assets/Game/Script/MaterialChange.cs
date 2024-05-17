using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChange : MonoBehaviour
{
    [SerializeField] Material OriginMaterial;
    [SerializeField] Material blurMaterial;

    public void GetBlur()
    {
        gameObject.GetComponent<MeshRenderer>().material = blurMaterial;
    }
    public void GetNormal()
    {
        gameObject.GetComponent<MeshRenderer>().material = OriginMaterial;
    }
}
