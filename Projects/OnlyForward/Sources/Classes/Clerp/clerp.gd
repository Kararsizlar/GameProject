extends Node

func lerp_ease_in(from,to,current_value,target_value = 1):
	var x = current_value / target_value
	var func_value = 1 - cos((PI * x) / 2);
	var return_value = lerp(from,to,func_value)
	return return_value

func lerp_ease_in_out(from,to,current_value,target_value = 1):
	var x = current_value / target_value
	var func_value = -(cos(PI * x) - 1) / 2;
	var return_value = lerp(from,to,func_value)
	return return_value
