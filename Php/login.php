<?php

	require_once 'db_functions.php';
	$db = new DB_Functions();

	/*
	 * Endpoint: http://<domain>/login.php
	 * Method: GET
	 * Params: email, password
	 * Result: JSON
	 */

	$response = array();
	if(isset($_GET['email']) && isset($_GET['password'])){

		$email = $_GET['email'];
		$password = $_GET['password'];

		$user = $db->loginUser($email,$password);
	}
	else{
		$response["error_msg"] = "Required paramater (email,password) is missing!";
		echo json_encode($response);
	}

?>