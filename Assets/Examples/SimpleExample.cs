using System.Collections;
using System.Collections.Generic;
using Conway;
using Johnson;
using UnityEngine;


[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SimpleExample : MonoBehaviour
{

    public int sides;

    private ConwayPoly initialPoly;

    public float TopAmount { get; set; }
    public float SidesAmount { get; set; }

    void Start()
    {
        Generate();
    }

    private void OnValidate()
    {
        Generate();
    }

    public void Generate()
    {
        initialPoly = JohnsonPoly.Prism(sides);
        var mesh = PolyMeshBuilder.BuildMeshFromConwayPoly(initialPoly, false);
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void Update()
    {

        var poly = initialPoly.Loft(new OpParams{valueA = 0.5f, valueB = TopAmount, facesel = FaceSelections.FacingUp});
        poly = poly.Kis(new OpParams{valueA = SidesAmount, facesel = FaceSelections.Ignored});

        var mesh = PolyMeshBuilder.BuildMeshFromConwayPoly(poly, false);
        GetComponent<MeshFilter>().mesh = mesh;
        transform.rotation = Quaternion.Euler(0, Time.time * 20f, 0);
    }
}
