using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour, IUITextPoolable
{
	const float UnfadedDuration = 5.5f;
	const float FadeSpeed = 1f;
	const float MoveSpeed = 100f;

	Text uiText;
	RectTransform rectTrans;

	//Cache
	Color startingColor;
	Vector2 uiOffset;
	UIScoreTextObjectPool pool;

    public void SetUp(UIScoreTextObjectPool pool, RectTransform canvasRect)
    {
		//Reference
		rectTrans = GetComponent<RectTransform>();
		uiText = GetComponent<Text>();
		this.pool = pool;

		//Cache
		uiOffset = canvasRect.sizeDelta * .5f;
		startingColor = uiText.color;
	}

	public void Activation(string text, Vector2 targetPosition)
	{
		//Set text attributes
		uiText.text = text;
		uiText.color = startingColor;

		//Set position
		Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, targetPosition);
		rectTrans.localPosition = screenPos - uiOffset;

		StartCoroutine(Fade());
	}

	public IEnumerator Fade()
	{
		float a = 1f;

        for (float i = 0; i < UnfadedDuration; i += Time.deltaTime)
        {
			MoveUp();
			yield return null;
        }

		while (a > 0f)
        {
			MoveUp();

			//Fade out
			a -= FadeSpeed * Time.deltaTime;
			startingColor.a = a;
			uiText.color = startingColor;
			yield return null;
		}
		pool.Despawn(gameObject);
	}


    void MoveUp ()
    {
		rectTrans.localPosition += Vector3.up * MoveSpeed * Time.deltaTime;
	}

}

////Face camera
//transform.LookAt(transform.position + camera.rotation * Vector3.forward,
//camera.rotation * Vector3.up);