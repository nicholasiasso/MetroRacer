shader_type canvas_item;

uniform sampler2D SCREEN_TEXTURE : hint_screen_texture, filter_linear_mipmap;

uniform vec4 bright_color : source_color = vec4(0.5, 1.0, 0.8, 1.0);
uniform vec4 dark_color : source_color = vec4(0.1, 0.0, 0.2, 1.0);
uniform float pixel_scale : hint_range(1.0, 8.0, 1.0) = 4.0;
const int dither_thresholds_size = 4;
const float dither_thresholds[16] = {
	0.0, 8.0, 2.0, 10.0,
	12.0, 4.0, 14.0, 6.0,
	3.0, 11.0, 1.0, 9.0,
	15.0, 7.0, 13.0, 5.0
};

float threshold(float val, float threshold, float low, float high)
{
	if (val > threshold)
	{
		return high;
	}
	return low;
}

void fragment() {
	vec2 small_res = 1.0 / (SCREEN_PIXEL_SIZE * pixel_scale);
	vec2 sample_uv = floor(SCREEN_UV * small_res) / small_res + ((pixel_scale * SCREEN_PIXEL_SIZE) / 2.0);
	vec4 color = texture(SCREEN_TEXTURE, sample_uv);
	
	float screen_x = SCREEN_UV.x / SCREEN_PIXEL_SIZE.x;
	float screen_y = SCREEN_UV.y / SCREEN_PIXEL_SIZE.y;
	
	int x_threshold = int(mod(floor(screen_x / pixel_scale), float(dither_thresholds_size)));
	int y_threshold = int(mod(floor(screen_y / pixel_scale), float(dither_thresholds_size)));
	
	float gate = dither_thresholds[y_threshold * dither_thresholds_size + x_threshold] / (float(dither_thresholds_size) * float(dither_thresholds_size));
	COLOR = vec4(
		threshold(color.r, gate, dark_color.r, bright_color.r),
		threshold(color.g, gate, dark_color.g, bright_color.g),
		threshold(color.b, gate, dark_color.b, bright_color.b),
		1.0f);
	
}
