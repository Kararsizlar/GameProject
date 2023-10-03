extends Control
var clerp = preload("res://Sources/Classes/clerp.gd").new()
var moving

var time_to_move = 0.8
var out_pos = -448
var in_pos  = 0

var sec_element
var sec_is_open

func move_element(element,is_open,is_secondary = false):
	if moving:
		return
	
	if is_secondary:
		sec_element = null
		sec_is_open = null
		
	var from
	var to
	
	if is_open:
		from = in_pos
		to = out_pos
	else:
		from = out_pos
		to = in_pos
		
	moving = true
	var time_passed = 0
	
	element.is_on_screen = !element.is_on_screen
	while(time_passed < time_to_move):
		time_passed += get_process_delta_time()
		element.position.x = clerp.lerp_ease_in_out(from,to,time_passed,time_to_move)
		await get_tree().process_frame
		pass
		
	moving = false
	
	if sec_element == null:
		return
	
	move_element(sec_element,sec_is_open,true)
	pass

func set_secondary(element,is_open):
	sec_element = element
	sec_is_open = is_open
