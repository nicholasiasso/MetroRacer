shader_type canvas_item;

uniform float gradient_dist_from_center_ratio = 0.5;
uniform float gradient_radius_ratio = 0.8;
uniform float alpha :hint_range(0.0, 1.0) = 1.0;
uniform float angle_offset = 0.0f;
uniform float rotation_speed_mult = 1.0;
uniform vec4 dark_color : source_color = vec4(0.0, 0.0, 0.0, 1.0);
uniform vec4 bright_color : source_color = vec4(1.0, 1.0, 1.0, 1.0);

void fragment() {
	vec2 screen_dims = 1.0 / SCREEN_PIXEL_SIZE;
	vec2 pixel_pos = SCREEN_UV / SCREEN_PIXEL_SIZE;
	
	float gradient_width = gradient_radius_ratio * screen_dims.x;
	
	vec2 light_pos = vec2(sin((TIME * rotation_speed_mult) + angle_offset), cos((TIME * rotation_speed_mult) + angle_offset)) * (gradient_dist_from_center_ratio * screen_dims.x) + (screen_dims / 2.0);
	float dist_from_light = length(abs(light_pos - pixel_pos));

	
	float brightness = 1.0 - clamp(dist_from_light / gradient_width, 0.0, 1.0);
	COLOR = mix(dark_color, bright_color, vec4(brightness));
	COLOR.a = COLOR.a * alpha;
}
