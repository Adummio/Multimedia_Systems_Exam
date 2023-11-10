<?php

$servername = "localhost";
$username = "id9733406_adummio";
$password = "ApaMondaSpaziani";
$dbName = "id9733406_ams_db";

//Make connection
$link = mysqli_connect($servername, $username, $password, $dbName);

//Check connection
if (!$link) {
    echo "Error: Unable to connect to MySQL." . PHP_EOL;
    echo "Debugging errno: " . mysqli_connect_errno() . PHP_EOL;
    echo "Debugging error: " . mysqli_connect_error() . PHP_EOL;
    exit;
}

$modifica = mysqli_real_escape_string($link, $_POST["modifica"]);

$poi = $link->query("SELECT * FROM POI WHERE ID = '$_POST[id]'");

$info = mysqli_fetch_object($poi)->INFO;

if(is_null($info))
{
	$link->begin_transaction();
	$result = $link->query("INSERT INTO INFO(FRANCESE) VALUES('$modifica')");

	if($result == FALSE)
	{
		echo("Error description: " . mysqli_error($link));
		$link->rollback();
	}

	$value = $link->insert_id;

	if(!$link->query("UPDATE POI SET INFO = '$value' WHERE ID = '$_POST[id]'"))
	{
		echo("Error description: " . mysqli_error($link));
		$link->rollback();
	}
}
else
{
	$link->query("UPDATE INFO SET FRANCESE = '$modifica' WHERE ID = $infoid");
}




echo "Query successful.";
mysqli_close($link);

?>