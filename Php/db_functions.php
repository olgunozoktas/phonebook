<?php

class DB_Functions{
	private $conn;

	function __construct(){

		require_once 'db_connect.php';
		$db = new DB_Connect();
		$this->conn = $db->connect();
	}

	function __destruct(){

	}

	/*
	 * Check user exist
	 * return true/false
	 */

	function checkExistsUser($email){

		$stmt = $this->conn->prepare("SELECT * FROM user Where email=:email");
		$stmt->execute(['email'=>$email]);
		$result = $stmt->fetchAll();
		$num_rows = count($result);

		if($num_rows > 0){
			return true;
		}else{
			return false;
		}
	}

	public function loginUser($email,$password){

		$stmt = $this->conn->prepare("SELECT * FROM user Where email=:email and password=:password");
		$stmt->execute(['email'=>$email,'password'=>$password]);
		$result = $stmt->fetchAll();
		$num_rows = count($result);

			if($num_rows > 0){
				foreach($result as $res){
				echo '{"exists":true, "uid":'.$res->id."}";
			}
		}else{
			echo '{"exists":false"}';
		}
	}

	/*
	 * Register new user
	 * return User object if user was created
	 * return false and show error message if have exceptions
	 */

	public function registerNewUser($name, $email, $password){

		//echo 'Email'.$email;

		$stmt = $this->conn->prepare("INSERT INTO User(name, email, password) VALUES (:name,:email,:password)");
		$stmt->execute(['name'=>$name,'email'=>$email,'password'=>$password]);

		$stmt = $this->conn->prepare("SELECT * FROM User WHERE email=:email and password=:password");
		$stmt->execute(['email'=>$email,'password'=>$password]);
		$user = $stmt->fetchAll();
		$num_rows = count($user);

		if($num_rows > 0){
			$items = array();
			$items["error_msg"] = false;
			$items["user"] = $user[0];
			echo json_encode($items);
		}else{
			echo '{"error_msg":true}';
		}
	}

	public function getList($user_id){

		  $stmt = $this->conn->prepare("SELECT * FROM records WHERE user=:user");
          $stmt->execute(['user'=>$user_id]);
          $results = $stmt->fetchAll();
          $num_rows = count($results);

          if($num_rows > 0){
          	$items = array();
			$items["exists"] = true;
			$items["items"] = $results;
          	echo json_encode($items);
          }else {
          	echo '{"exists":false}';
      	  }
	}

	public function addNew($user_id, $name, $email, $phone, $address){

		$stmt = $this->conn->prepare("INSERT INTO records(user, name, email, phone, address) VALUES (:user, :name,:email,:phone, :address)");
		$stmt->execute(['user'=> $user_id, 'name'=>$name,'email'=>$email,'phone'=>$phone,'address'=>$address]);		

		$stmt = $this->conn->prepare("SELECT * FROM records WHERE email=:email");
		$stmt->execute(['email'=>$email]);
		$user = $stmt->fetchAll();
		$num_rows = count($user);

		if($num_rows > 0){
			echo '{"error_msg":false}';
		}else{
			echo '{"error_msg":true}';
		}		
	}

	public function updateUser($id, $name, $email, $phone, $address){

		$stmt = $this->conn->prepare("UPDATE records set name=:name, email=:email, phone=:phone, address=:address where id=:id");
		$stmt->execute(['id'=> $id, 'name'=>$name,'email'=>$email,'phone'=>$phone,'address'=>$address]);		

		$stmt = $this->conn->prepare("SELECT * FROM records WHERE email=:email and name=:name and phone=:phone and address=:address and id=:id");
		$stmt->execute(['id'=>$id,'name'=>$name,'email'=>$email,'phone'=>$phone,'address'=>$address]);
		$user = $stmt->fetchAll();
		$num_rows = count($user);

		if($num_rows > 0){
			echo '{"error_msg":false}';
		}else{
			echo '{"error_msg":true}';
		}		
	}
}