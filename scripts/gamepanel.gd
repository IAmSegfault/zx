extends Panel
var style = StyleBoxFlat.new()
func _ready():
	style.set_bg_color(Color("#00001d"))
	var panel = get_node(".")
	panel.set('custom_styles/panel', style)

#func _process(delta):
#	# Called every frame. Delta is time since last frame.
#	# Update game logic here.
#	pass
