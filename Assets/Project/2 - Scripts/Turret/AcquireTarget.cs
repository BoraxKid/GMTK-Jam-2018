using UnityEngine;

public class AcquireTarget : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private LayerMask _layersLineCast;

    public Collider2D GetTarget(Transform from, TurretSettings settings)
    {
        Collider2D[] results;

        results = Physics2D.OverlapCircleAll(from.position, settings.Range, this._targetLayer.value);

        if (results.Length <= 0)
            return (null);

        Collider2D best = null;
        float bestDistance = float.MaxValue;
        RaycastHit2D[] hits;

        foreach (Collider2D collider in results)
        {
            Collider2D tmpBest = null;
            float tmpBestDistance = bestDistance;
            hits = Physics2D.LinecastAll(from.position, collider.transform.position, this._layersLineCast);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform == from) // We shouldn't target ourself!
                    continue;
                if (hit.distance < tmpBestDistance)
                {
                    tmpBest = hit.collider;
                    tmpBestDistance = hit.distance;
                }
            }
            if (tmpBest != null && this._targetLayer.value == (this._targetLayer.value | (1 << tmpBest.gameObject.layer)))
            {
                best = tmpBest;
                bestDistance = tmpBestDistance;
            }
        }
        return (best);
    }

    public void SetTargetLayer(int targetLayer)
    {
        this._targetLayer = 1 << targetLayer;
    }
}
