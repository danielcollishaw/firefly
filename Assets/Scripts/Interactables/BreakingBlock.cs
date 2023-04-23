using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingBlock : MonoBehaviour
{
    [SerializeField]
    private GameObject regularMesh;
    [SerializeField]
    private GameObject breakingMesh;

    private const float DELAY = 5.0f;

    private readonly List<GameObject> allBreakablePieces = new();

    private void Start()
    {
        int count = breakingMesh.transform.childCount;
        
        for (int i = 0; i < count; i++)
        {
            GameObject breakablePiece = breakingMesh.transform.GetChild(i).gameObject;
            allBreakablePieces.Add(breakablePiece);
        }

        ToggleBreakable(false);
        BreakApart();
    }
    private void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Animator>().GetBool("IsRolling"))
        {
            //BreakApart();
        }
    }
    private void ToggleBreakable(bool activated)
    {
        if (regularMesh.TryGetComponent<MeshRenderer>(out var meshRenderer))
        {
            meshRenderer.enabled = !activated;
        }
            
        if (regularMesh.TryGetComponent<BoxCollider>(out var boxCollider))
        {
            boxCollider.enabled = !activated;
        }

        for (int i = 0; i < allBreakablePieces.Count; i++)
        {
            GameObject breakablePiece = allBreakablePieces[i];

            if (breakablePiece.TryGetComponent<MeshRenderer>(out var breakableMeshRenderer))
            {
                breakableMeshRenderer.enabled = activated;
            }

            if (breakablePiece.TryGetComponent<MeshCollider>(out var meshCollider))
            {
                meshCollider.enabled = activated;
            }
        }
    }
    private void BreakApart()
    {
        ToggleBreakable(true);
        //StartCoroutine(DestroyDelay());
    }
    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(DELAY);

        for (int i = 0; i < allBreakablePieces.Count; i++)
        {
            Destroy(allBreakablePieces[i]);
        }

        Destroy(this);
    }
}
