<?php

	require_once 'db_functions.php';
	$db = new DB_Functions();

	/*
	 * Endpoint: http://<domain>/getList.php?user=[value]
	 * Method: GET
	 * Params: user
	 * Result: JSON
	 */

	$response = array();
	if(isset($_GET['user'])){

		$user = $_GET['user'];
		$db->getList($user);
	}
	else{
		$response["error_msg"] = "Required paramater (user) is missing!";
		echo json_encode($response);
	}

?>