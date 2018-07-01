<?php

	require_once 'db_functions.php';
	$db = new DB_Functions();

	/*
	 * Endpoint: http://<domain>/updateUser.php
	 * Method: GET
	 * Params: id, name, email, phone, address
	 * Result: JSON
	 */

	$response = array();
	if(isset($_GET['id']) &&
		isset($_GET['name']) &&
		isset($_GET['email']) &&
		isset($_GET['phone']) &&
		isset($_GET['address'])){

		$id = $_GET['id'];
		$name = $_GET['name'];
		$email = $_GET['email'];
		$phone = $_GET['phone'];
		$address = $_GET['address'];

		//Update the record
		$user = $db->updateUser($id,$name,$email,$phone,$address);
	}
	else{
		$response["error_msg"] = "Required paramater (id,name,email,password,address) is missing!";
		echo json_encode($response);
	}

?>