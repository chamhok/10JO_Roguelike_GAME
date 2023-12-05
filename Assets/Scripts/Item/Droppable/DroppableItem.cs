using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DroppableItem : MonoBehaviour
{
    [Header("Droppable Settings")]
    [SerializeField] protected float _scale = 1f;
    [SerializeField] protected GameObject vfxObject;
    [SerializeField] protected float detectRange = 1f;
    [SerializeField] protected float randomDropRange = 1f;
    [SerializeField] protected float dropCurveProgressTime = 0.5f;
    [SerializeField] protected float lootingProgressTime = 1f;
    [SerializeField] protected Vector2 dropBezierPoint = Vector2.up;

    protected bool isDetecting = false;
    protected Vector2 dropStartPoint;
    protected Vector2 randomDropPoint;
    protected Transform target;

    public float Scale
    {
        get => _scale;
        set { _scale = value; SetScale(); }
    }

    protected virtual void OnEnable()
    {
        SetScale();
        target = GameManager.Instance?.player?.transform;
        StartCoroutine(DroppingProgress());
        GameManager.Instance.items.Add(this);
    }

    protected virtual void OnDisable()
    {
        GameManager.Instance.items.Remove(this);
    }

    public virtual void SetScale() => transform.localScale = Vector3.one * _scale;

    protected virtual void Update()
    {
        if (isDetecting && target)
        {
            if (Vector2.Distance(target.position, transform.position) < detectRange)
            {
                StartCoroutine(LootingProgress());
            }
        }
    }

    protected virtual IEnumerator DroppingProgress()
    {
        isDetecting = false;
        dropStartPoint = transform.position;
        randomDropPoint = Random.insideUnitCircle * randomDropRange + dropStartPoint;

        for (float t = 0f; t < dropCurveProgressTime; t += Time.deltaTime)
        {
            float progress = t / dropCurveProgressTime;
            Vector2 bezierPoint1 = Vector2.Lerp(dropStartPoint, dropBezierPoint + dropStartPoint, progress);
            Vector2 bezierPoint2 = Vector2.Lerp(dropBezierPoint + dropStartPoint, randomDropPoint, progress);
            transform.position = Vector2.Lerp(bezierPoint1, bezierPoint2, progress);

            // draw bezier curve
            //Debug.DrawLine(dropStartPoint, dropBezierPoint + dropStartPoint, Color.yellow);
            //Debug.DrawLine(dropBezierPoint + dropStartPoint, randomDropPoint, Color.yellow);
            //Debug.DrawLine(bezierPoint1, bezierPoint2, Color.green);
            
            yield return null;
        }

        isDetecting = true;
    }

    protected virtual IEnumerator LootingProgress()
    {
        isDetecting = false;
        Vector2 lootingStartPoint = transform.position;
        for (float t = 0f; t < lootingProgressTime; t += Time.deltaTime)
        {
            float progress = t / lootingProgressTime;
            transform.position = Vector2.Lerp(lootingStartPoint, target.position, progress);
            yield return null;
        }
        vfxObject.transform.SetParent(null, true);
        OnLooting();
        Destroy(vfxObject, 1f);
        Destroy(gameObject);
    }

    protected virtual void OnLooting()
    {

    }

    public void AutoLooting()
    {
        target = GameManager.Instance.player.transform;
        detectRange = float.MaxValue;
    }
}