shader_type canvas_item;

uniform sampler2D SCREEN_TEXTURE : hint_screen_texture, filter_linear_mipmap;

uniform vec4 bright_color : source_color = vec4(0.5, 1.0, 0.8, 1.0);
uniform vec4 dark_color : source_color = vec4(0.1, 0.0, 0.2, 1.0);
uniform float pixel_scale = 4.0;
const int dither_thresholds_size = 4;
const float dither_thresholds[16] = {
	0.0, 8.0, 2.0, 10.0,
	12.0, 4.0, 14.0, 6.0,
	3.0, 11.0, 1.0, 9.0,
	15.0, 7.0, 13.0, 5.0
};

void fragment() {
	vec2 small_res = 1.0 / (SCREEN_PIXEL_SIZE * pixel_scale);
	vec2 sample_uv = floor(SCREEN_UV * small_res) / small_res + ((pixel_scale * SCREEN_PIXEL_SIZE) / 2.0);
	vec4 color = texture(SCREEN_TEXTURE, sample_uv);
	float brightness = (color.r + color.g + color.b) / 3.0;
	
	float x = SCREEN_UV.x / SCREEN_PIXEL_SIZE.x;
	float y = SCREEN_UV.y / SCREEN_PIXEL_SIZE.y;
	
	int x_threshold = int(mod(floor(x / pixel_scale), float(dither_thresholds_size)));
	int y_threshold = int(mod(floor(y / pixel_scale), float(dither_thresholds_size)));
	
	float threshold = dither_thresholds[y_threshold * dither_thresholds_size + x_threshold];
	
	if (brightness > (threshold / (float(dither_thresholds_size) * float(dither_thresholds_size))))
	{
		COLOR = bright_color;
	} else {
		COLOR = dark_color;
	}
	
}
