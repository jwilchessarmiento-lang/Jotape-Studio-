using UnityEngine;

/// <summary>Builds a simple 3D version of the historic plaza exploration scene.</summary>
public class BogotaSceneBuilder : MonoBehaviour
{
    [SerializeField] private bool buildOnStart = true;
    private void Start() { if (buildOnStart) BuildScene(); }

    [ContextMenu("Build Bogotá Scene")]
    public void BuildScene()
    {
        CreateBlock("Plaza Ground", new Vector3(0, -0.2f, 0), new Vector3(28, .4f, 22), SceneAssetCatalog.Stone);
        CreateBlock("Grass Border Left", new Vector3(-13, 0, 0), new Vector3(2, .1f, 22), SceneAssetCatalog.Grass);
        CreateBlock("Grass Border Right", new Vector3(13, 0, 0), new Vector3(2, .1f, 22), SceneAssetCatalog.Grass);
        CreateBuilding("Plaza de Bolívar", new Vector3(-8, 0, 5), new Vector3(5, 4, 2), new Color32(229,185,110,255));
        CreateBuilding("Catedral Primada", new Vector3(0, 0, 7), new Vector3(5, 5, 2), new Color32(211,166,109,255));
        CreateBuilding("Capitolio Nacional", new Vector3(8, 0, 5), new Vector3(5, 4, 2), new Color32(189,140,97,255));
        CreateBuilding("Museo y Memoria", new Vector3(-10, 0, -3), new Vector3(4, 3, 2), new Color32(177,122,86,255));
        CreateBench(new Vector3(-5, 0, -5)); CreateBench(new Vector3(7, 0, -5));
        CreateTree(new Vector3(-12, 0, -7)); CreateTree(new Vector3(12, 0, -7));
        CreateLamp(new Vector3(4, 0, -2)); CreateCart(new Vector3(0, 0, -7));
        CreateBlock("Player Start", new Vector3(0, .65f, -9), new Vector3(.7f, 1.3f, .7f), SceneAssetCatalog.Brick);
    }

    private GameObject CreateBlock(string objectName, Vector3 position, Vector3 scale, Color color)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube); go.name = objectName; go.transform.SetParent(transform); go.transform.position = position; go.transform.localScale = scale; SetColor(go, color); return go;
    }
    private void CreateBuilding(string label, Vector3 p, Vector3 size, Color color)
    {
        CreateBlock(label, p + Vector3.up * size.y * .5f, size, color);
        CreateBlock(label + " Roof", p + Vector3.up * (size.y + .12f), new Vector3(size.x + .35f, .25f, size.z + .25f), new Color32(141, 99, 66, 255));
        for (int i = 0; i < 3; i++) CreateBlock(label + " Window " + (i + 1), p + new Vector3(-size.x * .25f + i * size.x * .25f, size.y * .55f, -size.z * .51f), new Vector3(.35f, .6f, .08f), new Color32(255, 240, 207, 255));
        CreateBlock(label + " Door", p + new Vector3(0, .65f, -size.z * .52f), new Vector3(.55f, 1.3f, .08f), new Color32(99, 66, 47, 255));
    }
    private void CreateBench(Vector3 p) { CreateBlock("Park Bench Seat", p + new Vector3(0,.55f,0), new Vector3(2,.18f,.45f), SceneAssetCatalog.Wood); CreateBlock("Park Bench Back", p + new Vector3(0,.95f,.2f), new Vector3(2,.65f,.15f), SceneAssetCatalog.Wood); CreateBlock("Bench Legs", p + new Vector3(0,.25f,0), new Vector3(1.6f,.5f,.15f), new Color32(106,75,52,255)); }
    private void CreateTree(Vector3 p) { GameObject trunk = GameObject.CreatePrimitive(PrimitiveType.Cylinder); trunk.name="Tree Trunk"; trunk.transform.SetParent(transform); trunk.transform.position=p+Vector3.up; trunk.transform.localScale=new Vector3(.25f,1f,.25f); SetColor(trunk,new Color32(118,81,57,255)); GameObject crown=GameObject.CreatePrimitive(PrimitiveType.Sphere); crown.name="Tree Crown"; crown.transform.SetParent(transform); crown.transform.position=p+Vector3.up*2.5f; crown.transform.localScale=Vector3.one*2.2f; SetColor(crown,SceneAssetCatalog.Grass); }
    private void CreateLamp(Vector3 p) { GameObject pole=GameObject.CreatePrimitive(PrimitiveType.Cylinder); pole.name="Street Lamp"; pole.transform.SetParent(transform); pole.transform.position=p+Vector3.up*1.4f; pole.transform.localScale=new Vector3(.09f,1.4f,.09f); SetColor(pole,new Color32(70,80,80,255)); CreateBlock("Lamp Light",p+Vector3.up*2.9f,new Vector3(.45f,.35f,.45f),SceneAssetCatalog.Gold); }
    private void CreateCart(Vector3 p) { CreateBlock("Market Cart",p+Vector3.up*.55f,new Vector3(1.6f,1.1f,.8f),SceneAssetCatalog.Brick); CreateBlock("Market Canopy",p+Vector3.up*1.25f,new Vector3(2,.18f,1.1f),SceneAssetCatalog.Gold); }
    private void SetColor(GameObject go, Color color) { Renderer r=go.GetComponent<Renderer>(); if(r!=null){r.material.color=color;} }
}
