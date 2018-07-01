<?php

	require_once 'db_functions.php';
	$db = new DB_Functions();

	/*
	 * Endpoint: http://<domain>/addNew.php
	 * Method: GET
	 * Params: user, name, email, phone, address
	 * Result: JSON
	 */

	$response = array();
	if(isset($_GET['user']) &&
		isset($_GET['name']) &&
		isset($_GET['email']) &&
		isset($_GET['phone']) &&
		isset($_GET['address'])){

		$user = $_GET['user'];
		$name = $_GET['name'];
		$email = $_GET['email'];
		$phone = $_GET['phone'];
		$address = $_GET['address'];

		if($db->checkExistsUser($email)){

			$response["error_msg"] = true;
			echo json_encode($response);
		}else{

			//Create new user
			$user = $db->addNew($user,$name,$email,$phone,$address);
		}
	}
	else{
		$response["error_msg"] = "Required paramater (name,email,password) is missing!";
		echo json_encode($response);
	}

?>