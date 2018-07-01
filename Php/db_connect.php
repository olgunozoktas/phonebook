<?php

class DB_Connect{
	private $conn;

	public function connect(){

		try{
		require_once 'config.php';
		//$this->conn = new mysqli(DB_HOST,DB_USER,DB_PASSWORD,DB_DATABASE);
		//return $this->conn;

		$dsn = 'mysql:host='. DB_HOST .';dbname='. DB_DATABASE;
  		$this->conn = new PDO($dsn, DB_USER, DB_PASSWORD);
  		$this->conn->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);
  		return $this->conn;

 		}catch(PDOException $e){
  			echo "error".$e->getMessage();
  		}
	}
}