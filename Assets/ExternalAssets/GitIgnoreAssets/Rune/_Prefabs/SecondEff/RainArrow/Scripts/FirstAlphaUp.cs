using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class FirstAlphaUp : MonoBehaviour {

	private SpriteRenderer m_spriteRenderer;
	private float m_alphaUpSpeed = 1.5f;
	public float m_alphaWait;

	void Start () {
		StartCoroutine (AlphaDown ());
		m_spriteRenderer = GetComponent<SpriteRenderer> ();
		m_spriteRenderer.color = new Color (1.0f, 1.0f, 1.0f, 0.0f);

		float alpha = 0.0f;
		this.UpdateAsObservable ()
			.TakeWhile (_ => m_spriteRenderer.color.a != 1.0f)
			.Subscribe (_ => {
			m_spriteRenderer.color = new Color (1.0f, 1.0f, 1.0f, alpha);
			alpha += Time.deltaTime * m_alphaUpSpeed;
		});
	}

	IEnumerator AlphaDown () {
		yield return new WaitForSeconds (m_alphaWait);
		float alpha = 1.0f;
		while (m_spriteRenderer.color.a > 0) {
			m_spriteRenderer.color = new Color (1.0f, 1.0f, 1.0f, alpha);
			alpha -= Time.deltaTime;
			yield return null;
		}
		Destroy (transform.root.gameObject);
	}
}
