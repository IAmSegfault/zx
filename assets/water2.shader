shader_type canvas_item;

void fragment()
{
	vec2 offset;
	offset.x = cos(TIME + UV.x + UV.y) * 0.05;
	offset.y = sin(TIME + UV.x + UV.y) * 0.05;
	COLOR = texture(TEXTURE, UV + offset);
}