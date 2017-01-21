using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class TexturesManager : MonoBehaviour {

	public int columns			= 8;
	public int rows 			= 8;

	public int currentFrame		= 1;

	private Vector2 framePosition;
	private Vector2 frameSize;
	private Vector2 frameOffset;
	private int i;

	protected Renderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();

		HandleMaterial ();
	}

	protected void HandleMaterial () {

		framePosition.y = 1;

		for (i = currentFrame; i > columns; i -= rows) {
		
			framePosition.y += 1;
		}

		framePosition.x = i - 1;

		frameSize = new Vector2 (1.0f / columns, 1.0f / rows);
		frameOffset = new Vector2 (framePosition.x / columns, 1.0f - (framePosition.y / rows));

		if (rend != null) {

			rend.sharedMaterial.SetTextureScale ("_MainTex", frameSize);
			rend.sharedMaterial.SetTextureOffset ("_MainTex", frameOffset);
		} else {
			Debug.Log ("rend == null!!!");
		}


	}
	

}
