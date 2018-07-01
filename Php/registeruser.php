<?php

	require_once 'db_functions.php';
	$db = new DB_Functions();

	/*
	 * Endpoint: http://<domain>/registeruser.php
	 * Method: GET
	 * Params: name, email, password
	 * Result: JSON
	 */

	$response = array();
	if(isset($_GET['name']) &&
		isset($_GET['email']) &&
		isset($_GET['password'])){

		$name = $_GET['name'];
		$email = $_GET['email'];
		$password = $_GET['password'];

		if($db->checkExistsUser($email)){

			$response["error_msg"] = true;
			echo json_encode($response);
		}else{

			//Create new user
			$user = $db->registerNewUser($name,$email,$password);
		}
	}
	else{
		$response["error_msg"] = "Required paramater (name,email,password) is missing!";
		echo json_encode($response);
	}

?>