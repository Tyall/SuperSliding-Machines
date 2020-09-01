using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombiner : MonoBehaviour
{

    public GameObject[] treesArray;
    public GameObject combinedObj;

    // Start is called before the first frame update
    void Start()
    {
        CombineTrees();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CombineTrees()
    {
        List<CombineInstance> woodList = new List<CombineInstance>();
        List<CombineInstance> leafList = new List<CombineInstance>();

        for (int i=0; i>treesArray.Length; i++)
        {
            GameObject currentTree = treesArray[i];
            currentTree.SetActive(false);

            MeshFilter[] meshFilters = currentTree.GetComponentsInChildren<MeshFilter>(true);

            for (int j = 0; j<meshFilters.Length; j++)
            {
                MeshFilter meshFilter = meshFilters[j];
                CombineInstance combine = new CombineInstance();

                MeshRenderer meshRender = meshFilter.GetComponent<MeshRenderer>();

                string materialName = meshRender.material.name.Replace(" (Instance)", "");
                if (materialName == "Leaves")
                {
                    combine.mesh = meshFilter.mesh;
                    combine.transform = meshFilter.transform.localToWorldMatrix;

                    leafList.Add(combine);
                }
                else if (materialName == "Tree")
                {
                    combine.mesh = meshFilter.mesh;
                    combine.transform = meshFilter.transform.localToWorldMatrix;

                    woodList.Add(combine);
                }
            }
        }
        Mesh combinedWoodMesh = new Mesh();
        combinedWoodMesh.CombineMeshes(woodList.ToArray());

        Mesh combinedLeafMesh = new Mesh();
        combinedLeafMesh.CombineMeshes(leafList.ToArray());

        CombineInstance[] totalMesh = new CombineInstance[2];

        totalMesh[0].mesh = combinedLeafMesh;
        totalMesh[0].transform = combinedObj.transform.localToWorldMatrix;

        totalMesh[1].mesh = combinedWoodMesh;
        totalMesh[1].transform = combinedObj.transform.localToWorldMatrix;

        Mesh combinedAllMesh = new Mesh();

        combinedAllMesh.CombineMeshes(totalMesh, false);
        combinedObj.GetComponent<MeshFilter>().mesh = combinedAllMesh;

    }
}
