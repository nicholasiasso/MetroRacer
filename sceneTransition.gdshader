shader_type canvas_item;

// 0.0 = Scene Visible
// 1.0 = Scene Not Visible
uniform float fadeRatio : hint_range(0.0, 1.0) = 0.0;

void fragment() {
	COLOR = vec4(0.0, 0.0, 0.0, fadeRatio);
}
