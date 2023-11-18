shader_type canvas_item;
uniform sampler2D SCREEN_TEXTURE : hint_screen_texture, filter_linear_mipmap;

const float color_count = 4.0;
uniform vec4 dark_col : source_color = vec4(0.1, 0.1, 0.1, 1.0);
uniform vec4 meddark_col : source_color = vec4(0.3, 0.3, 0.3, 1.0);
uniform vec4 medbright_col : source_color = vec4(0.6, 0.6, 0.6, 1.0);
uniform vec4 bright_col : source_color = vec4(1.0, 1.0, 1.0, 1.0);

uniform float pixel_scale : hint_range(1.0, 16.0, 1.0) = 2.0;

//const int thresholds_size = 2;
//const float small_thresholds[4] = {
//	0.0, 3.0,
//	2.0, 1.0
//};

const int thresholds_size = 4;
const float thresholds[16] = {
	0.0, 8.0, 2.0, 10.0,
	12.0, 4.0, 14.0, 6.0,
	3.0, 11.0, 1.0, 9.0,
	15.0, 7.0, 13.0, 5.0
};

vec4 get_color(int index) {
	switch (index) {
		case 0:
			return dark_col;
		case 1:
			return meddark_col;
		case 2:
			return medbright_col;
		case 3:
			return bright_col;
		default: 
			return medbright_col;
	}
}

void fragment() {
	vec2 small_res = 1.0 / (SCREEN_PIXEL_SIZE * pixel_scale);
	vec2 sample_uv = floor(SCREEN_UV * small_res) / small_res + ((pixel_scale * SCREEN_PIXEL_SIZE) / 2.0);
	vec4 color = texture(SCREEN_TEXTURE, sample_uv);
	float brightness = (color.r + color.g + color.b) / 3.0;
		
	float x = SCREEN_UV.x / SCREEN_PIXEL_SIZE.x;
	float y = SCREEN_UV.y / SCREEN_PIXEL_SIZE.y;
	int threshold_x = int(mod(floor(x / pixel_scale), float(thresholds_size)));
	int threshold_y = int(mod(floor(y / pixel_scale), float(thresholds_size)));
	float my_threshold = thresholds[threshold_y * thresholds_size + threshold_x] /
		 				 (float(thresholds_size) * float(thresholds_size));
						
	int low_color_index = max(int(floor(brightness * color_count)) - 1, 0);
	int high_color_index = low_color_index + 1;

	if (brightness > (float(low_color_index + 1) + my_threshold) / color_count)
	{
		COLOR = get_color(high_color_index);
	} else {
		COLOR = get_color(low_color_index);
	}
}
