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

if(!mysqli_query($link, "INSERT INTO FOTO(POI, FOTO) VALUES('$_POST[id]','$_POST[modifica]')"))
{
	echo("Error description: " . mysqli_error($link));
}else
	echo "Query successful.";
	
mysqli_close($link);

?>