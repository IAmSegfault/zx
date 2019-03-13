extends PopupMenu

# class member variables go here, for example:
# var a = 2
# var b = "textvar"
var state = "hidden"
var vlock = false
var classes = []

func _ready():
	# Called when the node is added to the scene for the first time.
	# Initialization here
	var file = File.new()
	file.open("res://data/character/enforcer.json", file.READ)
	var text = file.get_as_text()
	file.close()

	var json_data = JSON.parse(text)
	var data = json_data.result
	classes.append(data)

	file.open("res://data/character/mentat.json", file.READ)
	text = file.get_as_text()
	file.close()

	json_data = JSON.parse(text)
	data = json_data.result
	classes.append(data)

	file.open("res://data/character/ranger.json", file.READ)
	text = file.get_as_text()
	file.close()

	json_data = JSON.parse(text)
	data = json_data.result
	classes.append(data)

	var description = get_node("./ClassDescription")
	description.append_bbcode(classes[0]["description"])
	
func _process(delta):
	if not is_visible():
		state = "hidden"
	else:
		state = "visible" 
	if state != "visible" and vlock:
		popup_centered()
#	# Called every frame. Delta is time since last frame.
#	# Update game logic here.
#	pass


func _on_CheckBoxEnforcer_focus_entered():
	var description = get_node("./ClassDescription")
	description.clear()
	description.append_bbcode(classes[0]["description"])


func _on_CheckBoxMentat_focus_entered():
	var description = get_node("./ClassDescription")
	description.clear()
	description.append_bbcode(classes[1]["description"])

func _on_CheckBoxRanger_focus_entered():
	var description = get_node("./ClassDescription")
	description.clear()
	description.append_bbcode(classes[2]["description"])


func _on_CheckBoxEnforcer_pressed():
	var enforcer = get_node("./CheckBoxContainer/CheckBoxEnforcer")
	if not enforcer.pressed:
		enforcer.pressed = true

	var mentat = get_node("./CheckBoxContainer/CheckBoxMentat")
	mentat.pressed = false

	var ranger = get_node("./CheckBoxContainer/CheckBoxRanger")
	ranger.pressed = false

func _on_CheckBoxMentat_pressed():
	var mentat = get_node("./CheckBoxContainer/CheckBoxMentat")
	if not mentat.pressed:
		mentat.pressed = true

	var enforcer = get_node("./CheckBoxContainer/CheckBoxEnforcer")
	enforcer.pressed = false

	var ranger = get_node("./CheckBoxContainer/CheckBoxRanger")
	ranger.pressed = false


func _on_CheckBoxRanger_pressed():
	var ranger = get_node("./CheckBoxContainer/CheckBoxRanger")
	if not ranger.pressed:
		ranger.pressed = true

	var enforcer = get_node("./CheckBoxContainer/CheckBoxEnforcer")
	enforcer.pressed = false

	var mentat = get_node("./CheckBoxContainer/CheckBoxMentat")
	mentat.pressed = false
