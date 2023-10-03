extends Node2D

var door
var area
@export var teleport_value : String

func _ready():
	initialize()

func go_to_next_scene():
	get_tree().change_scene_to_file('res://Nodes/Scenes/' + teleport_value + '.tscn')

func initialize():
	door = self
	area = get_child(1)
	area.initialize(door)
