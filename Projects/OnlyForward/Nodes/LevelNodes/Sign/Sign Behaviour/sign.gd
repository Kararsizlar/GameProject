extends Node2D


var sign_node
var clerp = preload("res://Sources/Classes/Clerp/clerp.gd" ).new()

var wait_time = 3
var lerp_time = 3

var close_y = 360
var open_y = 0

func open_sign():
	var x = 0
	while x < lerp_time:
		var delta = get_process_delta_time()
		x += delta
		
		sign_node.modulate.a = x
		sign_node.position.y = clerp.lerp_ease_in_out(close_y,open_y,x / lerp_time)
		
		await get_tree().process_frame
		
	sign_node.modulate.a = 1

func _ready():
	sign_node = get_child(0)
	await get_tree().create_timer(wait_time).timeout
	sign_node.visible = true
	open_sign()
