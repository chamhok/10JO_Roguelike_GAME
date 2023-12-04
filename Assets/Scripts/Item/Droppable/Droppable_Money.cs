using UnityEngine;

public class Droppable_Money : DroppableItem
{
    int value = 1;
    float spinTime;
    Transform sprite;
    TrailRenderer trail;

    [Header("Sprite Settings")]
    [SerializeField] float maxScale = 0.66f;
    [SerializeField] float spinSpeed = 1f;

    protected override void OnEnable()
    {
        base.OnEnable();
        sprite = transform.GetChild(0);
        trail = GetComponentInChildren<TrailRenderer>();
        trail.startWidth = _scale * sprite.transform.localScale.x * 0.5f;
    }

    protected override void Update()
    {
        base.Update();

        spinTime += Time.deltaTime * spinSpeed;
        float x = Mathf.Abs(Mathf.Sin(spinTime));
        sprite.localScale = new Vector3(x * maxScale, 1f, 1f);
    }

    protected override void OnLooting()
    {

    }
}