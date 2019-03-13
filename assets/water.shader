shader_type canvas_item;

void fragment()
{
	vec2 offset;
	offset.x = cos(TIME) * 0.08;
	offset.y = sin(TIME) * 0.08;
	COLOR = texture(TEXTURE, UV + offset);
}