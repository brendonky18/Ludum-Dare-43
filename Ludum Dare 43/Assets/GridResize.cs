using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridResize : MonoBehaviour {
    public int height, width;

    void Start() {
        RectTransform container = gameObject.GetComponent<RectTransform>();
        GridLayoutGroup grid = gameObject.GetComponent<GridLayoutGroup>();

        grid.cellSize = new Vector2(container.rect.width / width, container.rect.height / height);
    }
}
