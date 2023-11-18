shader_type canvas_item;


const int dither_thresholds_size = 4;
const float dither_thresholds[16] = {
	0.0, 8.0, 2.0, 10.0,
	12.0, 4.0, 14.0, 6.0,
	3.0, 11.0, 1.0, 9.0,
	16.0, 7.0, 13.0, 5.0
};

void fragment() {
	vec4 color = texture(TEXTURE, UV);
	float brightness = (color.r + color.g + color.b) / 3.0;
	
	float x = UV.x / TEXTURE_PIXEL_SIZE.x;
	float y = UV.y / TEXTURE_PIXEL_SIZE.y;
	
	int x_threshold = int(mod(x, float(dither_thresholds_size)));
	int y_threshold = int(mod(y, float(dither_thresholds_size)));
	
	float threshold = dither_thresholds[y_threshold * dither_thresholds_size + x_threshold];
	
	if (brightness > (threshold / (float(dither_thresholds_size) * float(dither_thresholds_size))))
	{
		COLOR = vec4(0.5, 1.0, 0.8, 1.0);
	} else {
		COLOR = vec4(0.0, 0.0, 0.0, 0.0);
	}
	
}
