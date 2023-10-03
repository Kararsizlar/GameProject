extends Node2D

var player

var player_movement

func initialize(p):
	player = p
	player_movement = get_child(0)
	player_movement.initialize(p)
	pass

func _physics_process(delta):
	player_movement.update_movement()
