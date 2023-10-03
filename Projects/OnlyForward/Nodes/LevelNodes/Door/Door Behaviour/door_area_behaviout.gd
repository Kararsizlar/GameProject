extends Area2D

var door

# Called when the node enters the scene tree for the first time.
func initialize(d):
	door = d
	pass

func _on_body_entered(body):
	print("Connection!")
	if body.name == "player":
		door.go_to_next_scene()
	
	pass
