extends Control

@export var dialogue : String

var text_label
var wait_time_per_char = 0.04

func start_dialogue(dialogue: String):
	text_label.visible_characters = -1
	text_label.text = dialogue
	
	var parsed_text = text_label.get_parsed_text()
	var length = parsed_text.length()
	
	while text_label.visible_characters < length:
		text_label.visible_characters += 1
		await get_tree().create_timer(wait_time_per_char).timeout
		pass
	pass

# Called when the node enters the scene tree for the first time.
func _ready():
	text_label = get_child(1)
	await get_tree().create_timer(3).timeout
	start_dialogue(dialogue)
	pass # Replace with function body.
