extends Node

# class member variables go here, for example:
# var a = 2
# var b = "textvar"

var state = "random"
var selected_random = false
var stats = {}
func _ready():
	# Called when the node is added to the scene for the first time.
	# Initialization here
	var rc = get_node("./RandomCreate")
	rc.popup_centered()
	var global = get_node("/root/Global")
	stats = global.getStatBlocks()


func _input(event):
	if event is InputEventKey:
		if event.pressed and event.scancode == KEY_ESCAPE:
			if state == "random":
				var i = get_tree().change_scene("res://scenes/mainmenu.tscn")
			elif state == "class_select":
				var i = get_tree().change_scene("res://scenes/mainmenu.tscn")
			elif state == "stat_select":
				state = "class_select"
				var ss = get_node("./StatSelect")
				ss.visible = false
				ss.state = "hidden"
				ss.vlock = false

				var cs = get_node("./ClassSelect")
				cs.state = "visible"
				cs.vlock = true
				cs.popup_centered()
			elif state == "name_input":
				if not selected_random:
					state = "stat_select"
					var ni = get_node("./NameInput")
					ni.visible = false
					ni.state = "hidden"
					ni.vlock = false

					var ss = get_node("./StatSelect")
					ss.state = "visible"
					ss.vlock = true
					ss.popup_centered()
				else:
					var i = get_tree().change_scene("res://scenes/mainmenu.tscn")


func _on_RandomYesButton_pressed():
	selected_random = true
	state = "name_input"
	var rc = get_node("./RandomCreate")
	rc.state = "hidden"
	rc.vlock = false
	rc.visible = false
	var ni = get_node("./NameInput")
	ni.state = "visible"
	ni.vlock = true
	ni.visible = true
	ni.popup_centered()

func _on_RandomNoButton_pressed():
	var rc = get_node("./RandomCreate")
	rc.state = "hidden"
	rc.vlock = false
	rc.visible = false
	state = "class_select"
	var cs = get_node("./ClassSelect")
	cs.state = "visible"
	cs.vlock = true
	cs.popup_centered()

func _on_ClassSelectButton_pressed():
	
	var cs = get_node("./ClassSelect")
	cs.visible = false
	cs.state = "hidden"
	cs.vlock = false
	state = "stat_select"
	
	var str1 = get_node("./StatSelect/StatContainer/STR1")
	var dex1 = get_node("./StatSelect/StatContainer/DEX1")
	var con1 = get_node("./StatSelect/StatContainer/CON1")
	var int1 = get_node("./StatSelect/StatContainer/INT1")
	var wis1 = get_node("./StatSelect/StatContainer/WIS1")
	var cha1 = get_node("./StatSelect/StatContainer/CHA1")

	var str2 = get_node("./StatSelect/StatContainer/STR2")
	var dex2 = get_node("./StatSelect/StatContainer/DEX2")
	var con2 = get_node("./StatSelect/StatContainer/CON2")
	var int2 = get_node("./StatSelect/StatContainer/INT2")
	var wis2 = get_node("./StatSelect/StatContainer/WIS2")
	var cha2 = get_node("./StatSelect/StatContainer/CHA2")

	var str3 = get_node("./StatSelect/StatContainer/STR3")
	var dex3 = get_node("./StatSelect/StatContainer/DEX3")
	var con3 = get_node("./StatSelect/StatContainer/CON3")
	var int3 = get_node("./StatSelect/StatContainer/INT3")
	var wis3 = get_node("./StatSelect/StatContainer/WIS3")
	var cha3 = get_node("./StatSelect/StatContainer/CHA3")

	var str4 = get_node("./StatSelect/StatContainer/STR4")
	var dex4 = get_node("./StatSelect/StatContainer/DEX4")
	var con4 = get_node("./StatSelect/StatContainer/CON4")
	var int4 = get_node("./StatSelect/StatContainer/INT4")
	var wis4 = get_node("./StatSelect/StatContainer/WIS4")
	var cha4 = get_node("./StatSelect/StatContainer/CHA4")

	var str5 = get_node("./StatSelect/StatContainer/STR5")
	var dex5 = get_node("./StatSelect/StatContainer/DEX5")
	var con5 = get_node("./StatSelect/StatContainer/CON5")
	var int5 = get_node("./StatSelect/StatContainer/INT5")
	var wis5 = get_node("./StatSelect/StatContainer/WIS5")
	var cha5 = get_node("./StatSelect/StatContainer/CHA5")

	str1.text = str(stats["0"][0]) + "    "
	dex1.text = str(stats["0"][1]) + "    "
	con1.text = str(stats["0"][2]) + "    "
	int1.text = str(stats["0"][3]) + "    "
	wis1.text = str(stats["0"][4]) + "    "
	cha1.text = str(stats["0"][5]) + "    "

	str2.text = str(stats["1"][0]) + "    "
	dex2.text = str(stats["1"][1]) + "    "
	con2.text = str(stats["1"][2]) + "    "
	int2.text = str(stats["1"][3]) + "    "
	wis2.text = str(stats["1"][4]) + "    "
	cha2.text = str(stats["1"][5]) + "    "

	str3.text = str(stats["2"][0]) + "    "
	dex3.text = str(stats["2"][1]) + "    "
	con3.text = str(stats["2"][2]) + "    "
	int3.text = str(stats["2"][3]) + "    "
	wis3.text = str(stats["2"][4]) + "    "
	cha3.text = str(stats["2"][5]) + "    "

	str4.text = str(stats["3"][0]) + "    "
	dex4.text = str(stats["3"][1]) + "    "
	con4.text = str(stats["3"][2]) + "    "
	int4.text = str(stats["3"][3]) + "    "
	wis4.text = str(stats["3"][4]) + "    "
	cha4.text = str(stats["3"][5]) + "    "

	str5.text = str(stats["4"][0]) + "    "
	dex5.text = str(stats["4"][1]) + "    "
	con5.text = str(stats["4"][2]) + "    "
	int5.text = str(stats["4"][3]) + "    "
	wis5.text = str(stats["4"][4]) + "    "
	cha5.text = str(stats["4"][5]) + "    "
	var ss = get_node("./StatSelect")
	ss.visible = true
	ss.state = "visible"
	ss.vlock = true
	ss.popup_centered()


func _on_StatSelectButton_pressed():
	state = "name_input"
	var cs = get_node("./StatSelect")
	cs.state = "hidden"
	cs.vlock = false
	cs.visible = false

	var ni = get_node("./NameInput")
	ni.state = "visible"
	ni.vlock = true
	ni.visible = true
	ni.popup_centered()

func _on_NameInputButton_pressed():
	var ntb = get_node("./NameInput/NameTextBox")
	if ntb.text != "":
		var i = get_tree().change_scene("res://scenes/gameworld.tscn")

func _on_NameTextBox_text_entered(text):
	var ntb = get_node("./NameInput/NameTextBox")
	if ntb.text != "":
		var i = get_tree().change_scene("res://scenes/gameworld.tscn")