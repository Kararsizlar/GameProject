extends Node2D

var player

var player_walk
var player_gravity
var player_jump

func activate_object(index):
	var obj = get_child(index)
	obj.initialize(player)
	obj.set_active(true)
	return obj
	pass

func initialize(p):
	player = p
	player_gravity  = activate_object(0)
	player_walk     = activate_object(1)
	player_jump     = activate_object(2)
	pass

func update_movement():
	player_walk.step()
	player_gravity.step()
	player_jump.step()
	
	player.move_and_slide()
	pass
