using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingBlock : MonoBehaviour
{
    [SerializeField]
    private GameObject breakingMesh;

    private readonly List<GameObject> allBreakablePieces = new();

    private void Start()
    {
        int count = breakingMesh.transform.childCount;
        
        for (int i = 0; i < count; i++)
        {
            GameObject breakablePiece = breakingMesh.transform.GetChild(i).gameObject;
            allBreakablePieces.Add(breakablePiece);
        }

        StartCoroutine(Delay(count));
    }

    
    private void Update()
    {
        
    }

    private IEnumerator Delay(int count)
    {
        yield return new WaitForSeconds(5.0f);

        for (int i = 0; i < count; i++)
        {
            Destroy(allBreakablePieces[i]);
        }

        Destroy(this);
    }
}
