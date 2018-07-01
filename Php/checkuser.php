<?php

	require_once 'db_functions.php';
	$db = new DB_Functions();

	/*
	 * Endpoint: http://<domain>/checkuser.php
	 * Method: GET
	 * Params: email
	 * Result: JSON
	 */

	$response = array();
	if(isset($_GET['email'])){

		$phone = $_GET['email'];

		if($db->checkExistsUser($phone)){

			$response["exists"] = TRUE;
			echo json_encode($response);
		}else{

			$response["exists"] = FALSE;
			echo json_encode($response);
		}
	}
	else{
		$response["error_msg"] = "Required paramater (email) is missing!";
		echo json_encode($response);
	}

?>