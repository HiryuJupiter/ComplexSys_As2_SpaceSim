using System.Collections;
using UnityEngine;

public interface IUITextPoolable
{
    void SetUp(UIScoreTextObjectPool pool, RectTransform canvasRect);
    void Activation(string text, Vector2 targetPosition);
}
