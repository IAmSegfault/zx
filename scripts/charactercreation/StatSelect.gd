extends PopupMenu

# class member variables go here, for example:
# var a = 2
# var b = "textvar"
var state = "hidden"
var vlock = false

func _ready():
	# Called when the node is added to the scene for the first time.
	# Initialization here
	var sb1 = get_node("./SelectContainer/StatBlock1")
	sb1.pressed = true

func _process(delta):
	if not is_visible():
		state = "hidden"
	else:
		state = "visible" 

	if state != "visible" and vlock:
		popup_centered()

func _on_StatBlock1_pressed():
	var sb1 = get_node("./SelectContainer/StatBlock1")
	var sb2 = get_node("./SelectContainer/StatBlock2")
	var sb3 = get_node("./SelectContainer/StatBlock3")
	var sb4 = get_node("./SelectContainer/StatBlock4")
	var sb5 = get_node("./SelectContainer/StatBlock5")

	sb1.pressed = true
	sb2.pressed = false
	sb3.pressed = false
	sb4.pressed = false
	sb5.pressed = false

func _on_StatBlock2_pressed():
	var sb1 = get_node("./SelectContainer/StatBlock1")
	var sb2 = get_node("./SelectContainer/StatBlock2")
	var sb3 = get_node("./SelectContainer/StatBlock3")
	var sb4 = get_node("./SelectContainer/StatBlock4")
	var sb5 = get_node("./SelectContainer/StatBlock5")

	sb1.pressed = false
	sb2.pressed = true
	sb3.pressed = false
	sb4.pressed = false
	sb5.pressed = false

func _on_StatBlock3_pressed():
	var sb1 = get_node("./SelectContainer/StatBlock1")
	var sb2 = get_node("./SelectContainer/StatBlock2")
	var sb3 = get_node("./SelectContainer/StatBlock3")
	var sb4 = get_node("./SelectContainer/StatBlock4")
	var sb5 = get_node("./SelectContainer/StatBlock5")

	sb1.pressed = false
	sb2.pressed = false
	sb3.pressed = true
	sb4.pressed = false
	sb5.pressed = false

func _on_StatBlock4_pressed():
	var sb1 = get_node("./SelectContainer/StatBlock1")
	var sb2 = get_node("./SelectContainer/StatBlock2")
	var sb3 = get_node("./SelectContainer/StatBlock3")
	var sb4 = get_node("./SelectContainer/StatBlock4")
	var sb5 = get_node("./SelectContainer/StatBlock5")

	sb1.pressed = false
	sb2.pressed = false
	sb3.pressed = false
	sb4.pressed = true
	sb5.pressed = false

func _on_StatBlock5_pressed():
	var sb1 = get_node("./SelectContainer/StatBlock1")
	var sb2 = get_node("./SelectContainer/StatBlock2")
	var sb3 = get_node("./SelectContainer/StatBlock3")
	var sb4 = get_node("./SelectContainer/StatBlock4")
	var sb5 = get_node("./SelectContainer/StatBlock5")

	sb1.pressed = false
	sb2.pressed = false
	sb3.pressed = false
	sb4.pressed = false
	sb5.pressed = true