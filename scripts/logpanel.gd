extends Panel
var style = StyleBoxFlat.new()
func _ready():
	style.set_bg_color(Color("#00001d"))
	var panel = get_node(".")
	panel.set('custom_styles/panel', style)
    # The Panel doc tells you which style names there are
