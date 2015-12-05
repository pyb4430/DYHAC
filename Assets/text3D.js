#pragma strict

var mat : Material;
var font : Font;
function Awake() {
	var Text = new GameObject();
	var textMesh = gameObject.AddComponent(TextMesh);
	textMesh.font = font;
	var meshRenderer = gameObject.AddComponent(MeshRenderer);
	meshRenderer.material = mat;
	textMesh.text = "Hello World!";
}