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

$poi = $link->query("SELECT * FROM POI WHERE ID = '$_POST[id]'");

$audioid = mysqli_fetch_object($poi)->AUDIO;

if(is_null($audioid))
{
	$result = $link->query("INSERT INTO AUDIO(ITALIANO) VALUES('$_POST[modifica]')");

	if($result == FALSE)
	{
		echo("Error description: " . mysqli_error($link));
		$link->rollback();
	}


	$value = $link->insert_id;

	if(!$link->query("UPDATE POI SET AUDIO = '$value' WHERE ID = '$_POST[id]'"))
	{
		echo("Error description: " . mysqli_error($link));
		$link->rollback();
	}
}
else
{
	$audio = $link->query("SELECT * FROM AUDIO WHERE ID = $audioid");
	$italiano = mysqli_fetch_object($audio)->ITALIANO;
	unlink("audio/" . $italiano);
	$link->query("UPDATE AUDIO SET ITALIANO = '$_POST[modifica]' WHERE ID = $audioid");
}




echo "Query successful.";
mysqli_close($link);

?>