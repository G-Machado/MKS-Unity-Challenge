using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarManager : MonoBehaviour
{
    public BoatManager target;
    private Vector3 offset;

    public Slider slider;

    private void Start()
    {
        offset = new Vector3(0, 0, -1);
    }

    void Update()
    {
        Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(target.transform.position);
        transform.position = Vector3.Lerp(transform.position, target.transform.position - offset, .2f);

        slider.value = Mathf.Clamp(slider.value, (target.life + .01f) / target.maxLife, .2f);
    }
}
