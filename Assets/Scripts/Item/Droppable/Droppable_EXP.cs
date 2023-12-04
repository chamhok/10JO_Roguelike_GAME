using UnityEngine;

public class Droppable_EXP : DroppableItem
{
    int value = 1;
    float colorTime;
    SpriteRenderer bigCircle;
    SpriteRenderer smallCircle;
    TrailRenderer trail;

    [Header("Color Settings")]
    [SerializeField] Gradient bigCircleColor;
    [SerializeField] Gradient smallCircleColor;

    protected override void OnEnable()
    {
        base.OnEnable();
        bigCircle = transform.GetChild(0).GetComponent<SpriteRenderer>();
        smallCircle = transform.GetChild(1).GetComponent<SpriteRenderer>();
        trail = GetComponentInChildren<TrailRenderer>();
        trail.startWidth = _scale * smallCircle.transform.localScale.x;
    }

    protected override void Update()
    {
        base.Update();

        colorTime += Time.deltaTime;
        if (colorTime > 1f) colorTime -= 1f;
        bigCircle.color = bigCircleColor.Evaluate(colorTime);
        smallCircle.color = smallCircleColor.Evaluate(colorTime);
        trail.startColor = smallCircleColor.Evaluate(colorTime);
        trail.endColor = smallCircleColor.Evaluate(colorTime);
    }

    protected override void OnLooting()
    {

    }
}